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
    }
}
