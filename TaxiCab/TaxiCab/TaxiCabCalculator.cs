using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    /// <summary>
    /// A public interface for performing operations on taxicab numbers
    /// 
    /// A taxicab number is an integer that can be expressed as the sum of two factors squared,
    /// for exactly two sets of factors.
    /// 
    /// 50 = (1^2 + 7^2) = (5^2 + 5^2) is a taxicab number
    /// </summary>
    public interface TaxiCabCalculator
    {
        /// <summary>
        ///  Computes the max sum-of-squares factor for value
        ///  
        ///  For example, if value=50, the maxFactor is sqrt(50-1)
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the max sum of square factor for value</returns>
        int MaxFactor(int value);

        /// <summary>
        /// Returns true if value is a taxicab number
        /// </summary>
        /// <param name="value">the value to check</param>
        /// <returns>true if value is a taxicab number</returns>
        bool IsaTaxiCabNumber(int value);

        /// <summary>
        /// Returns a list of all factor pairs, whose sum of squares equals value
        /// 
        /// Note that a taxicab number will return a list of length 2.
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the list of square factors for value</returns>
        List<FactorPair> GetFactorPairs(int value);

        /// <summary>
        /// Returns a list of all taxicab numbers less than max
        /// </summary>
        /// <param name="max">the maximum value</param>
        /// <returns>list of all taxicab numbers</returns>
        List<TaxiCabNumber> GetAllTaxiCabNumbers(int max);



    }
}
