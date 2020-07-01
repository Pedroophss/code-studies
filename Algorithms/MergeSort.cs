using System.Linq;

namespace Algorithms
{
    public static class MergeSort
    {
        public static int[] Sort(int[] array)
        {
            if (array.Length == 1)
                return array;

            var mid = array.Length / 2;
            return Conquest(Sort(Range(array, 0, mid)), Sort(Range(array, mid, array.Length + 1)));
        }

        private static int[] Conquest(int[] left, int[] right)
        {
            int max = left.Length + right.Length, leftIndex = 0, rightIndex = 0;
            var mergedArray = new int[max];

            for (int i = 0; i < max; i++)
            {
                if (leftIndex == left.Length)
                    mergedArray[i] = right[rightIndex++];

                else if (rightIndex == right.Length)
                    mergedArray[i] = left[leftIndex++];

                else mergedArray[i] = left[leftIndex] <= right[rightIndex]
                    ? left[leftIndex++]
                    : right[rightIndex++];
            }
                

            return mergedArray;
        }

        private static int[] Range(int[] array, int start, int end) =>
            array.Where((w, index) => index < end && index >= start).ToArray();
    }
}