using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{
    /// <summary>
    /// Simple value object that represents a car order
    /// </summary>
    public class Order
    {
        public string Make { get; }
        public string Model { get; }

        public static Order TakeOrder(string make, string model)
        {
            Console.WriteLine($"Taking the order...");
            return new Order(make, model);
        }

        public Order(string make, string model)
        {
            Model = model;
            Make = make;
        }

        public override string ToString()
        {
            return $"[Order for a \"{Make}, {Model}\"]";
        }
    }
}
