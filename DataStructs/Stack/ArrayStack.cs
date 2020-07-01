using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructs.Stack
{
    public class ArrayStack
    {
        private int[] Stack { get; }
        private int Next { get; set; }

        public bool IsEmpty { get; private set; }

        public ArrayStack(int maxLenght)
        {
            Next = -1;
            IsEmpty = true;
            Stack = new int[maxLenght];
        }

        public int? Top => Next != -1 ? Stack[Next] : (int?)null;

        public void Push(int newValue)
        {
            if (++Next >= Stack.Length)
                throw new StackOverflowException();

            Stack[Next] = newValue;
        }

        public int Pop()
        {
            var aux = Next--;
            return Stack[aux];
        }
    }
}
