using System.Text;

namespace DataStructs.Trees
{
    public class SimpleTree<T>
    {
        public class Node
        {
            public T Value { get; }
            public Node Parent { get; set; }
            public Node[] Childrens { get; }
            private int NumberOfLiveChildrens { get; set; }

            public bool IsComplete =>
                NumberOfLiveChildrens == Childrens.Length;

            public Node(T value, int lenght)
            {
                Value = value;
                NumberOfLiveChildrens = 0;
                Childrens = new Node[lenght];
            }

            public void AddChild(T value) =>
                Childrens[NumberOfLiveChildrens++] = new Node(value, Childrens.Length) { Parent = this };

            public override string ToString() =>
                Value.ToString();
        }


        public Node Root;
        int NodeChildsLength;

        public SimpleTree(int treeChildLenght)
        {
            NodeChildsLength = treeChildLenght;
        }

        public SimpleTree<T> AddNode(T value)
        {
            FindByLeft(ref Root, value);
            return this;
        }

        private void FindByLeft(ref Node node, T value)
        {
            if (Tryadd(ref Root, value))
                return;

            var found = false;
            for (int i = 0; i < node.Childrens.Length; i++)
            {
                found = Tryadd(ref node.Childrens[i], value);
                if (found) break;
            }

            if (!found)
                FindByLeft(ref Root.Childrens[0], value);
        }

        private bool Tryadd(ref Node node, T value)
        {
            if (node == null)
                node = new Node(value, NodeChildsLength);

            else if (!node.IsComplete)
                node.AddChild(value);

            else return false;

            return true;
        }

        public string PrintPostorder()
        {
            var output = new StringBuilder();

            TraversalPostorder(Root, output);
            return output.ToString();
        }

        private void TraversalPostorder(Node node, StringBuilder output)
        {
            for (int i = 0; i < node.Childrens.Length; i++)
            {
                if (node.Childrens[i] != null)
                    TraversalPostorder(node.Childrens[i], output);
            }

            output.Append(node.Value.ToString());
        }

        public string PrintPreorder()
        {
            var output = new StringBuilder();

            TraversalPreorder(Root, output);
            return output.ToString();
        }

        private void TraversalPreorder(Node node, StringBuilder output)
        {
            output.Append(node.Value.ToString());

            for (int i = 0; i < node.Childrens.Length; i++)
            {
                if (node.Childrens[i] != null)
                    TraversalPreorder(node.Childrens[i], output);
            }
        }
    }
}