using System;

namespace ConsoleApplication
{
    // A silly play program just to make sure my local envrionment is set up correctly. 
    class Program
    {
        static void Main(String[] args)
        {
            const int meaningOfLife = 42;
            Console.WriteLine($"Hello world! The meaning of life is {meaningOfLife}");

            Console.Write("How old are yout? ");
            var ageStr = Console.ReadLine();
            int age = Convert.ToInt32(ageStr);

            if (age < 18)
            {
                Console.WriteLine("You are a child.");
            }
            else if (age < 21)
            {
                Console.WriteLine("You can vote.");
            }
            else
            {
                Console.WriteLine("You are an adult.");
            }

            string[] vowels = { "a", "e", "i", "o", "u" };

            foreach (var vowel in vowels)
            {
                Console.WriteLine($"\"{vowel}\" is a vowel.");
            }
        }
    }

}