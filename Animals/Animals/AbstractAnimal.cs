using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public abstract class AbstractAnimal : IAnimal
    {
        public string Name { get; set; }

        public abstract string MakeSound();

        public AbstractAnimal(string name)
        {
            Name = name; 
        }

        public string SayHello()
        {
            return $"My name is \"{Name}\" and I say \"{MakeSound()}\".";
        }
    }
}
