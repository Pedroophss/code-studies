using DataStructs.Trees;
using System;
using System.Linq;
using Xunit;

namespace DataStructs.test
{
    public class SimpleTreeTest
    {
        [Fact]
        public void AddNode()
        {
            var tree = new SimpleTree<int>(3);
            Enumerable.Range(1, 100).ToList().ForEach(s => tree.AddNode(s));
        }

        [Fact]
        public void PrintPostOrder()
        {
            var tree = new SimpleTree<int>(2);
            Enumerable.Range(1, 5).ToList().ForEach(s => tree.AddNode(s));

            var output = tree.PrintPostorder();
        }

        [Fact]
        public void PrintPreOrder()
        {
            var tree = new SimpleTree<int>(2);
            Enumerable.Range(1, 5).ToList().ForEach(s => tree.AddNode(s));

            var output = tree.PrintPreorder();
        }
    }
}
