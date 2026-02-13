using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{
    // A final assembler, that knows how to make a car from an array of parts
    public static class FinalAssembler
    {
        // Assmebly time per part, which is 500ms
        public static int MS_PER_PART = 500;

        public static Car Assemble(Order order, CarPart[] parts)
        {
            // Calculate the assembly time in ms
            int assemblyTime = MS_PER_PART * parts.Length;

            // Print to console
            Console.WriteLine($"Assembling an {order}, which takes {assemblyTime}ms...");

            // Assemble it...
            Task.Delay( assemblyTime );

            // Assemble and return the car
            var result = new Car(order.Make, order.Model, parts);
            return result;
        }

        public static async Task<Car> AssembleAsync(Order order, CarPart[] parts)
        {
            // Calculate the assembly time in ms
            int assemblyTime = MS_PER_PART * parts.Length;

            // Print to console
            Console.WriteLine($"Assembling an {order}, which takes {assemblyTime}ms...");

            // Assemble it...
            await Task.Delay(assemblyTime);

            // Assemble and return the car
            var result = new Car(order.Make, order.Model, parts);
            return result;
        }
    }
}
