using LinkedLists;

namespace UnitTests
{
    internal class ListTestHelper
    {
        public static void AssertLength(IList l, int length)
        {
            Assert.Equal(l.Size, length);

            if (length > 0)
            {
                Assert.False(l.IsEmpty);
            }
            else
            {
                Assert.True(l.IsEmpty);
            }
        }

        public static void AssertEmpty(IList l)
        {
            AssertLength(l, 0);
        }

        static  void AssertElement(IList l, int index, string expectedValue)
        {
            var value = l.Get(index);

            Assert.Equal(value, expectedValue);
        }

        public static void TestAdd (IList list, int index, string value)
        {
            int initialSize = list.Size;

            list.Insert(index, value);

            AssertLength(list, initialSize + 1);
            AssertElement(list, index, value);
        }

        public static void TestAddFront(IList list, string value)
        {
            int initialSize = list.Size;

            list.AddFront(value);

            AssertLength(list, initialSize + 1);
            AssertElement(list, 0, value);
        }

        public static void TestAddBack(IList list, string value)
        {
            int initialSize = list.Size;

            list.AddBack(value);

            AssertLength(list, initialSize + 1);
            AssertElement(list, list.Size - 1, value);
        }

        public static void TestRemove(IList list, int index)
        {
            int initialSize = list.Size;

            bool hasNextValue = index < list.Size - 1;

            string value = list.Get(index);
            string nextValue = (hasNextValue) ? list.Get(index + 1) : "";

            list.Remove(index);

            AssertLength(list, initialSize - 1);

            if (hasNextValue)
            {
                AssertElement(list, index, nextValue);
            }
        }

        public static void TestRemoveFront(IList list)
        {
            int initialSize = list.Size;

            bool hasNext = initialSize > 1;
            string nextValue = hasNext ? list.Get(1) : "";

            list.RemoveFront();

            AssertLength(list, initialSize - 1);

            if (hasNext)
            {
                Assert.Equal(list.Get(0), nextValue);
            }
        }

        public static void TestRemoveBack(IList list)
        {
            int initialSize = list.Size;

            bool hasPrev = initialSize > 1;
            string prevValue = hasPrev ? list.Get(initialSize - 2) : "";

            list.RemoveBack();

            AssertLength(list, initialSize - 1);

            if (hasPrev)
            {
                Assert.Equal(list.Get(list.Size - 1), prevValue);
            }
        }

        public static void TestGet(IList list, int index, string expectedValue = "")
        {
            if (index < 0 || index > list.Size - 1)
            {
                Assert.Throws<IndexOutOfRangeException>(() => list.Get(index));
            }

            else
            {
                Assert.Equal(list.Get(index), expectedValue);
            }
        }

        public static void TestClear(IList list)
        {
            list.Clear();

            AssertEmpty(list);
        }


        public static void TestList(IList l)
        {
            // Make sure it's empty
            AssertEmpty(l);

            // Test AddFront
            TestAddFront(l, "c");
            TestAddFront(l, "b");
            TestAddFront(l, "a");
            TestAddFront(l, "aa");

            // Test AddBack
            TestAddBack(l, "x");
            TestAddBack(l, "y");
            TestAddBack(l, "z");
            TestAddBack(l, "zz");

            // Test add at front, back and middle
            TestAdd(l, 0, "front");
            TestAdd(l, l.Size, "back");
            TestAdd(l, 2, "index-2");

            // Test RemoveFront
            TestRemoveFront(l);
            TestRemoveFront(l);

            // Test removeBack
            TestRemoveBack(l);
            TestRemoveBack(l);

            // Test remove at front back and middle
            TestRemove(l, 0);
            TestRemove(l, 1);
            TestRemove(l, l.Size - 1);

            // Test get at front, back, middle and outside range
            TestGet(l, 0, "a");
            TestGet(l, l.Size - 1, "y");
            TestGet(l, 1000);
            TestGet(l, -1);

        }

        public static void CreateAndTestList<T>() where T : IList, new()
        {
            // Create a new list of type T
            var list = new T();

            // Test it
            TestList(list);
        }


    }

}
