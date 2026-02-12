using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Pig : AbstractAnimal, IAnimal
    {
        public Pig() : base("")
        {
        }

        public Pig(string name) : base(name)
        {
        }

        public override string MakeSound()
        {
            return "Oink, oink";
        }
    }
}
