using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// An animal breeder.  Essentially, a factory for animals.
    /// </summary>
    public static class AnimalBreeder
    {

        /// <summary>
        /// Create and return a new instance of T
        /// </summary>
        /// <typeparam name="T">A kind of animal</typeparam>
        /// <param name="name">the animal's name</param>
        /// <returns>the animal</returns>
        public static T Breed<T>(string name) where T : IAnimal, new()
        {
            var animal = new T()
            {
                Name = name
            };
            return animal;
        }
    }
}
