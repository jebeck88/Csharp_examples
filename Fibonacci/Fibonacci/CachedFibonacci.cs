using System;
using System.Collections.Generic;
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

        public int Get(int i)
        {
            // If we don't have enough cached values in our sequence, extend it
            if(_fibonacciSequence.Count < i + 1)
            {
                Extend(i + 1);
            }

            // Return the value
            return _fibonacciSequence[i];
        }

        private void Extend(int i)
        {
            while(_fibonacciSequence.Count < i)
            {
                if (_fibonacciSequence.Count == 0)
                {
                    _fibonacciSequence.Add(0);
                }
                else if (_fibonacciSequence.Count == 1)
                {
                    _fibonacciSequence.Add(1);
                }
                else
                {
                    var count = _fibonacciSequence.Count;
                    _fibonacciSequence.Add(_fibonacciSequence[count-1] + _fibonacciSequence[count-2]);
                }
            }
        }

        public List<int> _fibonacciSequence = new();
    }
}
