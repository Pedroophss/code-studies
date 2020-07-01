namespace Algorithms
{
    public class BinarySearch
    {
        public static (int position, int jumps) Search(int[] array, int requestNumber)
        {
            int jumps = 0, min = 0, max = array.GetUpperBound(0);
            
            while(min <= max)
            {
                jumps++;
                var mid = min + (max - min) / 2;
                var midNumber = array[mid];

                if (midNumber == requestNumber)
                    return (mid, jumps);

                else if (midNumber > requestNumber)
                    max = mid - 1;

                else min = mid + 1;
            }

            return (-1, jumps);
        }
    }
}
