namespace Algorithms
{
    public static class QuickSort
    {
        public static int[] Sort(int[] array) =>
            Conquer(array, 0, array.Length);

        private static int[] Conquer(int[] array, int start, int end)
        {
            if (start != end)
            {
                var pivotIndex = Divide(array, start, end);
                Conquer(array, start, pivotIndex);
                Conquer(array, pivotIndex +1, end);
            }

            return array;
        }

        private static int Divide(int[] array, int start, int end)
        {
            int maxIndex = end -1, pivot = array[maxIndex], swapIndex = start;
            for (int i = start; i < maxIndex; i++)
            {
                if (array[i] < pivot)
                    array.Swap(swapIndex++, i);
            }

            array.Swap(maxIndex, swapIndex);
            return swapIndex;
        }

        private static void Swap(this int[] array, int originIndex, int targetIndex)
        {
            var aux = array[originIndex];
            array[originIndex] = array[targetIndex];
            array[targetIndex] = aux;
        }
    }
}
