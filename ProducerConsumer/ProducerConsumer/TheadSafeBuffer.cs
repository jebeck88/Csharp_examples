using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class ThreadSafeBuffer
    {
        static readonly int CAPACITY = 3;

        /// <summary>
        /// Get a handle to the singleton instance
        /// </summary>
        public static ThreadSafeBuffer Instance
        {
            get
            {
                return sInstance;
            }
        }

        // Property that returns the next sequential item
        public string NextItem
        {
            get
            {
                lock(mLock)
                {
                    return "Item-" + mNextItemNumber++;
                }
            }
        }
        
        // Blocks the caller until a slot is available and the appends an item to the buffer
        public void Push(string username, string item)
        {
            // Wait for a slot
            mSlotsSemaphore.WaitOne();

            // Push the item to the back
            lock(mLock)
            {
                // Append the item
                mBuffer.Enqueue(item);
                
                // Signal that an item is available 
                mItmesSemaphore.Release();

                Console.WriteLine($"\"{username}\" cooked \"{item}\"");
            }
        }

        // Blocks the caller until an item is available and then pops it from the buffer
        public string Pop(string username)
        {
            // Wait for an item
            mItmesSemaphore.WaitOne();

            lock(mLock)
            {
                // Get an item
                var item = mBuffer.Dequeue();

                // Signal that a slot is now available
                mSlotsSemaphore.Release();

                Console.WriteLine($"\"{username}\" ate \"{item}\"");

                // Return the tiem
                return item;
            }
        }

        // Returns the count of items in the queue
        public int Count()
        {
            lock (mLock)
            {
                return mBuffer.Count;
            }
        }

        public override string ToString()
        {
            lock (mLock)
            {
                var result = $"[{string.Join(", ", mBuffer)}]";
                return result;
            }
        }


        // Private constructor.  This is a singleton
        private ThreadSafeBuffer() { }

        // Singleton instance, created in a thread-safe way w/out a lock
        private static readonly ThreadSafeBuffer sInstance = new();

        // Semaphore for slots
        private Semaphore mSlotsSemaphore = new(CAPACITY, CAPACITY);

        // Semaphore for items
        private Semaphore mItmesSemaphore = new(0, CAPACITY);

        // lock
        private object mLock = new object();

        // Fixed size queue
        private Queue<string> mBuffer = new(CAPACITY);

        // The next item number
        private int mNextItemNumber = 0;
    }
}
