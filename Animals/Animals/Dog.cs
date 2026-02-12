using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Dog : AbstractAnimal, IAnimal
    {
        public Dog() : base("")
        {

        }

        public Dog(string name) : base(name)
        { 
        }

        public override string MakeSound()
        {
            return "Woof, woof!";
        }
    }
}
