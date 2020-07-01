using System;
using Xunit;

namespace BookPractice
{
    public class BitManipulation
    {
        #region .:: Score ::.

        // 5.1 - Different, but good solution
        // 100%

        #endregion

        #region .:: 5.1 - Insertion ::.

        // You are given two 32-bit numbers, N and M, and two bit positions, i and
        // j.Write a method to insert M into N such that M starts at bit j and ends at bit i.You
        // can assume that the bits j through i have enough space to fit all of M. That is, if
        // M = 10011, you can assume that there are at least 5 bits between j and i.You would not, for
        // example, have j = 3 and i = 2, because M could not fully fit between bit 3 and bit 2.

        // EXAMPLE:
        // Input: N 10000000000, M 10011, i 2, j 6
        // Output: N = 10001001100 

        [Fact]
        public void Insertion()
        {
            // Arrange
            int i = 2, j = 6;
            int n = Convert.ToInt32("10000000000", 2), m = Convert.ToInt32("10011", 2);

            // Code
            int bit;
            int aux = j - i;

            for (; j >= i; j--, aux--)
            {
                bit = m & (1 << aux);
                if (bit == 0)
                     n &= ~(1 << j);
                else n |= 1 << j;
            }

            var result = Convert.ToString(n, 2);
        }

        #endregion

        #region .:: 5.3 Flip Bit to Win ::.

        // You have an integer and you can flip exactly one bit from a 0 to a 1. Write code to
        // find the length of the longest sequence of ls you could create.
        [Fact]
        public int FlipToWin()
        {
            // Arrange
            int value = 1775;

            int aux = 32;
            int bestValue = -1, zeroIndex = -1, startIndex = 0;
            while (--aux >= 0)
            {
                if ((value & (1 << aux)) != 0 && zeroIndex < startIndex)
                    startIndex = aux;
                
                else
                {
                    if (startIndex != 0)
                    {
                        var _value = startIndex - aux;
                        if (_value > bestValue)
                            bestValue = _value;
                    }

                    startIndex = zeroIndex + 1;
                    zeroIndex = aux;
                }
            }

            if (startIndex != 0)
            {
                var _value = startIndex - aux;
                if (_value > bestValue)
                    bestValue = _value;
            }

            return bestValue;
        }

        #endregion
    }
}
