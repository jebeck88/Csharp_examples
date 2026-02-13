using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{
    /// <summary>
    /// A simple value object representing a Car
    /// </summary>
    public class Car
    {
        public string Make { get; }
        public string Model { get; }

        public CarPart[] Parts { get; }
        public Car(string make, string model, CarPart[] parts)
        {
            Make = make;
            Model = model;
            Parts = parts;
        }

        public override string ToString()
        {
            string partsList = string.Join<CarPart>(", ", Parts);
            string result = $"Make: \"{Make}\" Model: \"{Model}\" Parts: [ {partsList} ]";
            return result;
        }
    }
}
