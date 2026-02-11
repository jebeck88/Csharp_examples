using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LinkedLists
{
    public class DoublyLinkedList : IList
    {
        public bool IsEmpty => (mHead is null);

        public int Size
        {
            get
            {
                int result = 0;
                var node = mHead;
                while( node is not null)
                {
                    node = node.Next;
                    ++result;
                }
                return result;
            }
        }

        public void AddBack(string item)
        {
            var node = new Node(item, null, mTail);

            mTail = node;
            if(node.Prev is not null)
            {
                node.Prev.Next = node; 
            }

            if (mHead is null)
            {
                mHead = node;
            }
        }

        public void AddFront(string item)
        {
            Node node = new Node(item, mHead, null);
            mHead = node;

            if(node.Next is not null)
            {
                node.Next.Prev = node;
            }

            if (mTail is null)
            {
                mTail = node;
            }
        }

        public void Clear()
        {
            mHead = mTail = null;
        }

        public string Get(int index)
        {
            // Check the range
            if( index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            // Advance to index while node is not null
            int i = 0;
            var node = mHead;
            while(i < index && node is not null)
            {
                node = node.Next;
                ++i;
            }

            // Return the value of throw an exception if out of range
            return node?.Value ?? throw new IndexOutOfRangeException($"Index {index} is out of range");
        }

        public void Insert(int index, string item)
        {
            // Checck the range
            if ( index < 0 || index > Size)
            {
                throw new IndexOutOfRangeException();
            }

            // Add Front
            else if ( index == 0 )
            {
                AddFront(item);
            }

            // Add Back
            else if ( index == Size )
            {
                AddBack(item);
            }

            // Add Middle
            else
            {
                // Advance to parent node
                int i = 0;
                Node? parent = mHead;
                while (i < index - 1 && parent is not null)
                {
                    parent = parent.Next;
                    ++i;
                }

                // Sanity check -- parent better not be null
                if (parent is null)
                {
                    throw new Exception("Internal Error.  parent is null");
                }

                // Create new node and insert after parent
                var node = new Node(item, parent.Next, parent);
                if( parent.Next?.Prev is not null )
                {
                    parent.Next.Prev = node;
                }
                parent.Next = node;
            }
        }

        public void Remove(int index)
        {
            // Check the index
            if (index < 0 || index > Size - 1)
            {
                throw new IndexOutOfRangeException();
            }

            // Remove front
            else if (index == 0)
            {
                RemoveFront();
            }

            // Remove back
            else if (index == Size - 1)
            {
                RemoveBack();
            }

            // Remove middle
            else
            {
                // Find the parent and the successor
                var parent = mHead;
                int i = 0;
                while( i < index - 1 && parent is not null)
                {
                    parent = parent.Next;
                    ++i;
                }

                // Sanity check - parent better not be null
                if (parent is null)
                {
                    throw new Exception("Internal error. Parent is null");
                }

                // Get a handle to the successor
                Node? successor = parent.Next?.Next;

                // Remove node following parent
                parent.Next = successor;
                if(successor is not null)
                {
                    successor.Prev = parent;
                }
            }
        }

        public void RemoveBack()
        {
            // If the list is empty or has only one item, clear it.
            if (Size < 2)
            {
                Clear();
            }

            // Otherwise remove the last node
            {
                mTail = mTail?.Prev;
                mTail!.Next = null;
            }
        }

        public void RemoveFront()
        {
            // If the list is empty or has only one item, clear it.
            if(Size < 2)
            {
                Clear();
            }

            // Otherwise remove the first node
            else
            {
                mHead = mHead?.Next;
                mHead!.Prev = null;
            }
        }

        public override string ToString()
        {
            string result = "[";

            Node? node = mHead;
            while (node is not null)
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

        // Private Node class
        private class Node
        {
            public string Value;
            public Node? Next;
            public Node? Prev;

            public Node(string value, Node? next = null, Node? prev = null)
            {
                Value = value;
                Next = next;
                Prev = prev;
            }
        }

        // The head
        private Node? mHead = null;

        // The tail
        private Node? mTail = null;
    }
}