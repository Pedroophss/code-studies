using System;
using System.Linq;
using Xunit;

namespace Algorithms.Test
{
    public class QuickSortTest
    {
        [Fact]
        public void SimplestSort()
        {
            int[] sortedArray = new int[] { 2, 3, 4, 5, 8, 9 };
            int[] unsortedArray = new int[] { 5, 9, 8, 4, 3, 2 };

            var mySortedArray = QuickSort.Sort(unsortedArray);

            Assert.Equal(sortedArray.Length, mySortedArray.Length);
            for (int i = 0; i < mySortedArray.Length; i++)
                Assert.Equal(sortedArray[i], mySortedArray[i]);
        }

        [Fact]
        public void SimpleSort()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var arraySize = random.Next(1_000, 1_000_000);

            int[] unsortedArray = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
                unsortedArray[i] = random.Next(1000_000);

            int[] sortedArray = unsortedArray.OrderBy(o => o).ToArray();
            var mySortedArray = QuickSort.Sort(unsortedArray);

            Assert.Equal(sortedArray.Length, mySortedArray.Length);
            for (int i = 0; i < mySortedArray.Length; i++)
                Assert.Equal(sortedArray[i], mySortedArray[i]);
        }
    }
}
