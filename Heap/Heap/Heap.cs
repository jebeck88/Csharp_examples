using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    /// <summary>
    /// A simple heap, backed by a list, and ordered by an IComparable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> where T : IComparable
    {
        /// <summary>
        /// Number of items in the heap
        /// </summary>
        public int COUNT { get => _mItems.Count; }

        /// <summary>
        /// True if empty
        /// </summary>
        public bool EMPTY { get => _mItems.Count == 0; }

        /// <summary>
        /// Returns the comparer used to order this heap
        /// </summary>
        public IComparer<T> COMPARER { get => _mComparer; }


        /// <summary>
        /// Constructor.  Comparer determines how the heap is constructed
        /// 
        /// comparer == '<' ==> Min Heap with smallest value at root
        /// cpmparer == '>' ==> Max Heap with largest value at root
        /// </summary>
        /// <param name="comparer"></param>
        public Heap(IComparer<T>? comparer = null) 
        {
            _mComparer = comparer ?? Comparer<T>.Default;
        }

        /**********************************************************************
         *  Public API
         **********************************************************************/

        /// <summary>
        /// Inserts an item into the heap
        /// </summary>
        /// <param name="value">value to insert</param>
        public void Insert(T value)
        {
            // Add item to the bottom
            _mItems.Add(value);

            // Swim it up to maintain heap condition
            swim(COUNT - 1);
        }

        /// <summary>
        /// Pops the root and repairs the heap
        /// </summary>
        /// <returns>The root of the heap</returns>
        /// <exception cref="InvalidOperationException">if the heap is empty</exception>
        public T Pop()
        {
            // Can't be empty
            if( EMPTY )
            {
                throw new InvalidOperationException("Heap is empty");
            }

            // Get the root
            var result = _mItems[0];

            // Remove the last item and put it at the root
            _mItems[0] = _mItems[COUNT - 1];
            _mItems.RemoveAt(COUNT - 1);

            // Sink the root to maintain heap condition
            sink(0);

            // Return the result
            return result;
        }

        /// <summary>
        /// Checks the heap and verifies it satisfies the heap condition
        /// </summary>
        public bool IsHeapified()
        {
            bool result = true;
            for (int i = 0; i < COUNT; i++)
            {
                if(!isaLeaf(i))
                {
                    int leftIdx = leftChildIndex(i);
                    if (isLess(leftIdx, i))
                    {
                        result = false;
                        break;
                    }

                    int rightIdx = rightChildIndex(i);  
                    if( rightIdx < COUNT && isLess(rightIdx, i))
                    {
                        result = false;
                        break;
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// Fixes the heap to restore the heap condition
        /// 
        /// If elements have static values that don't change, you 
        /// never need to call this method.  
        /// 
        /// Use this method if the value of one or more elements in the heap has changed, 
        /// and you need to re-organize the heap based on the new values
        /// </summary>
        public void ReHeapify()
        {
            // To re-heapify, we are going to sink the root of each binary tree in the heap
            // starting at the bottom
            for (int i = COUNT - 1; i >= 0; i--)
            {
                if (!isaLeaf(i))
                {
                    sink(i);
                }
            }
        }

        /// <summary>
        /// Returns a string of the heap elements in order
        /// </summary>
        /// <returns>string representation of heap elements</returns>
        public override string ToString()
        {
            return $"[{string.Join(", ", _mItems)}]";
        }

        /**********************************************************************
         *  Private helper methods
         **********************************************************************/

        /// <summary>
        /// Swims a node up the tree to maintain the heap condition
        /// </summary>
        /// <param name="index"></param>
        private void swim(int idx)
        {
            while(!isRoot(idx))
            {
                // Is node less than it's parent?
                int parentIdx = parentIndex(idx);
                if (isLess(idx, parentIdx))
                {
                    // Swap with the parent and keep swimming up
                    swap(idx, parentIdx);
                    idx = parentIdx;
                }
                else
                {
                    // Node is where it needs to be to satisfy heap condition
                    // so Stop
                    break;
                }
            }
        }

        /// <summary>
        /// Sinks a node down to maintain the heap condition
        /// </summary>
        private void sink(int idx)
        {
            while(!isaLeaf(idx))
            {
                // Is node bigger than the smallest child?
                int smallestChildIdx = smallestChildIndex(idx);
                if(isLess(smallestChildIdx, idx))
                {
                    // Swap with the smallest child and keep sinking down
                    swap(idx, smallestChildIdx);
                    idx = smallestChildIdx;
                }
                else
                {
                    // Node is where it needs to be to satisfy heap condition
                    // Stop
                    break;
                }

            }
        }

        /// <summary>
        /// Uses our comparator to determine if item[idx1] < item[idx2]
        /// </summary>
        /// <param name="idx1">index of item 1</param>
        /// <param name="idx2">index of item 2</param>
        /// <returns>true if item[idx1] < item[idx2]</returns>
        private bool isLess(int idx1, int idx2)
        {
            var item_1 = _mItems[idx1];
            var item_2 = _mItems[idx2];
            return _mComparer.Compare(item_1, item_2) < 0;
        }

        /// <summary>
        /// Swaps two items
        /// </summary>
        /// <param name="idx1">index of item 1</param>
        /// <param name="idx2">index of item 2</param>
        private void swap(int idx1, int idx2)
        {
            T temp = _mItems[idx1];
            _mItems[idx1] = _mItems[idx2];
            _mItems[idx2] = temp;
        }

        /// <summary>
        /// Returns true if index is the root
        /// </summary>
        /// <param name="i">an index</param>
        /// <returns>true if the root</returns>
        private bool isRoot(int i)
        {
            return i == 0;
        }

        /// <summary>
        /// Returns true if index is a leaf
        /// </summary>
        /// <param name="i">an index</param>
        /// <returns>true if a leaf</returns>
        private bool isaLeaf(int i)
        {
            return (leftChildIndex(i) > COUNT - 1);
        }

        /// <summary>
        /// Returns the parent index for node at index i.
        /// 
        /// If i=0 and is the root, this returns -1
        /// 
        /// </summary>
        /// <param name="i">an index</param>
        /// <returns>parent index</returns>
        private int parentIndex(int i)
        {
            return ((i + 1) / 2) - 1;
        }

        /// <summary>
        /// Returns the index of the left child for node at i
        /// </summary>
        /// <param name="i">an index</param>
        /// <returns>left child index</returns>
        private int leftChildIndex(int i)
        {
            return ((i + 1) * 2) - 1;
        }

        /// <summary>
        /// Returns the index of the right child for node at i
        /// </summary>
        /// <param name="i">an index</param>
        /// <returns>right child index</returns>
        private int rightChildIndex(int i)
        {
            return leftChildIndex(i) + 1;
        }

        /// <summary>
        /// Returns the index of the smallest child for node at i
        /// </summary>
        /// <param name="i">a non-leaf node</param>
        /// <returns>index of its smallest child</returns>
        /// <exception cref="InvalidOperationException">if i is a leaf</exception>
        private int smallestChildIndex(int i)
        {
            if (isaLeaf(i))
            {
                throw new InvalidOperationException("i is a leaf");
            }

            int leftIdx = leftChildIndex(i);
            int rightIdx = rightChildIndex(i);

            // Only one child -- return it
            if (rightIdx >= COUNT)
            {
                return leftIdx;
            }

            // Compare children
            else
            {
                return (isLess(leftIdx, rightIdx)) ? leftIdx : rightIdx;
            }
        }

        // My comparer
        private readonly IComparer<T> _mComparer;

        // My items
        private readonly List<T> _mItems = new();
    }
}
