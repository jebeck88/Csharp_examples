using System;
using System.Diagnostics;

namespace AsyncExample
{
    public class AsyncExample
    {
        //public static void Main(string[] args)
        //{
        //    // Stopwatch
        //    var stopWatch = Stopwatch.StartNew();

        //    // Take an order
        //    var order = Order.TakeOrder("Kia", "Sorento");
        //    Console.WriteLine($" > order is done: {order}");

        //    // Build the parts
        //    var parts = PartBuilder.BuildParts(order);
        //    string partsList = string.Join<CarPart>(", ", parts);
        //    Console.WriteLine($" > parts are done: [{partsList}]");

        //    // Assemble the car
        //    var car = FinalAssembler.Assemble(order, parts);
        //    Console.WriteLine($" > car is done: {car}.  ElapsedTime: {stopWatch.ElapsedMilliseconds} mS");
        //}

        public static async Task Main(string[] args)
        {
            // Stopwatch
            var stopWatch = Stopwatch.StartNew();

            // Take an order
            var order = Order.TakeOrder("Kia", "Sorento");
            Console.WriteLine($" > order is done: {order}");

            // Build the parts concurrently and wait until they're done
            var partsTask = PartBuilder.BuildPartsAsync(order);
            await partsTask; 
            string partsList = string.Join<CarPart>(", ", partsTask.Result);
            Console.WriteLine($" > parts are done: [{partsList}]");

            // Assemble the car
            var assembleTask = FinalAssembler.AssembleAsync(order, partsTask.Result);
            await assembleTask;
            Console.WriteLine($" > car is done: {assembleTask.Result}.  ElapsedTime: {stopWatch.ElapsedMilliseconds} mS");
        }
    }
}