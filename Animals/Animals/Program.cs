using System;

namespace Animals
{
    public class TestProgram
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            // Make a dog the old fashioned way
            var myDog = new Dog("Bingo");
            Console.WriteLine($"{myDog.SayHello()}");

            // Get a handle to the animal shelter
            var shelter = AnimalShelter.Instance;

            // Donate some animals and dontate them to the shelter
            shelter.Donate(AnimalBreeder.Breed<Dog>("Dukie"));
            shelter.Donate(AnimalBreeder.Breed<Dog>("Princess"));
            shelter.Donate(AnimalBreeder.Breed<Dog>("Ginger"));
            shelter.Donate(AnimalBreeder.Breed<Pig>("Porky"));

            // Print the names of the animals in our shelter
            Console.WriteLine($"Names of animals: {string.Join(", ", shelter.Names<IAnimal>())}");
            Console.WriteLine($"Names of dogs: {string.Join(", ", shelter.Names<Dog>())}");
            Console.WriteLine($"Names of pigs: {string.Join(", ", shelter.Names<Pig>())}");

            // donate a duplicate name.  This will throw an exception...
            //shelter.Donate(AnimalBreeder.Breed<Dog>("Ginger"));

            // Borrow an animal from the shelter
            var ginger = shelter.Borrow("Ginger");
            Console.WriteLine($"{ginger.SayHello()}");

            // Return ginger to the shelter
            shelter.Return(ginger);

            // Adopt ginger from the shelter
            ginger = shelter.Adopt("Ginger");
            Console.WriteLine($"{ginger.SayHello()}");

            // Print the names of the animals in our shelter
            Console.WriteLine($"Names of animals: {string.Join(", ", shelter.Names<IAnimal>())}");
            Console.WriteLine($"Names of dogs: {string.Join(", ", shelter.Names<Dog>())}");
            Console.WriteLine($"Names of pigs: {string.Join(", ", shelter.Names<Pig>())}");

            // borrow an animal with a name that doesn't exist - throws an exception
            //shelter.borrow("Mickey");

            // borrow a dog with a name that corresponds with a pig -- throws an exception
            shelter.Borrow("Porky");

        }
    }
}
