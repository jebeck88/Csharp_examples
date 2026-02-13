using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExample
{

    // Abstract base class for a car part
    public class CarPart
    {
        // Name of the part
        public virtual string Name { get; } = "";

        // Time required to build the part in milliseconds
        public virtual int BuildTimeInMillis { get; } = 10;

        public override string ToString()
        {
            return Name;
        }
    }

    public class Engine : CarPart
    {
        public override string Name { get; } = "Engine";

        public override int BuildTimeInMillis { get; } = 1000;
    }

    public class Transmission : CarPart
    {
        public override string Name { get; } = "Transmission";

        public override int BuildTimeInMillis { get; } = 1500;
    }

    public class Brakes : CarPart
    {
        public override string Name { get; } = "Brakes";

        public override int BuildTimeInMillis { get; } = 750;
    }

    public class Chassis : CarPart
    {
        public override string Name { get; } = "Chassis";

        public override int BuildTimeInMillis { get; } = 1250;
    }

    public class Entertainment : CarPart
    {
        public override string Name { get; } = "Entertainment";

        public override int BuildTimeInMillis { get; } = 2000;
    }




}
