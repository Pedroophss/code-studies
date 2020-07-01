using DataStructs.Queue;
using Xunit;

namespace DataStructs.test.Queue
{
    public class QueueTest
    {
        [Fact]
        public void SimpleTest()
        {
            var queue = new LinkedListQueue();

            Assert.True(queue.IsEmpty);

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            Assert.Equal(1, queue.Front);
            Assert.Equal(4, queue.Rear);

            Assert.Equal(1, queue.Dequeue());
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(3, queue.Dequeue());
            Assert.Equal(4, queue.Dequeue());
        }
    }
}