using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    /// <summary>
    /// A stateful implementation,
    /// that creates and caches a fibonacci sequence in a list of integers 
    /// 
    /// When you get a fibonacci number, if the internal sequence isn't long enough it's 
    /// extended, and then the cached result in the internal sequence is returned. 
    /// </summary>
    public class CachedFibonacci : Fibonacci
    {
        public bool EMPTY {  get => _fibonacciSequence.Count == 0; }

        public int COUNT { get => _fibonacciSequence.Count; }

        public int Get(int i)
        {
            // If we don't have enough cached values in our sequence, extend it
            if(COUNT < i + 1)
            {
                Extend(i + 1);
            }

            // Return the value
            return _fibonacciSequence[i];
        }

        public int Find(int target)
        {
            // Extend our internal fibonacci seqeunce until the maximum value in the sequence is less than target
            while(EMPTY || _fibonacciSequence[COUNT -1] <  target)
            {
                Extend(COUNT + 1);
            }

            // Next, let's do a binary search to find our result
            return BinarySearch(target);
        }

        private int BinarySearch(int target)
        {
            // Check the range
            if (target < _fibonacciSequence[0] || target > _fibonacciSequence[COUNT - 1])
            {
                throw new ArgumentOutOfRangeException($"{target} does not exist in our fibonacci sequence");
            }

            int startIndex = 0;
            int endIndex = COUNT - 1;

            int result = -1;
            while (startIndex != endIndex)
            {
                int midIndex = startIndex + (endIndex - startIndex) / 2;

                if (_fibonacciSequence[midIndex] <= target && _fibonacciSequence[midIndex + 1] > target)
                {
                    result = _fibonacciSequence[midIndex];
                    break;
                }

                else if (_fibonacciSequence[midIndex] > target)
                {
                    endIndex = midIndex;
                }

                else
                {
                    startIndex = midIndex + 1;
                }
            }
            return result;

        }
        private void Extend(int i)
        {
            while(_fibonacciSequence.Count < i)
            {
                if (COUNT == 0)
                {
                    _fibonacciSequence.Add(0);
                }
                else if (COUNT == 1)
                {
                    _fibonacciSequence.Add(1);
                }
                else
                {
                    _fibonacciSequence.Add(_fibonacciSequence[COUNT-1] + _fibonacciSequence[COUNT-2]);
                }
            }
        }

        public List<int> _fibonacciSequence = new();
    }
}
