using LinkedLists;

namespace UnitTests
{
    public class LinkedListUnitTests
    {
        [Fact]
        public void TestSinglyLinkedList()
        {
            ListTestHelper.CreateAndTestList<SinglyLinkedList>();
        }

        [Fact]
        public void TestDoublyLinkedList()
        {
            ListTestHelper.CreateAndTestList<DoublyLinkedList>();
        }
    }
}