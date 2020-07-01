using DataStructs.HashTable;
using Xunit;

namespace DataStructs.test.HashTable
{
    public class MultipleArraysHashTableTest
    {
        [Fact]
        public void AddGetTest()
        {
            var table = new MultipleArraysHashTable<string, int>();

            table.Add("zero", 0);
            table.Add("one", 1);
            table.Add("two", 2);
            table.Add("three", 3);
            table.Add("four", 4);
            table.Add("five", 5);
            table.Add("six", 6);
            table.Add("seven", 7);
            table.Add("eight", 8);
            table.Add("nine", 9);

            Assert.Equal(0, table.Get("zero"));
            Assert.Equal(1, table.Get("one"));
            Assert.Equal(2, table.Get("two"));
            Assert.Equal(3, table.Get("three"));
            Assert.Equal(4, table.Get("four"));
            Assert.Equal(5, table.Get("five"));
            Assert.Equal(6, table.Get("six"));
            Assert.Equal(7, table.Get("seven"));
            Assert.Equal(8, table.Get("eight"));
            Assert.Equal(9, table.Get("nine"));
            Assert.Equal(0, table.Get("xablau"));
        }
    }
}
