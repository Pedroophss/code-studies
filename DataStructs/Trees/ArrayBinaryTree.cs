using System;
using System.Linq;

namespace DataStructs.Trees
{
    public class ArrayBinaryTree
    {
        private const int ROOT_INDEX = 0;

        public struct Node
        {
            public readonly int Value;
            public readonly int Index;
            public readonly bool IsValid;
            public readonly int LeftIndex;
            public readonly int RightIndex;
            public readonly int? ParentIndex;

            public Node(int value, int index, int? parentIndex)
            {
                Value = value;
                Index = index;
                IsValid = true;
                ParentIndex = parentIndex;

                var aux = index * 2;
                LeftIndex = aux + 1;
                RightIndex = aux + 2;
            }

            public override string ToString() =>
                $"{IsValid.ToString()} - {Value.ToString()}";
        }

        private int MaxHeigth { get; set; }
        private Node[] TreeNodes { get; set; }

        public ArrayBinaryTree(int heigth = 4)
        {
            MaxHeigth = heigth;
            TreeNodes = new Node[(int)Math.Pow(heigth - 1, 2)];
        }

        public void AddNode(int value) =>
            AddNode(value, null, ROOT_INDEX);

        private void AddNode(int value, int? parentIndex, int nodeIndex)
        {
            if (nodeIndex >= TreeNodes.Length)
                IncreaseTreeHeigth();

            ref var currentNode = ref TreeNodes[nodeIndex];
            if (!currentNode.IsValid)
                currentNode = new Node(value, nodeIndex, parentIndex);

            else if (value > currentNode.Value)
                AddNode(value, nodeIndex, currentNode.RightIndex);

            else if (value < currentNode.Value)
                AddNode(value, nodeIndex, currentNode.LeftIndex);
        }

        private void IncreaseTreeHeigth()
        {
            var aux = new Node[(int)Math.Pow(++MaxHeigth - 1, 2)];
            for (int i = 0; i < TreeNodes.Length; i++)
                aux[i] = TreeNodes[i];

            TreeNodes = aux;
        }

        public int[] Read(TreeTraversalTypes type)
        {
            var startArrayIndex = -1;
            switch (type)
            {
                default:
                case TreeTraversalTypes.Inorder: return ReadInorder(new int[TreeNodes.Length], ROOT_INDEX, ref startArrayIndex);
                case TreeTraversalTypes.Preorder: return ReadPreOrder(new int[TreeNodes.Length], ROOT_INDEX, ref startArrayIndex);
                case TreeTraversalTypes.Postorder: return ReadPostOrder(new int[TreeNodes.Length], ROOT_INDEX, ref startArrayIndex);
            }
        }

        private int[] ReadPostOrder(int[] postOrder, int index, ref int nextInorderValue)
        {
            if (index > postOrder.Length)
                return postOrder;

            ref var currentNode = ref TreeNodes[index];
            if (!currentNode.IsValid)
                return postOrder;


            if (currentNode.LeftIndex != 0)
                ReadPostOrder(postOrder, currentNode.LeftIndex, ref nextInorderValue);

            if (currentNode.RightIndex != 0)
                ReadPostOrder(postOrder, currentNode.RightIndex, ref nextInorderValue);

            postOrder[++nextInorderValue] = currentNode.Value;

            return postOrder;
        }

        private int[] ReadPreOrder(int[] preOrder, int index, ref int nextInorderValue)
        {
            if (index >= preOrder.Length)
                return preOrder;

            ref var currentNode = ref TreeNodes[index];
            if (!currentNode.IsValid)
                return preOrder;

            preOrder[++nextInorderValue] = currentNode.Value;

            if (currentNode.LeftIndex != 0)
                ReadPreOrder(preOrder, currentNode.LeftIndex, ref nextInorderValue);

            if (currentNode.RightIndex != 0)
                ReadPreOrder(preOrder, currentNode.RightIndex, ref nextInorderValue);

            return preOrder;
        }

        private int[] ReadInorder(int[] inorder, int index, ref int nextInorderValue)
        {
            if (index >= inorder.Length)
                return inorder;

            ref var currentNode = ref TreeNodes[index];
            if (!currentNode.IsValid)
                return inorder;

            if (currentNode.LeftIndex != 0)
                ReadInorder(inorder, currentNode.LeftIndex, ref nextInorderValue);

            inorder[++nextInorderValue] = currentNode.Value;

            if (currentNode.RightIndex != 0)
                ReadInorder(inorder, currentNode.RightIndex, ref nextInorderValue);

            return inorder;
        }

        public bool Find(int value) =>
            Find(value, ROOT_INDEX).IsValid;

        private Node Find(int value, int nodeIndex)
        {
            if (nodeIndex >= TreeNodes.Length)
                return default;

            ref var currentNode = ref TreeNodes[nodeIndex];
            if (value > currentNode.Value)
                return Find(value, currentNode.RightIndex);

            else if (value < currentNode.Value)
                return Find(value, currentNode.LeftIndex);

            else return currentNode;
        }

        public void DeleteNode(int value)
        {
            var node = Find(value, ROOT_INDEX);
            if (node.IsValid)
            {
                // Is leaf? so just delete it
                if (!TreeNodes[node.LeftIndex].IsValid && !TreeNodes[node.RightIndex].IsValid)
                    TreeNodes[node.Index] = default;

                // Has two childs.. so put the next inorder child on node place
                else if (TreeNodes[node.LeftIndex].IsValid && TreeNodes[node.RightIndex].IsValid)
                {
                    // Get inorder child
                    var inorderNode = TreeNodes[node.RightIndex];
                    while (inorderNode.LeftIndex < TreeNodes.Length && TreeNodes[inorderNode.LeftIndex].IsValid)
                        inorderNode = TreeNodes[inorderNode.LeftIndex];

                    // Clean inorder space
                    TreeNodes[inorderNode.Index] = default;

                    // Switch to deleted node place
                    TreeNodes[node.Index] = new Node(inorderNode.Value, node.Index, node.ParentIndex);
                }

                // Has one child.. so put child on parent place
                else
                {
                    var theNode = TreeNodes[node.LeftIndex].IsValid
                        ? TreeNodes[node.LeftIndex]
                        : TreeNodes[node.RightIndex];

                    TreeNodes[theNode.Index] = default;
                    TreeNodes[node.Index] = new Node(theNode.Value, node.Index, node.Index);
                }
            }
        }
    }
}