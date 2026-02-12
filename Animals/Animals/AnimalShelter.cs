using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Animals
{
    /// <summary>
    /// Animal shelter.  
    /// 
    /// Think of this as a repo for animals.
    /// 
    /// Implemented as a singleton.
    /// 
    /// Our animal shelter is a little weird.  We require all animals in our shelter to have a unique name. 
    /// Only one "Molly" or "Duke" please.  
    /// </summary>
    public class AnimalShelter
    {

        /// <summary>
        /// Property holding the singleton instance
        /// </summary>
        public static AnimalShelter Instance
        {
            get
            {
                return sInstancee;
            }
        }

        /// <summary>
        /// Returns the names of the anmials in our shelter of a given type, in alphabetical order
        /// </summary>
        /// <typeparam name="T">the type of animal</typeparam>
        /// <returns>array of names</returns>
        public string[] Names<T>() where T : IAnimal
        {
            lock (mLock)
            {
                List<string> result = new();
                foreach (var name in mAnimals.Keys)
                {
                    var animal = mAnimals[name];
                    if (animal is T)
                    {
                        result.Add(animal.Name);
                    }
                }
                result.Sort();
                return result.ToArray();
            }
        }

        /// <summary>
        /// Donate an animal to the shelter
        /// </summary>
        /// <param name="animal">the animal to donate</param>
        /// <exception cref="ArgumentException">If an animal of this name already exists</exception>
        public void Donate(IAnimal animal)
        {
            lock (mLock)
            {

                if (animal is null)
                {
                    throw new ArgumentNullException();
                }
                else if (string.IsNullOrEmpty(animal.Name))
                {
                    throw new ArgumentException("Your animal has no name!");
                }
                else if (mAnimals.ContainsKey(animal.Name))
                {
                    throw new ArgumentException($"Our shelter already has an animal named \"{animal.Name}\".");
                }

                mAnimals.Add(animal.Name, animal);
            }
        }

        /// <summary>
        /// Borrow an animal from the shelter
        /// </summary>
        /// <param name="name">name of the animal to borrow</param>
        /// <returns>the animal</returns>
        /// <exception cref="ArgumentException">if the animal doesn't exist or can't be borrowed</exception>
        public IAnimal Borrow(string name)
        {
            lock (mLock)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Name is null or empty.");
                }
                else if (!mAnimals.ContainsKey(name))
                {
                    throw new ArgumentException($"No animal named {name} lives at our shelter.");
                }
                else if (mBorrowedAnimals.Contains(name))
                {
                    throw new ArgumentException($"Sorry, {name} is out on a date!");
                }

                // Get a handle to the animal
                var animal = mAnimals[name];

                // Add it to the borrowed set
                mBorrowedAnimals.Add(animal.Name);

                // Return it
                return animal;
            }
        }

        /// <summary>
        /// Returns true if an animal with this name lives at the shelter
        /// and is available to be borrwed or adopted
        /// </summary>
        /// <param name="name">the animals name</param>
        /// <returns>true if available</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsAvailable(string name)
        {
            lock (mLock)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Name is null or empty.");
                }

                return (mAnimals.ContainsKey(name) && !mBorrowedAnimals.Contains(name));
            }
        }

        /// <summary>
        /// Return a borrowe animal to the shelter
        /// </summary>
        /// <param name="animal">the animal being returned</param>
        /// <exception cref="ArgumentNullException">if the animal doesn't exist</exception>
        /// <exception cref="ArgumentException">if the animal doesn't live here or wasn't borrowed</exception>
        public void Return(IAnimal animal)
        {
            lock (mLock) 
            {
                if (animal is null)
                {
                    throw new ArgumentNullException();
                }
                else if (string.IsNullOrEmpty(animal.Name))
                {
                    throw new ArgumentException("Your animal has no name!");
                }
                else if (!mAnimals.ContainsKey(animal.Name))
                {
                    throw new ArgumentException($"Your animal named \"{animal.Name}\" doesn't live at our shelter.");
                }
                else if (!mBorrowedAnimals.Contains(animal.Name))
                {
                    throw new ArgumentException($"{animal.Name} is already here and wasn't borrowed");
                }

                // Return the animal
                mBorrowedAnimals.Remove(animal.Name);
             }
        }

        /// <summary>
        /// Adopt an animal from the shelter
        /// </summary>
        /// <param name="name">name of animal to adopt</param>
        /// <returns>the animal</returns>
        /// <exception cref="ArgumentException">if the animal doesn't live here or can't be adopted</exception>
        public IAnimal Adopt(string name)
        {
            lock (mLock)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Name is null or empty.");
                }
                else if (!mAnimals.ContainsKey(name))
                {
                    throw new ArgumentException($"No animal named {name} lives at our shelter.");
                }
                else if (mBorrowedAnimals.Contains(name))
                {
                    throw new ArgumentException($"Sorry, {name} is out on a date!");
                }

                // Remove the animal from our roster and return it
                var animal = mAnimals[name];
                mAnimals.Remove(name);

                return animal;
            }
        }



        /// <summary>
        /// Private constructor.  We are a singleton
        /// </summary>
        private AnimalShelter() { }

        // The singleton instance, created in a thread-safe way without locking
        private static readonly AnimalShelter sInstancee= new();

        // lock
        private object mLock = new();

        // Private dictionary of animals in our shelter, name->animal
        private Dictionary<string, IAnimal> mAnimals = new();

        // Set of animal names that have been borrowed.
        private HashSet<string> mBorrowedAnimals = new();
    }
}
