using System;

namespace Heap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Create a heap
            var heap = new Heap<int>();
            Console.WriteLine($"Heap={heap}");

            // Do some inserts
            heap.Insert(2);
            heap.Insert(3);
            heap.Insert(-5);
            heap.Insert(0);
            heap.Insert(20);
            heap.Insert(3);

            // Print it
            Console.WriteLine($"Heap is heapified = {heap.IsHeapified()}, Heap={heap}");

            // Remove values, one at a time, to sort them
            List<int> sortedList = new();
            while(!heap.EMPTY)
            {
                sortedList.Add(heap.Pop());
                Console.WriteLine($"Heap is heapified = {heap.IsHeapified()}, Heap={heap}");
            }

            // Print the sorted list
            Console.WriteLine($"SortedList=[{string.Join(", ", sortedList)}]");
        }
    }
}
