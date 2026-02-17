using System;
using System.Security.Cryptography;

namespace ProducerConsumer
{
    public class Program
    {

        public static void ProducerFunction(string name)
        {
            var rng = new Random();
            var buffer = ThreadSafeBuffer.Instance;
            int count = 0;
            while (count++ < 3)
            {
                buffer.Push(name, buffer.NextItem);

                int sleepSec = rng.Next() % 6 + 1;
                int sleepMs = 1000 * sleepSec;

                Thread.Sleep(sleepMs);
            }
        }

        public static void ConsumerFunction(string name)
        {
            var rng = new Random();
            var buffer = ThreadSafeBuffer.Instance;
            int count = 0;
            while (count++ < 3)
            {
                var item = buffer.Pop(name);

                int sleepSec = rng.Next() % 6 + 1;
                int sleepMs = 1000 * sleepSec;

                Thread.Sleep(sleepMs);
            }
        }


        public static async Task Main(string[] args)
        {
            // Get a handle to the buffer
            var buffer = ThreadSafeBuffer.Instance;

            // List of worker tasks
            List<Task> workers = new();

            // Let's go!
            Console.WriteLine($"Starting. {buffer.ToString()}");

            // Create some producer threads
            for (int i = 0; i < 3; i++)
            {

                string name = "Cook-" + i;
                var t = Task.Run(() => ProducerFunction(name));
                workers.Add(t);
            }

            // Create some consumer threads
            for (int i = 0; i < 3; i++)
            {
                string name = "Eater-" + i;
                var t = Task.Run(() => ConsumerFunction(name));
                workers.Add(t);
            }

            // Wait for all the workers to complete
            Task.WaitAll(workers.ToArray());

            // All done. 
            Console.WriteLine($"Finished. {buffer.ToString()}");
        }
    }
} 