using System;
using System.Collections.Generic;
using Xunit;

namespace BookPractice
{
    public class LinkedLists
    {
        // Score: (4/8) => 50%

        #region .:: My LinkedList ::.

        public class MyLinkedList<T>
        {
            public int Count { get; set; }
            public MyLinkedListNode<T> Head { get; set; }
            public MyLinkedListNode<T> Tail { get; set; }

            public MyLinkedList() { }
            public MyLinkedList(T[] init)
            {
                for (int i = 0; i < init.Length; i++)
                    this.AddNode(init[i]);
            }

            public MyLinkedListNode<T> this[int index]
            {
                get
                {
                    var aux = -1;
                    var current = Head;
                    while (current != null)
                    {
                        if (++aux == index)
                            return current;

                        current = current.Next;
                    }

                    return null;
                }
            }

            public void AddNode(T value)
            {
                if (Head == null)
                    Tail = Head = new MyLinkedListNode<T>(value);
                else
                {
                    var aux = new MyLinkedListNode<T>(value);
                    Tail.Next = aux;
                    Tail = aux;
                }

                Count++;
            }

            public void AddNode(MyLinkedListNode<T> node)
            {
                if (Head == null)
                    Tail = Head = node;
                else
                {
                    Tail.Next = node;
                    Tail = node;
                }

                Count++;
            }

            public void Clear()
            {
                Head = Tail = null;
                Count = 0;
            }
        }

        public class MyLinkedListNode<T>
        {
            public T Value { get; set; }
            public MyLinkedListNode<T> Next { get; set; }

            public MyLinkedListNode(T value) =>
                Value = value;
        }

        #endregion

        #region .:: 2.1 - Remove Dups ::.

        // Remove Dups! Write code to remove duplicates from an unsorted linked list.
        // How would you solve this problem if a temporary buffer is not allowed?

        [Fact]
        public void RemoveDups()
        {
            var list = new LinkedList<int>(new int[] { 1, 5, 8, 7, 5, 8, 6, 2, 3, 1, 5 });

            // Space Complexity O(N)
            // Time Complexity O(N)

            var node = list.First;
            var table = new HashSet<int>(list.Count);

            do
            {
                if (table.Contains(node.Value))
                {
                    var aux = node;
                    node = node.Next;
                    list.Remove(aux);
                }
                else
                {
                    table.Add(node.Value);
                    node = node.Next;
                }
            }
            while (node != null);
            // Because the .NET linked list is a cycle linked list
        }

        // How would you solve this problem if a temporary buffer is not allowed?

        [Fact]
        public void RemoveDups_WithoutBuffer()
        {
            var list = new LinkedList<int>(new int[] { 1, 5, 8, 7, 5, 8, 6, 2, 3, 1, 5 });

            // Space Complexity O(1)
            // Time Complexity <= O(N^2)

            var node = list.First;
            while (node != null)
            {
                var runner = node.Next;
                while (runner != null)
                    if (runner.Value == node.Value)
                    {
                        var aux = runner;
                        runner = runner.Next;
                        list.Remove(aux);
                    }
                    else runner = runner.Next;


                node = node.Next;
            }
        }

        #endregion

        #region .:: 2.2 - Return Kth to Last ::.

        // Return Kth to Last: Implement an algorithm to find the kth to last element of a singly linked list.

        [Theory]
        [InlineData(3, 3)]
        public void GetKthLastNode(int kth, int expected)
        {
            var list = new LinkedList<int>(new int[] { 1, 5, 8, 7, 5, 8, 6, 2, 3, 1, 5 });

            // Space Complexity O(1)
            // Time Complexity O(N + kth) ~= O(N)

            var kthNode = list.First;
            for (int i = 1; i < kth; i++)
            {
                if (kthNode == null)
                    Assert.Equal(expected, -1);

                kthNode = kthNode.Next;
            }

            var node = list.First;
            while (kthNode.Next != null)
            {
                node = node.Next;
                kthNode = kthNode.Next;
            }

            Assert.Equal(expected, node.Value);
        }

        #endregion

        #region .:: 2.4 - Partition ::.

        // Partition: Write code to partition a linked list around a value x, such that all nodes less than x come before all nodes greater than or equal to x.
        // If xis contained within the list, the values of x only need to be after the elements less than x (see below). 
        // The partition element x can appear anywhere in the "right partition"; it does not need to appear between the left and right partitions. 

        // Input: 3 -> 5 -> 8 -> 5 -> 10 -> 2 -> 1 [partition= 5]
        // Output: 3 -> 1 -> 2 -> 10 -> 5 -> 5 -> 8

        [Fact]
        public void Partition()
        {
            var partition = 5;
            var list = new MyLinkedList<int>(new int[] { 3, 5, 8, 5, 10, 2, 1 });

            // Time Complexity: O(N)
            // Space Complexity: O(1)

            var currentNode = list.Head;

            list.Clear();
            var biggerList = new MyLinkedList<int>();

            while (currentNode != null)
            {
                if (currentNode.Value >= partition)
                    biggerList.AddNode(currentNode.Value);
                else list.AddNode(currentNode.Value);

                currentNode = currentNode.Next;
            }

            list.Tail.Next = biggerList.Head;
        }

        #endregion

        #region .:: 2.5 - Sum Lists ::.

        // You have two numbers represented by a linked list, where each node contains a single digit. 
        // The digits are stored in reverse order, such that the 1 's digit is at the head of the list.
        // Write a function that adds the two numbers and returns the sum as a linked list.

        // EXAMPLE 
        // Input: (7-> 1 -> 6) + (5 -> 9 -> 2). That is,617 + 295. 
        // Output: 2 -> 1 -> 9. That is, 912. 

        [Fact]
        public void SumLists()
        {
            var list1 = new LinkedList<int>(new int[] { 7, 1, 6 });
            var list2 = new LinkedList<int>(new int[] { 5, 9, 2 });

            // Time Complexity: O(3N) ~= O(N)
            // Space Complexity: O(N)

            int GetNumberFromList(LinkedList<int> list)
            {
                var aux = 0;
                var output = 0;
                var current = list.First;
                while (current != null)
                {
                    output += (int)(current.Value * Math.Pow(10, aux++));
                    current = current.Next;
                }

                return output;
            }

            LinkedList<int> GetListFromNumber(int number)
            {
                var output = new LinkedList<int>();

                do
                {
                    output.AddLast(number % 10);
                    number = number / 10;
                }
                while (number != 0);
                return output;
            }

            var n1 = GetNumberFromList(list1);
            var n2 = GetNumberFromList(list2);
            var expected = GetListFromNumber(n1 + n2);
        }

        // Result: NOT PASS!

        #endregion

        #region .:: 2.6 - Palindrome ::.

        // Palindrome: Implement a function to check if a linked list is a palindrome.

        // [ERROR]
        // it`s the second time that i mess the palindrome concept with permutation concept
        // i did the permutation algorithm

        #region => Error :/ 

        [Fact]
        public void IsPalindrome_Error()
        {
            var list1 = new LinkedList<int>(new int[] { 7, 1, 6, 5, 8, 9, 6, 1 });
            var list2 = new LinkedList<int>(new int[] { 6, 1, 7, 8, 9, 5, 6, 1 });

            // Time Complexity: O(2N) ~= O(N)
            // Space Complexity: O(N)

            if (list1.Count != list2.Count)
                Assert.True(false);

            var current = list1.First;
            var hashTable = new Dictionary<int, byte>();

            while (current != null)
            {
                if (hashTable.ContainsKey(current.Value))
                    hashTable[current.Value]++;
                else hashTable[current.Value] = 1;

                current = current.Next;
            }

            current = list2.First;
            while (current != null)
            {
                if (hashTable.ContainsKey(current.Value))
                {
                    var key = --hashTable[current.Value];
                    if (key == 0)
                        hashTable.Remove(current.Value);
                }
                current = current.Next;
            }

            Assert.Equal(0, hashTable.Count);
        }

        #endregion

        [Fact]
        public void IsPalidrome()
        {
            var list1 = new LinkedList<int>(new int[] { 0, 1, 2, 1, 0 });

            // Time Complexity: O(2N) ~= O(N)
            // Space Complexity: O(N)

            var current = list1.First;
            var checker = new Stack<int>();

            while (current != null)
            {
                checker.Push(current.Value);
                current = current.Next;
            }

            current = list1.First;
            while (current != null)
            {
                if (current.Value != checker.Pop())
                    Assert.True(false);

                current = current.Next;
            }
        }

        [Fact]
        public void IsPalindrome_Runner()
        {
            var list1 = new LinkedList<int>(new int[] { 0, 1, 3, 2, 3, 1, 0 });

            // Time Complexity: O(N)*
            // Space Complexity: O(N/2) ~= O(N)

            var checker = new Stack<int>();
            LinkedListNode<int> slow = list1.First, fast = list1.First;

            while (fast != null && fast.Next != null)
            {
                checker.Push(slow.Value);
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            if (fast != null)
                slow = slow.Next;

            while (slow != null)
            {
                if (slow.Value != checker.Pop())
                    Assert.True(false);

                slow = slow.Next;
            }
        }

        #endregion

        #region .:: 2.7 - Intersection ::.

        // Given two (singly) linked lists, determine if the two lists intersect. 
        // Return the intersecting node. Note that the intersection is defined based on reference, not value. 
        // That is, if the kth node of the first linked list is the exact same node (by reference) as the jth node of the second linked list, then they are intersecting.

        [Fact]
        public void GetIntersection()
        {
            var intersectNode1 = new MyLinkedListNode<int>(4);
            var intersectNode2 = new MyLinkedListNode<int>(7);

            var list1 = new MyLinkedList<int>(new int[] { 7, 1, 6 });
            list1.AddNode(intersectNode1);
            list1.AddNode(intersectNode2);

            var list2 = new MyLinkedList<int>(new int[] { 8, 5, 5, 9, 2 });
            list2.AddNode(intersectNode1);
            list2.AddNode(intersectNode2);

            // Space Complexity: O(1);
            // Time Complexity: O(3M + N) ~= O(N);

            (int count, MyLinkedListNode<int> last) GetLastNodeAndCount(MyLinkedList<int> list) =>
                (list.Count, list.Tail);

            var nodeCount_1 = GetLastNodeAndCount(list1);
            var nodeCount_2 = GetLastNodeAndCount(list2);

            if (nodeCount_1.last != nodeCount_2.last)
                Assert.True(false);

            var lists = nodeCount_1.count > nodeCount_2.count
                ? (major: list1, minor: list2)
                : (major: list2, minor: list1);

            var aux = lists.major.Count;
            var currentMajor = lists.major.Head;
            var currentMinor = lists.minor.Head;

            while (currentMajor != null && currentMinor != null)
            {
                if (currentMajor == currentMinor)
                {
                    Assert.Equal(currentMinor, intersectNode1);
                    break;
                }

                aux--;
                currentMajor = currentMajor.Next;
                if (aux < lists.minor.Count)
                    currentMinor = currentMinor.Next;
            }

            Assert.True(false);
        }

        #endregion

        #region .:: 2.8 Loop Detection ::.

        // Given a circular linked list, implement an algorithm that returns the node at the beginning of the loop. 

        [Fact]
        public void HasLoop()
        {
            // Arrange...
            var list = new MyLinkedList<int>(new int[] { 8, 5, 5, 9, 2, 7, 1 });
            var circleNode = list[3];
            list.AddNode(circleNode);

            // Space Complexity O(N)
            // Time Complexity O(N)

            var hashTable = new HashSet<int>();
            var current = list.Head;

            while (current != null)
            {
                var nodeId = current.GetHashCode();
                if (hashTable.Contains(nodeId))
                {
                    Assert.True(true);
                    break;
                }

                hashTable.Add(nodeId);
                current = current.Next;
            }

            Assert.True(false);
        }

        // The book anwser

        [Fact]
        public void HasLoop_Book()
        {
            // Arrange...
            var list = new MyLinkedList<int>(new int[] { 8, 5, 5, 9, 2, 7, 1 });
            var circleNode = list[3];
            list.AddNode(circleNode);

            // Space Complexity O(N)
            // Time Complexity O(1)

            var slow = list.Head;
            var fast = list.Head;

            // Find a loop
            while (fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;

                if (fast == slow)
                    break;
            }

            if (fast.Next == null)
                Assert.True(false);

            slow = list.Head;
            while (slow != fast)
            {
                slow = slow.Next;
                fast = fast.Next;
            }
        }

        #endregion
    }
}
