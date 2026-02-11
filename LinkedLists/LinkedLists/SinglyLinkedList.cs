using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LinkedLists
{
    public class SinglyLinkedList : List
    {
        public bool IsEmpty => mHead == null;

        public int Size
        {
            get 
            {
                int result = 0;

                Node? node = mHead;
                while (node is not null)
                {
                    result++;
                    node = node.Next;
                }

                return result;
            } 
        }

        public void Insert(int index, string item)
        {
            // First check the index
            if (index < 0 || index > Size)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            }

            // If index is 0, just add front
            else if (index == 0)
            {
                AddFront(item);
            }

            // Otherwise, insert into the list
            else
            {
                // Find the parent
                var parent = mHead;
                int i = 0;
                while (parent != null && i < index - 1)
                {
                    parent = parent.Next;
                    ++i;
                }

                // Sanity check -- parent better not be null
                if (parent is null)
                {
                    throw new Exception("Unexpected error.  Parent node is null in Insert");
                }

                // Create the node and add after parent
                Node node = new Node(item, parent.Next);
                parent.Next = node;
            }
        }

        public void AddBack(string item)
        {
            // Add to the back by inserting at index size
            Insert(Size, item);
        }

        public void AddFront(string item)
        {
            var node = new Node(item, mHead);
            mHead = node;
        }

        public void Clear()
        {
            mHead = null;
        }

        public string Get(int index)
        {
            // Advance to index while node isn't null
            int i = 0;
            Node? node = mHead;
            while (i < index && node is not null)
            {
                node = node.Next;
                ++i;
            }

            // Return the value of throw an exception if out of range
            return node?.Value ?? throw new IndexOutOfRangeException($"Index {index} is out of range");
        }

        public void RemoveFront()
        {
            mHead = mHead?.Next;
        }

        public void RemoveBack()
        {
            if (!IsEmpty)
            {
                // If only one item, remove from front
                if (Size == 1)
                {
                    RemoveFront();
                }

                // Otherwise, remove item other than the first
                else
                {

                    // Otherwise, start at mHead and move forward while grand child is not null
                    Node? parent = mHead;
                    while (parent?.Next?.Next is not null)
                    {
                        parent = parent.Next;
                    }

                    // Sanity check -- parent better not be null
                    if (parent is null)
                    {
                        throw new Exception("Unexpected error.  Parent node is null in Insert");
                    }

                    // Ok, parent is now pointing at last child.  Remove it.
                    parent.Next = null;
                }
            }
        }

        public void Remove(int index)
        {
            // Check range
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            }

            // Remove front
            else if (index == 0)
            { 
                RemoveFront(); 
            }

            // Remove item in list
            else
            {
                // Advance parent to node before one being removed
                var parent = mHead;
                int i = 0;
                while (i < index - 1 && parent is not null)
                {
                    parent = parent.Next;
                    i++;
                }

                // Sanity check -- parent and child better not be null
                if (parent is null)
                {
                    throw new Exception("Unexpected error.  Parent node is null in Remove");
                }
                if (parent.Next is null)
                {
                    throw new Exception("Unexpected error.  Child node is null in Remove");
                }

                // Remove the node 
                parent.Next = parent.Next.Next;
            }
        }

        public override string ToString()
        {
            string result = "[";

            Node? node = mHead;
            while(node is not null)
            {
                result += node.Value;
                node = node.Next;
                if (node is not null)
                {
                    result += ", ";
                }
            }

            result += "]";
            return result;
        }

        /// <summary>
        /// Node in the linked list
        /// </summary>
        private class Node
        {
            public string Value;
            public Node? Next;

            public Node(string value, Node? next = null)
            {
                Value = value;
                Next = next;
            }
        }

        // Head of the list
        private Node? mHead = null;

        
    }
}
