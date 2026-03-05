using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    /// <summary>
    /// This is a cached implementation of the taxicab number calculator.
    /// 
    /// It maintains an internal cache, that maps a number to all factor pairs whose squares
    /// sum to that number
    /// 
    /// For example, in the map:
    /// 
    /// 50 --> {(1, 7), (5, 5)}
    /// 
    /// Looking up the factor pairs for 50 in the cache, we see there are exactly two pairs, 
    /// hence 50 is a taxi cab number.
    /// 
    /// </summary>
    public class CachedTaxiCabCalculator : TaxiCabCalculator
    {

        // @Override
        public int MaxFactor(int value)
        {
            return Convert.ToInt32(Math.Floor(Math.Sqrt(value - 1)));
        }

        // @Override
        public List<FactorPair> GetFactorPairs(int target)
        {
            // Compute max factor for target
            var maxFactor = MaxFactor(target);

            // Extend cache up to max factor
            ExtendCache(maxFactor);

            // Retrieve the factor pairs for target from the map
            List<FactorPair> result = new();
            if (_factorPairsMap.ContainsKey(target))
            {
                result = _factorPairsMap[target];
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

        /// <summary>
        /// Extends our cache to contain all factor pair combinations up to and including maxFactor
        /// </summary>
        /// <param name="maxFactor">the new max factor</param>
        private void ExtendCache(int maxFactor)
        {
            if(maxFactor > _maxFactor)
            {
                // First factor is in range: [1 to maxFactor]
                for (int i = 1; i <= maxFactor; ++i)
                {
                    // Second factor is in range: [startFactor, maxFactor]
                    // Where start factor starts at one beyond _maxFactor, but must also be > i
                    int startFactor = Math.Max(i, _maxFactor + 1);
                    for (int j = startFactor; j <= maxFactor; ++j)
                    {
                        // Create a factor pair
                        var pair = new FactorPair()
                        {
                            FactorOne = i,
                            FactorTwo = j
                        };

                        // Figure out it's value
                        int value = pair.SumOfSquares;

                        if( value == 50 )
                        {
                            Console.WriteLine("##### Found it!");
                        }

                        // Add that factor pair to the map and associate it with value.
                        if(!_factorPairsMap.ContainsKey(value))
                        {
                            _factorPairsMap[value] = new();
                        }
                        _factorPairsMap[value].Add(pair);
                    }
                }

                // Update the caches new max factor
                _maxFactor = maxFactor;
            }
        }

        /// <summary>
        /// The maxium factor in our factored pairs map
        /// </summary>
        private int _maxFactor = 0;

        /// <summary>
        /// This is our cache.  It maps:
        /// 
        /// value -> List<FactorPair>
        /// 
        /// IOW, it maps a value to a list of factor pairs, s.t. the sum of the square of each factor pair equals value.
        /// 
        /// Why is this useful?
        /// 
        /// Basically, given any number like 50, I can retrieve it's list of factor pairs in O(1) time.  
        /// Then, if that list of factor pairs has exactly two entries in the list, I know the number, like 50, is a taxicab number
        /// 
        /// </summary>

        private readonly Dictionary<int, List<FactorPair>> _factorPairsMap = new();
    }
}
