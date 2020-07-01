using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace BookPractice
{
    public class StacksAndQueues
    {
        #region .:: Result ::.

        // Result: 60%
        // I am sucks

        // Score:
        // 3.1: Not Good: 30%
        // I answer correctly but it`s take a long time and i did know answer for one of the tips...

        // 3.2: blah 50%
        // I answer correctly but it`s take a considerable time, i needed to see all tips... 
        // and i keep think if i need solve that in a white board I probably couldn't 

        // 3.3: maybe 75%
        // I answer correctly and my code is very good, but on the follow up i answer wrong

        // 3.4: 100%
        // Perfect

        // 3.5: 80%
        // I created a class when a method was required but i think it`s okay

        // 3.6: 20%
        // My resolution was very poor!

        #endregion

        #region .:: 3.1 - Three in One ::.

        // Describe how you could use a single array to implement three stacks

        [Fact]
        public void ThreeInOne()
        {
            var counts = new int[3];
            var source = new int[30];

            void ValidateStack(int stackIndex)
            {
                if (--stackIndex > 2 || stackIndex < 0)
                    throw new Exception("Invalid Stack");
            }

            int Pop(int stackIndex)
            {
                ValidateStack(stackIndex);

                var stackBoundIndex = stackIndex - 1;
                ref int count = ref counts[stackBoundIndex];
                if (count == 0)
                    throw new Exception("Empty Stack");

                return source[--count * 3 + stackBoundIndex];
            }

            void Push(int stackIndex, int value)
            {
                ValidateStack(stackIndex);

                var stackBoundIndex = stackIndex - 1;
                ref int count = ref counts[stackBoundIndex];
                source[count++ * 3 + stackBoundIndex] = value;
            }

            int Peek(int stackIndex)
            {
                ValidateStack(stackIndex);

                var stackBoundIndex = stackIndex - 1;
                return source[counts[stackIndex - 1] * 2 + stackBoundIndex];
            }

            bool IsEmpty(int stackIndex)
            {
                ValidateStack(stackIndex);
                return counts[stackIndex - 1] == 0;
            }

            // Starts empty
            Assert.True(IsEmpty(1));
            Assert.True(IsEmpty(2));
            Assert.True(IsEmpty(3));

            // Add some values
            Push(1, 9); Push(2, 9); Push(3, 9);
            Push(1, 8); Push(2, 8); Push(3, 8);
            Push(1, 7); Push(2, 7); Push(3, 7);

            // Validate the top item
            Assert.Equal(7, Peek(1));
            Assert.Equal(7, Peek(2));
            Assert.Equal(7, Peek(3));

            // Validate the pop
            Assert.Equal(7, Pop(1)); Assert.Equal(7, Pop(2)); Assert.Equal(7, Pop(3));
            Assert.Equal(8, Pop(1)); Assert.Equal(8, Pop(2)); Assert.Equal(8, Pop(3));
            Assert.Equal(9, Pop(1)); Assert.Equal(9, Pop(2)); Assert.Equal(9, Pop(3));

            // Ends empty
            Assert.True(IsEmpty(1));
            Assert.True(IsEmpty(2));
            Assert.True(IsEmpty(3));
        }

        #endregion

        #region .:: 3.2 - Stack Min ::.

        // How would you design a stack which, in addition to push and pop, has a function min which returns the minimum element? 
        // Push, pop and min should all operate in 0(1) time.

        public void StackMin()
        {
            var minStack = new Stack<int>();
            var defaultStack = new Stack<int>();

            void Push(int value)
            {
                if (minStack.Count == 0)
                    minStack.Push(value);

                else if (minStack.Peek() > value)
                    minStack.Push(value);

                defaultStack.Push(value);
            }

            int Pop()
            {
                var aux = defaultStack.Pop();
                if (aux == minStack.Peek())
                    minStack.Pop();

                return aux;
            }

            void Min() => minStack.Peek();
        }

        #endregion

        #region .:: 3.3 - Stack of Plates ::.

        // Imagine a (literal) stack of plates. If the stack gets too high, it might topple. 
        // Therefore, in real life, we would likely start a new stack when the previous stack exceeds some threshold. 
        // Implement a data structure SetOfStacks that mimics this. SetO-fStacks should be composed of several stacks and 
        // should create a new stack once the previous one exceeds capacity. SetOfStacks. push() and SetOfStacks. 
        // pop() should behave identically to a single stack (that is, pop () should return the same values as it would if there were just a single stack). 

        // FOLLOW UP 
        // Implement a function popAt ( int index) which performs a pop operation on a specific sub-stack.

        public class SetOfStacks<T> where T : struct
        {
            private readonly int MaxByStack;
            private StackNode Head { get; set; }
            private StackNode Current { get; set; }

            public SetOfStacks(int maxByStack)
            {
                MaxByStack = maxByStack;
                Head = new StackNode(maxByStack);
                Current = Head;
            }

            private class StackNode
            {
                public StackNode Next { get; set; }
                public Stack<T> StackCore { get; set; }

                public StackNode(int max)
                {
                    StackCore = new Stack<T>(max);
                }
            }

            public void Push(T value)
            {
                Current.StackCore.Push(value);
                if (Current.StackCore.Count >= MaxByStack)
                {
                    if (Current != Head)
                        Current = Head;
                    else
                    {
                        var aux = Head;
                        Head = new StackNode(MaxByStack);
                        Head.Next = aux;
                        Current = Head;
                    }
                }
            }

            public T Pop()
            {
                if (Head.StackCore.Count == 0)
                    Head = Head.Next;

                var aux = Head.StackCore.Pop();
                return aux;
            }

            public T PopAt(int index)
            {
                var aux = 0;
                StackNode lastOne = null, target = Head;
                while (target != null && aux != index)
                {
                    lastOne = target;
                    target = target.Next;
                    aux++;
                }

                if (target == null)
                    throw new IndexOutOfRangeException();

                var output = target.StackCore.Pop();
                if (target.StackCore.Count == 0)
                    lastOne.Next = target.Next;

                else Current = target;
                return output;
            }
        }

        [Fact]
        public void SetOfStacks_Test()
        {
            var stack = new SetOfStacks<int>(2);

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            stack.Push(7);
            stack.Push(8);
            stack.Push(9);

            var qwe = stack.Pop();
            var zxc = stack.PopAt(2);
            stack.Push(15);
            stack.Push(16);
            var asd = stack.PopAt(2);
        }

        #endregion

        #region .:: 3.4 - Queue via Stacks ::.

        // Implement a MyQueue class which implements a queue using two stacks.

        public class Queue34<T>
        {
            private Stack<T> OrderedStack { get; }
            private Stack<T> AddStack { get; }

            public Queue34()
            {
                AddStack = new Stack<T>();
                OrderedStack = new Stack<T>();
            }

            public void Add(T item) =>
                AddStack.Push(item);

            public bool IsEmpty() =>
                AddStack.Count == 0 && OrderedStack.Count == 0;

            private void FillOrderedStack()
            {
                while (AddStack.Count > 0)
                    OrderedStack.Push(AddStack.Pop());
            }

            public T Peek()
            {
                if (OrderedStack.Count > 0)
                    return OrderedStack.Peek();

                else if (AddStack.Count > 0)
                {
                    FillOrderedStack();
                    return OrderedStack.Peek();
                }

                throw new Exception("Is Empty");
            }

            public T Remove()
            {
                if (OrderedStack.Count > 0)
                    return OrderedStack.Pop();

                else if (AddStack.Count > 0)
                {
                    FillOrderedStack();
                    return OrderedStack.Pop();
                }

                throw new Exception("Is Empty");
            }
        }

        [Fact]
        public void TestQueue34()
        {
            var queue = new Queue34<int>();

            queue.Add(3);
            queue.Add(4);
            queue.Add(5);

            Assert.Equal(3, queue.Remove());

            queue.Add(6);
            queue.Add(7);
            Assert.Equal(4, queue.Remove());

            queue.Add(8);
            Assert.Equal(5, queue.Remove());
            Assert.Equal(6, queue.Remove());
            Assert.Equal(7, queue.Remove());
            Assert.Equal(8, queue.Remove());
        }

        #endregion

        #region .:: 3.5 - Sort Stack ::.

        // Write a program to sort a stack such that the smallest items are on the top. 
        // You can use an additional temporary stack, but you may not copy the elements into any other data structure (such as an array). 
        // The stack supports the following operations: push, pop, peek, and is Empty. 

        public class SortStack35<T> where T : IComparable
        {
            public Stack<T> Aux { get; }
            public Stack<T> Sorted { get; }

            public SortStack35()
            {
                Aux = new Stack<T>();
                Sorted = new Stack<T>();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void GetBackFromAux()
            {
                while (Aux.Count > 0)
                    Sorted.Push(Aux.Pop());
            }

            public void Push(T value)
            {
                if (Sorted.Count == 0 || value.CompareTo(Sorted.Peek()) <= 0)
                    Sorted.Push(value);

                else
                {
                    do Aux.Push(Sorted.Pop());
                    while (Sorted.Count > 0 && value.CompareTo(Sorted.Peek()) > 0);

                    Sorted.Push(value);
                    GetBackFromAux();
                }
            }

            public T Pop() => Sorted.Pop();
            public T Peek() => Sorted.Peek();
            public bool IsEmpty() => Sorted.Count == 0;
        }

        [Fact]
        public void TestSortedStack35()
        {
            var sortedStack = new SortStack35<int>();

            sortedStack.Push(4);
            sortedStack.Push(7);
            sortedStack.Push(5);
            sortedStack.Push(8);
            sortedStack.Push(1);

            Assert.Equal(1, sortedStack.Pop());
            Assert.Equal(4, sortedStack.Pop());
            Assert.Equal(5, sortedStack.Pop());
            Assert.Equal(7, sortedStack.Pop());
            Assert.Equal(8, sortedStack.Pop());
        }

        #endregion

        #region .:: 3.6 - Animal Shelter ::.

        // An animal shelter, which holds only dogs and cats, operates on a strictly"first in, first out" basis.
        // People must adopt either the "oldest" (based on arrival time) of all animals at the shelter, or they can select whether they would prefer a dog or a cat (and will receive the oldest animal of that type).
        // They cannot select which specific animal they would like. Create the data structures to maintain this system and implement operations such as enqueue, dequeueAny, dequeueDog, and dequeueCat. 
        // You may use the built-in Linked list data structure.

        public enum AnimalType : byte
        {
            Dog = 1,
            Cat = 2
        }

        public class ShelterQueue
        {
            private ShelterNode Last { get; set; }
            private ShelterNode First { get; set; }

            private class ShelterNode
            {
                public AnimalType Type { get; }
                public string Name { get; set; }
                public ShelterNode Previous { get; set; }

                public ShelterNode(AnimalType type, string name)
                {
                    Type = type;
                    Name = name;
                }
            }

            public void Enqueue(AnimalType type, string name)
            {
                var node = new ShelterNode(type, name);
                if (First == null)
                    First = Last = node;
                else
                {
                    Last.Previous = node;
                    Last = node;
                }
            }

            public (string Name, AnimalType Type) DequeueAny()
            {
                var output = First;
                First = First.Previous;

                return (output.Name, output.Type);
            }

            private ShelterNode DequeueByType(AnimalType type)
            {
                ShelterNode current = First, before = null;
                while(current.Previous != null && current.Type != type)
                {
                    before = current;
                    current = current.Previous;
                }

                if (before == null)
                    First = First.Previous;
                else before.Previous = current.Previous;

                return current;
            }

            public string DequeueDog() =>
                DequeueByType(AnimalType.Dog)?.Name;

            public string DequeueCat() =>
                DequeueByType(AnimalType.Cat)?.Name;
        }

        [Fact]
        public void ShelterTest()
        {
            var queue = new ShelterQueue();

            queue.Enqueue(AnimalType.Dog, "Lola");
            queue.Enqueue(AnimalType.Dog, "Bere");
            queue.Enqueue(AnimalType.Cat, "Jose");
            queue.Enqueue(AnimalType.Cat, "Bongo");
            queue.Enqueue(AnimalType.Dog, "Joe");


            Assert.Equal("Lola", queue.DequeueAny().Name);
            Assert.Equal("Jose", queue.DequeueCat());
            Assert.Equal("Bere", queue.DequeueAny().Name);
        }

        #endregion
    }
}
