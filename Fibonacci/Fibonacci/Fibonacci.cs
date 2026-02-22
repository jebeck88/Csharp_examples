using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    public interface Fibonacci
    {
        /// <summary>
        /// Returns the ith fibonacci number
        /// </summary>
        /// <param name="i">the index of the fibonacci number to return</param>
        /// <returns>the ith fibonacci nuumber</returns>
        int Get(int index);
    }
}
