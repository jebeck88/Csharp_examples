using System;
using System.Diagnostics;

namespace TaxiCab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            // The raw, computed, uncached taxi cab number calculator
            //var calculator = new IterativeTaxiCabCalculator();

            // The more performant version using caching
            var calculator = new CachedTaxiCabCalculator();

            Console.WriteLine($"Is 50 a taxicab number? {calculator.IsaTaxiCabNumber(50)}");
            Console.WriteLine($"Is 51 a taxicab number? {calculator.IsaTaxiCabNumber(51)}");

            Console.WriteLine($"Factor Pairs for 50: {string.Join<FactorPair>(", ", calculator.GetFactorPairs(50).ToArray())}");


            var stopWatch = Stopwatch.StartNew();

            int max = 10000;
            var taxiCabNumbers = calculator.GetAllTaxiCabNumbers(max);
            Console.WriteLine($"There are {taxiCabNumbers.Count} TaxiCab numbers < {max}:\n{string.Join("\n", taxiCabNumbers)}");

            stopWatch.Stop();

            Console.WriteLine($" > ElapsedTime: {stopWatch.ElapsedMilliseconds} mS");
        }
    }



}
