using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCourse
{
    public class MyLinkedList
    {
        #region .:: Node Class ::.

        // Elo
        // Node
        public class MyLinkedListNode
        {
            public int Value { get; set; }
            public MyLinkedListNode Next { get; set; }

            public MyLinkedListNode(int value)
            {
                Value = value;
                Next = null;
            }

            public void SetNextNode(MyLinkedListNode node)
            {
                Next = node;
            }
        }

        #endregion

        private MyLinkedListNode Head { get; set; }
        private MyLinkedListNode Tail { get; set; }

        public MyLinkedList()
        {
            Head = null;
            Tail = null;
        }

        public void Add(int newValue)
        {
            var newNode = new MyLinkedListNode(newValue);
            if (Head == null)
                Head = Tail = newNode;

            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }
        }

        public void DeleteLast()
        {
            MyLinkedListNode beforeTail = null;
            MyLinkedListNode current = Head;

            while(current.Next != null)
            {
                beforeTail = current;
                current = current.Next;
            }

            Tail = beforeTail;
        }

        public int SearchIndex(int indexValue)
        {
            var aux = 0;

            var current = Head;
            while (current.Next != null)
            {
                if (aux == indexValue)
                    break;

                aux++;
                current = current.Next;
            }

            return current.Value;
        }
    }
}
