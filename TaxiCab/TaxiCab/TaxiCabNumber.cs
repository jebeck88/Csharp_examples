using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    /// <summary>
    /// A simple value object representing a taxicab number
    /// </summary>
    public class TaxiCabNumber
    {
        /// <summary>
        /// The taxicab number
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// The factor pairs 
        /// </summary>
        public List<FactorPair> FactorPairs { get; }

        public TaxiCabNumber(int value, List<FactorPair> factorPairs)
        {
            if (factorPairs.Count != 2)
            {
                throw new ArgumentException($"Invalid number of factors: {factorPairs.Count}");
            }

            foreach (FactorPair pair in factorPairs)
            {
                if (pair.SumOfSquares != value)
                {
                    throw new ArgumentException($"Invalid factor pair {pair}");
                }
            }

            Value = value;
            FactorPairs = factorPairs;
        }

        public override string ToString()
        {
            return $"{Value}: [{string.Join(", ", FactorPairs)}]";
        }
    }
}
