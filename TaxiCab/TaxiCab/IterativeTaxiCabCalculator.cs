using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    /// <summary>
    /// This is an iterative implementation of the taxicab number calculator with no caching.
    /// </summary>
    public class IterativeTaxiCabCalculator : TaxiCabCalculator
    {

        // @Override
        public int MaxFactor(int value)
        {
            return Convert.ToInt32(Math.Floor(Math.Sqrt(value - 1)));
        }

        // @Override
        public List<FactorPair> GetFactorPairs(int target)
        {
            List<FactorPair> result = new();

            // Min factor is 1
            int minFactor = 1;

            // Max Factor is much less than target.
            int maxFactor = MaxFactor(target);

            // First factor is in range: [minFactor, maxFactor]
            for(int i = minFactor; i <= maxFactor; ++i )
            {
                // Second factor is in range: [factorOne, maxFactor]
                for( int j = i; j <= maxFactor; ++j )
                {
                    // Create a factor pair
                    var factors = new FactorPair()
                    {
                        FactorOne = i,
                        FactorTwo = j
                    };

                    // Does the sum of their squares equal target?
                    if (factors.SumOfSquares == target)
                    {
                        result.Add(factors);
                        break;
                    }
                }
            }
            return result;
        }

        // @Override
        public bool IsaTaxiCabNumber(int value)
        {
            var factors = GetFactorPairs(value);
            return factors.Count == 2;
        }

        public List<TaxiCabNumber> GetAllTaxiCabNumbers(int max)
        {
            List<TaxiCabNumber> result = new();

            for(int i = 1; i < max; ++i)
            {
                var pairs = GetFactorPairs(i);
                if(pairs.Count == 2)
                {
                    result.Add(new TaxiCabNumber(i, pairs));
                }
            }

            return result;
        }
    }
}
