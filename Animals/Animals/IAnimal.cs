using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public interface IAnimal
    {
        public string Name { get; set; }

        public string MakeSound();

        public string SayHello();
    }
}
