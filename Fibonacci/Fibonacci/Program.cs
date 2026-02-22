using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace Fibonacci
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Instantiate a fibonacci calculator

            Fibonacci fib = new RecursiveFibonacci();
            //Fibonacci fib = new CachedFibonacci();


            // Calculate some fibonacci numbers
            var stopWatch = Stopwatch.StartNew();

            Console.WriteLine($"The 0th fibonacci number is {fib.Get(0)}");
            Console.WriteLine($"The 1st fibonacci number is {fib.Get(1)}");
            Console.WriteLine($"The 39th fibonacci number is {fib.Get(39)}");
            Console.WriteLine($"The 41th fibonacci number is {fib.Get(41)}");

            Console.WriteLine($"Find Fibonacci number <= 0: {fib.Find(0)}");
            Console.WriteLine($"Find Fibonacci number <= 1: {fib.Find(1)}");
            Console.WriteLine($"Find Fibonacci number <= 100: {fib.Find(100)}");
            Console.WriteLine($"Find Fibonacci number <= 1000: {fib.Find(1000)}");
            Console.WriteLine($"Find Fibonacci number <= 10000: {fib.Find(10000)}");

            Console.WriteLine($" > ElapsedTime: {stopWatch.ElapsedMilliseconds} mS");

        }
    }
}