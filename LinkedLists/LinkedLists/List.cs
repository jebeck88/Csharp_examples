using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    /// <summary>
    /// Public interface for a list
    /// </summary>
    public interface List
    {
        /// <summary>
        /// Adds an item to the front of the list
        /// </summary>
        /// <param name="item">the item to add</param>
        void AddFront(string item);

        /// <summary>
        /// Adds an item to the back of the list
        /// </summary>
        /// <param name="item">the item to add</param>
        void AddBack(string item);

        /// <summary>
        /// Inserts an item to the list at a given index
        /// </summary>
        /// <param name="index">the index</param>
        /// <param name="item">the item to add</param>
        /// <throws>Exception if the index is out of range</throws>
        void Insert(int index, string item);

        /// <summary>
        /// Removes the first item from the list, or does nothing if the list is empty
        /// </summary>
        void RemoveFront();

        /// <summary>
        /// Removes the last item from the list, or does nothing if the list is empty
        /// </summary>
        void RemoveBack();

        /// <summary>
        /// Remove item at a particular index
        /// </summary>
        /// <param name="index">index of the item to remove</param>
        /// <throws>Exception if index is out of range</throws>
        void Remove(int index);

        /// <summary>
        /// Clears the list
        /// </summary>
        void Clear();

        /// <summary>
        /// True if the list is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Length of the list
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets the item at a particular index
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>the item</returns>
        /// <throws>An exception if the index is out of bounds</throws>
        string Get(int index);
    }
}
