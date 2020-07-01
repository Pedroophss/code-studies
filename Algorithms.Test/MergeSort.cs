using System;
using System.Linq;
using Xunit;

namespace Algorithms.Test
{
    public class MergeSortTest
    {
        [Fact]
        public void SimplestSort()
        {
            int[] sortedArray = new int[] { 2, 3, 4, 5, 8, 9 };
            int[] unsortedArray = new int[] { 5, 9, 8, 4, 3, 2 };

            var mergeSortedArray = MergeSort.Sort(unsortedArray);

            Assert.Equal(sortedArray.Length, mergeSortedArray.Length);
            for (int i = 0; i < mergeSortedArray.Length; i++)
                Assert.Equal(sortedArray[i], mergeSortedArray[i]);
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
            var mergeSortedArray = MergeSort.Sort(unsortedArray);

            Assert.Equal(sortedArray.Length, mergeSortedArray.Length);
            for (int i = 0; i < mergeSortedArray.Length; i++)
                Assert.Equal(sortedArray[i], mergeSortedArray[i]);
        }
    }
}