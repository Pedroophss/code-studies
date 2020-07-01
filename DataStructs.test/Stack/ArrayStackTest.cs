using DataStructs.Stack;
using System;
using Xunit;

namespace DataStructs.test.Stack
{
    public class ArrayStackTest
    {
        [Fact]
        public void PushPopTest()
        {
            var stack = new ArrayStack(10);

            Assert.True(stack.IsEmpty);
            Assert.False(stack.Top.HasValue);

            stack.Push(1);
            stack.Push(5);
            stack.Push(6);
            stack.Push(7);

            Assert.Equal(7, stack.Top);
            Assert.Equal(7, stack.Pop());
            Assert.Equal(6, stack.Pop());
            Assert.Equal(5, stack.Pop());

            Assert.Equal(1, stack.Top);
            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void MaxLength()
        {
            var stack = new ArrayStack(3);

            Assert.True(stack.IsEmpty);
            Assert.False(stack.Top.HasValue);

            stack.Push(1);
            stack.Push(5);
            stack.Push(3);
            Assert.Throws<StackOverflowException>(() => stack.Push(6));
        }
    }
}
