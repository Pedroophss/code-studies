using DataStructs.Trees;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace DataStructs.test.Trees
{
    public class BinaryTreeTest
    {
        [Fact]
        public void AddNodes()
        {
            var output = new List<object>();
            var binaryTree = new BinaryTree();

            binaryTree.AddNode(50);
            binaryTree.AddNode(25);
            binaryTree.AddNode(75);
            binaryTree.AddNode(12);
            binaryTree.AddNode(30);
            binaryTree.AddNode(60);
            binaryTree.AddNode(85);
            binaryTree.AddNode(52);
            binaryTree.AddNode(70);

            void print()
            {
                output.Clear();
                foreach (var item in binaryTree.Print(TreeTraversalTypes.Inorder))
                    output.Add(item);
            }

            print();

            binaryTree.DeleteNode(50);

            print();

            binaryTree.DeleteNode(30);

            print();

            binaryTree.DeleteNode(50);

            print();

            //foreach (var item in binaryTree.Print(TreeTraversalTypes.Preorder))
            //    Debug.Write(item);

            //foreach (var item in binaryTree.Print(TreeTraversalTypes.Postorder))
            //    Debug.Write(item);

            //var has = binaryTree.HasNode(15);
            //has = binaryTree.HasNode(45);
        }

        [Fact]
        public void AddNodes_Array()
        {
            int[] output;
            var binaryTree = new ArrayBinaryTree();

            binaryTree.AddNode(50);
            binaryTree.AddNode(30);
            binaryTree.AddNode(70);
            binaryTree.AddNode(20);
            binaryTree.AddNode(40);
            binaryTree.AddNode(60);
            binaryTree.AddNode(80);
            //binaryTree.AddNode(52);
            //binaryTree.AddNode(70);

            void print() =>
                output = binaryTree.Read(TreeTraversalTypes.Preorder);

            print();

            binaryTree.DeleteNode(20);

            print();

            binaryTree.DeleteNode(30);

            print();

            binaryTree.DeleteNode(50);

            print();
        }
    }
}