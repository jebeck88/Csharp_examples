using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    /// <summary>
    /// A simple value object to represent a pair of factors, and a value
    /// property to return the sum of their squares
    /// </summary>
    public class FactorPair
    {
        /// <summary>
        /// The first factor
        /// </summary>
        public int FactorOne { get; set; }

        /// <summary>
        /// The second factor
        /// </summary>
        public int FactorTwo { get; set; }

        /// <summary>
        /// Returns the sum of the squares of the factors
        /// </summary>
        public int SumOfSquares
        {
            get
            {
                return FactorOne * FactorOne + FactorTwo * FactorTwo;
            }
        }

        public override string ToString()
        {
            return $"({FactorOne}, {FactorTwo})";
        }
    }
}
