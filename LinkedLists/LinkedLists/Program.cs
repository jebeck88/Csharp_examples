using System;
using System.Collections.Generic;

namespace LinkedLists
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var list = new SinglyLinkedList();
            var list = new DoublyLinkedList();
            TestList(list);
        }

        private static void TestList(IList list)
        {
            // Print empty list
            Console.WriteLine($"Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // Add a couple values to the front
            list.AddFront("c");
            Console.WriteLine($"AddFront(\"c\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.AddFront("b");
            Console.WriteLine($"AddFront(\"b\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.AddFront("a");
            Console.WriteLine($"AddFront(\"a\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.AddFront("aa");
            Console.WriteLine($"AddFront(\"aa\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // Add a couple values to the back
            list.AddBack("x");
            Console.WriteLine($"AddBack(\"x\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.AddBack("y");
            Console.WriteLine($"AddBack(\"y\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.AddBack("z");
            Console.WriteLine($"AddBack(\"z\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.AddBack("zz");
            Console.WriteLine($"AddBack(\"zz\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // Add a value to index 0
            list.Insert(0, "front");
            Console.WriteLine($"Insert(0, \"front\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // insert a value to index <size>
            list.Insert(list.Size, "back");
            Console.WriteLine($"Insert(list.Size, \"back\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // insert a value to index 2
            list.Insert(2, "index-2");
            Console.WriteLine($"Insert(2, \"index-2\")\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // insert a value to an invalid index
            try
            {
                list.Insert(1000, "Bad Insert");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Insert(1000, \"Bad Insert\")Caught expected excpetion inserting to invalid index: {e.Message}");
            }

            // Remove front
            list.RemoveFront();
            Console.WriteLine($"RemoveFront()\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.RemoveFront();
            Console.WriteLine($"RemoveFront()\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // Remove back
            list.RemoveBack();
            Console.WriteLine($"RemoveBack()\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
            list.RemoveBack();
            Console.WriteLine($"RemoveBack()\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // Remove index=1
            list.Remove(1);
            Console.WriteLine($"list.Remove(1)\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");

            // Get 0
            Console.WriteLine($"list.Get(0) = {list.Get(0)}");

            // Get 5
            Console.WriteLine($"list.Get(5) = {list.Get(5)}");

            // Get 6
            try
            {
                list.Get(6);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"list.Get(6)Caught expected excpetion getting non-existent element: {e.Message}");
            }

            // Clear the list
            list.Clear();
            Console.WriteLine($"list.Clear()\n  Size = {list.Size} isEmpty = {list.IsEmpty} list={list}");
        }
    }
}
