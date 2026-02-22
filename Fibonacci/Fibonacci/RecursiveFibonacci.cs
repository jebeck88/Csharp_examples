using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    /// <summary>
    /// A simple stateless implementation that uses a recursive method to calculate and return the ith fibonacci number
    /// </summary>
    public class RecursiveFibonacci : Fibonacci
    {

        public int Get(int i)
        {
            if( i == 0 )
            {
                return 0;
            }
            else if ( i == 1 )
            {
                return 1;
            }
            else
            {
                return Get(i - 1) + Get(i - 2);
            }
        }

        public int Find(int target)
        {
            // Check the argument
            if ( target < 0)
            {
                throw new ArgumentException($"No fibonacci number is <= {target}");
            }

            int index = 0;
            var nextFib = Get(index);
            int result = -1;
            while( nextFib <= target )
            {
                result = nextFib;
                nextFib = Get(++index);
            }

            return result;

        }
    }
}
