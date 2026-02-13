using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{
    /// <summary>
    /// Simulates an auto parts builder
    /// 
    /// Contains both synchronous and asynchronous factory methods
    /// </summary>
    public static class PartBuilder
    {

        /**
         * Synchronous Methods
         */


        public static CarPart[] BuildParts(Order order)
        {
            Console.WriteLine($"Building the parts...");

            // Build all the parts sequentially, one at a time
            // blocking the caller until each one is done
            var engine = PartBuilder.Build<Engine>();

            var transmission = PartBuilder.Build<Transmission>();

            var chassis = PartBuilder.Build<Chassis>();

            var brakes = PartBuilder.Build<Brakes>();

            var entertainment = PartBuilder.Build<Entertainment>();

            return [engine, transmission, chassis, brakes, entertainment];
        }

        public static T Build<T>() where T : CarPart, new()
        {
            T part = new();
            
            // Print a console message
            Console.WriteLine($"Building a {part.Name} which takes {part.BuildTimeInMillis} ms...");

            // Synchronously wait for the operation to complete and block the caller
            Task.Delay(part.BuildTimeInMillis).Wait();

            Console.WriteLine($" > {part.Name} is done");

            // Return the built part
            return part;
        }


        ///////////////////////////////////////
        ///////////////////////////////////////

        /**
         * Asynchronous methods
         */

        public static async Task<CarPart[]> BuildPartsAsync(Order order)
        {
            Console.WriteLine($"Building the parts...");

            // Start each task asynchronously, spawn an async ask for each but don't block the caller
            var engineTask = PartBuilder.BuildAsync<Engine>();

            var transmissionTask = PartBuilder.BuildAsync<Transmission>();

            var chassisTask = PartBuilder.BuildAsync<Chassis>();

            var brakesTask = PartBuilder.BuildAsync<Brakes>();

            var entertainmentTask = PartBuilder.BuildAsync<Entertainment>();

            // Wait until all the part tasks are done
            Task.WaitAll(engineTask, transmissionTask, chassisTask, brakesTask, entertainmentTask);
            Console.WriteLine(" > all parts are done");

            return [engineTask.Result, transmissionTask.Result, chassisTask.Result,
                brakesTask.Result, entertainmentTask.Result];
        }

        public static async Task<T> BuildAsync<T>() where T : CarPart, new()
        {
            T part = new();

            // Print a console message
            Console.WriteLine($"Building a {part.Name} which takes {part.BuildTimeInMillis} ms...");

            // Synchronously wait for the operation to complete and block the caller
            await Task.Delay(part.BuildTimeInMillis);

            Console.WriteLine($" > {part.Name} is done");

            // Return the built part
            return part;
        }
    }
}
