using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms.Test
{
    public class BinarySearchTest
    {
        [Fact]
        public void SimpleSearch()
        {
            var searchList = new SortedSet<int>();
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < random.Next(50, 100); i++)
                searchList.Add(random.Next(int.MaxValue));

            var requestPosition = random.Next(0, searchList.Count);
            var responsePosition = BinarySearch.Search(searchList.ToArray(), searchList.ElementAt(requestPosition));

            Assert.Equal(requestPosition, responsePosition.position);
        }

        [Fact]
        public void NotFoundSearch()
        {
            var searchList = new SortedSet<int>();
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < random.Next(50, 100); i++)
                searchList.Add(random.Next(int.MaxValue));

            int searchValue;
            do searchValue = random.Next(int.MaxValue);
            while (searchList.Contains(searchValue));

            var (position, jumps) = BinarySearch.Search(searchList.ToArray(), searchValue);
            Assert.Equal(-1, position);
        }
    }
}
