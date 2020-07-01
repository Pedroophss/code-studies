using System.Collections;

namespace DataStructs.Trees
{
    public class BinaryTree
    {
        public Node Root;
        private int NodesLenght;

        public class Node
        {
            public Node Left;
            public Node Right;
            public Node Parent;
            public readonly int Value;

            public Node(int value, Node parent)
            {
                Value = value;
                Parent = parent;
            }
        }

        public BinaryTree AddNode(int value)
        {
            AddValue(ref Root, null, value);
            return this;
        }

        private void AddValue(ref Node node, Node parent, int value)
        {
            if (node == null)
            {
                NodesLenght++;
                node = new Node(value, parent);
            }

            else if (value >  node.Value)
                AddValue(ref node.Right, node, value);

            else if (value < node.Value)
                AddValue(ref node.Left, node, value);
        }

        public void DeleteNode(int value)
        {
            var _node = FindNode(value);
            if (_node != null)
            {
                // Dont have any child
                if (_node.Left == null && _node.Right == null)
                {
                    // Is Root
                    if (_node.Parent == null)
                        Root = null;

                    else if (_node.Parent.Left == _node)
                        _node.Parent.Left = null;

                    else _node.Parent.Right = null;
                }

                // Has one child
                else if (_node.Left == null || _node.Right == null)
                {
                    var onlyChild = _node.Left ?? _node.Right;
                    if (_node.Parent == null)
                        Root = onlyChild;

                    else if (_node.Parent.Left == _node)
                        _node.Parent.Left = onlyChild;

                    else _node.Right = onlyChild;
                }

                // Has complete node
                else
                {
                    var leftestNode = _node.Right.Left;
                    while (leftestNode.Left != null)
                        leftestNode = leftestNode.Left;

                    leftestNode.Left = _node.Left;
                    leftestNode.Parent.Left = null;
                    leftestNode.Right = _node.Right;

                    if (_node.Parent == null)
                        Root = leftestNode;

                    else if (_node.Parent.Left == _node)
                        _node.Parent.Left = leftestNode;

                    else _node.Parent.Right = leftestNode;
                }
            }
        }

        public bool HasNode(int value)
        {
            var valueNode = FindNode(value);
            if (valueNode != null)
                return true;

            return false;
        }

        private Node FindNode(int value)
        {
            var current = Root;
            while (current != null)
                if (current.Value == value)
                    return current;

                else if (value > current.Value)
                    current = current.Right;

                else current = current.Left;

            return null;
        }

        public IEnumerable Print(TreeTraversalTypes mode)
        {
            var output = new ArrayList(NodesLenght);

            switch (mode)
            {
                default:
                case TreeTraversalTypes.Inorder:
                    PrintInorder(Root, output);
                    break;

                case TreeTraversalTypes.Preorder:
                    PrintPreorder(Root, output);
                    break;

                case TreeTraversalTypes.Postorder:
                    PrintPosorder(Root, output);
                    break;
            }

            return output;
        }

        private void PrintInorder(Node node, ArrayList writer)
        {
            if (node.Left != null)
                PrintInorder(node.Left, writer);

            writer.Add(node.Value);

            if (node.Right != null)
                PrintInorder(node.Right, writer);
        }

        private void PrintPreorder(Node node, ArrayList writer)
        {
            writer.Add(node.Value);

            if (node.Left != null)
                PrintPreorder(node.Left, writer);

            if (node.Right != null)
                PrintPreorder(node.Right, writer);
        }

        private void PrintPosorder(Node node, ArrayList writer)
        {
            if (node.Left != null)
                PrintPosorder(node.Left, writer);

            if (node.Right != null)
                PrintPosorder(node.Right, writer);

            writer.Add(node.Value);
        }
    }
}
