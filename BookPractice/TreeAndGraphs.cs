using DataStructs.Graphs;
using DataStructs.Trees;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Xunit;

namespace BookPractice
{
    #region .:: My Binary Tree ::.

    public class BinaryTree<T>
    {
        public T Value { get; }
        public BinaryTree<T> Left { get; private set; }
        public BinaryTree<T> Right { get; private set; }
        public BinaryTree<T> Parent { get; private set; }

        private BinaryTree(T value) =>
            Value = value;

        public static BinaryTree<T> Create(T value) =>
            new BinaryTree<T>(value);

        public BinaryTree<T> LinkLeft(T value)
        {
            Left = new BinaryTree<T>(value);
            Left.Parent = this;
            return this;
        }

        public BinaryTree<T> LinkLeft(BinaryTree<T> node)
        {
            Left = node;
            node.Parent = this;
            return this;
        }

        public BinaryTree<T> LinkRight(T value)
        {
            Right = new BinaryTree<T>(value);
            Right.Parent = this;
            return this;
        }

        public BinaryTree<T> LinkRight(BinaryTree<T> node)
        {
            Right = node;
            node.Parent = this;
            return this;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }

    #endregion

    public class TreeAndGraphs
    {
        #region .:: Utils ::.

        public BinaryTree<T> Tree<T>(T value) =>
            BinaryTree<T>.Create(value);

        public BinaryTree<int> GetBST()
        {
            var minorTree1 = Tree(9).LinkLeft(4).LinkRight(10);
            var minorTree2 = Tree(15).LinkLeft(13).LinkRight(19);
            var minorTree = Tree(12).LinkLeft(minorTree1).LinkRight(minorTree2);

            var majorTree1 = Tree(50).LinkLeft(45).LinkRight(61);
            var majorTree = Tree(43).LinkLeft(37).LinkRight(majorTree1);

            return Tree(31).LinkLeft(minorTree).LinkRight(majorTree);
        }

        #endregion

        #region .:: Score ::.

        // 4.1 I got it right, but my solution was a bit complex
        // 75%

        // 4.2 I did the exactly they was expecting
        // 100%

        // 4.3 My approach it was very different then book`s, but i think was good because i did in the same O notation
        // 80%

        // 4.4 I did the exactly they was expecting
        // 100%

        // 4.5 The idea was good but the implementation was sucks
        // I spent much more space than theirs one and my solution was a few messed
        // 50%

        // 4.10 The execution was good, and i did the same result as book, but the slower response
        // 90%

        // 4.7 Not Pass 0%
        // 4.8 Not Pass 0%
        // 4.9 Not Pass 0%

        // 4.10 The execution was good, and i did the same result as book, but the slower response
        // 90%

        // 4.11 My solution was a brutal force
        // 10%

        // 4.12 I was interpret wrong the exercicise, because that, i create more complex code to solve a more complex problem
        // 70%

        #endregion

        #region .:: 4.1 - Route Between Nodes ::.

        // Given a directed graph, design an algorithm to find out whether there is a route between two nodes.

        [Fact]
        public void HasConnection()
        {
            // Arrange
            var myGraph = new MatrixGraph(GraphTypes.Directed, 15);
            {
                myGraph.AddEdge(0, 1);
                myGraph.AddEdge(0, 2);
                myGraph.AddEdge(1, 9);
                myGraph.AddEdge(1, 2);
                myGraph.AddEdge(1, 3);
                myGraph.AddEdge(1, 11);
                myGraph.AddEdge(11, 9);
                myGraph.AddEdge(2, 1);
                myGraph.AddEdge(3, 4);
                myGraph.AddEdge(3, 8);
                myGraph.AddEdge(4, 10);
                myGraph.AddEdge(4, 3);
                myGraph.AddEdge(4, 5);
                myGraph.AddEdge(10, 7);
                myGraph.AddEdge(10, 4);
                myGraph.AddEdge(7, 6);
                myGraph.AddEdge(6, 7);
                myGraph.AddEdge(5, 6);
                myGraph.AddEdge(5, 2);
                myGraph.AddEdge(8, 13);
                myGraph.AddEdge(13, 14);
            }

            var graphMatrix = myGraph.GetGraphRepresentation();

            // Practice
            bool HasRoute(int node1, int node2)
            {
                var keepGoing = true;
                var vertices = graphMatrix.GetLength(0);
                var seachVisited = new int[vertices];

                var bfsOrder1 = new Queue<int>(vertices);
                bfsOrder1.Enqueue(node1);

                var bfsOrder2 = new Queue<int>(vertices);
                bfsOrder2.Enqueue(node2);

                bool HasVisited(int node, int nodeIndex) =>
                    (seachVisited[node] & (1 << nodeIndex)) != 0;

                while (keepGoing)
                {
                    keepGoing = bfsOrder1.TryDequeue(out int node);
                    if (keepGoing)
                        for (byte i = 0; i < vertices; i++)
                            if (graphMatrix[node, i] == 1 && !HasVisited(i, 1))
                            {
                                bfsOrder1.Enqueue(i);
                                seachVisited[i] = seachVisited[i] | 1 << 1;
                                if (HasVisited(i, 2))
                                    return true;
                            }

                    keepGoing = bfsOrder2.TryDequeue(out node);
                    if (keepGoing)
                        for (byte i = 0; i < vertices; i++)
                            if (graphMatrix[node, i] == 1 && !HasVisited(i, 2))
                            {
                                bfsOrder2.Enqueue(i);
                                seachVisited[i] = seachVisited[i] | 1 << 2;
                                if (HasVisited(i, 1))
                                    return true;
                            }
                }

                return false;
            }

            Assert.True(HasRoute(2, 10));
            Assert.False(HasRoute(11, 8));
        }

        #endregion

        #region .:: 4.2 - Minimal Tree ::.

        // Given a sorted (increasing order) array with unique integer elements, write an algorithm to create a binary search tree with minimal height.

        public class BstNode
        {
            public int Value { get; set; }
            public BstNode Left { get; set; }
            public BstNode Right { get; set; }

            public BstNode(int value)
            {
                Value = value;
            }
        }

        public int[] Less42 = new int[] { 1, 2 };
        public int[] Pair42 = new int[] { 1, 2, 3, 4, 5, 6 };
        public int[] Simple42 = new int[] { 1, 2, 3, 4, 5, 6, 7 };

        [Fact]
        public void CreateBstFromArray()
        {
            var array = Simple42;

            BstNode FillSubTree(int min, int max)
            {
                var distance = max - min;
                if (distance < 0)
                    return null;

                else if (distance == 0)
                    return new BstNode(array[min]);

                else
                {
                    var mid = min + distance / 2;
                    var root = new BstNode(array[mid]);

                    root.Left = FillSubTree(min, mid - 1);
                    root.Right = FillSubTree(mid + 1, max);

                    return root;
                }
            }

            var tree = FillSubTree(0, array.GetUpperBound(0));
            Debug.WriteLine(tree.Value);
        }

        #endregion

        #region .:: 4.3 - List of Depths ::.

        // Given a binary tree, design an algorithm which creates a linked list of all the nodes
        // at each depth(e.g., if you have a tree with depth D, you'll have D linked lists). 

        [Fact]
        public void ListDepths()
        {
            // Arrange
            var tree = new SimpleTree<int>(2);
            tree.AddNode(11)
                .AddNode(21).AddNode(22)
                .AddNode(31).AddNode(32).AddNode(33).AddNode(34)
                .AddNode(41).AddNode(42).AddNode(43).AddNode(44);//.AddNode(45).AddNode(46).AddNode(47).AddNode(48);

            var output = new List<LinkedList<int>>();

            var currentLimitLeafs = 1;
            var currentLinkedList = new LinkedList<int>();
            var auxDs = new Queue<SimpleTree<int>.Node>();

            void AddVisitedNode(SimpleTree<int>.Node node)
            {
                auxDs.Enqueue(node);
                currentLinkedList.AddLast(node.Value);

                if (currentLinkedList.Count == currentLimitLeafs)
                {
                    currentLimitLeafs *= 2;
                    output.Add(currentLinkedList);
                    currentLinkedList = new LinkedList<int>();
                }
            }

            // Do root manualy
            AddVisitedNode(tree.Root);

            while (auxDs.TryDequeue(out SimpleTree<int>.Node currentNode))
            {
                // Left
                if (currentNode.Childrens[0] != null)
                    AddVisitedNode(currentNode.Childrens[0]);

                // Right
                if (currentNode.Childrens[1] != null)
                    AddVisitedNode(currentNode.Childrens[1]);
            }

            // Handling imcomplete trees
            if (currentLinkedList.Count > 0)
                output.Add(currentLinkedList);
        }

        #endregion

        #region .:: 4.4 - Check Balanced ::.

        // Implement a function to check if a binary tree is balanced. 
        // For the purposes of this question, a balanced tree is defined to be a tree such that the heights of the two subtrees of any
        // node never differ by more than one.

        [Fact]
        public void CheckBalanced()
        {
            // Arrange
            var root = Tree(1).LinkLeft(Tree(2).LinkLeft(Tree(4).LinkLeft(6))
                                               .LinkRight(5))
                              .LinkRight(Tree(3).LinkLeft(Tree(7).LinkLeft(9))
                                                .LinkRight(Tree(8).LinkLeft(Tree(10).LinkLeft(Tree(12).LinkLeft(13)))
                                                                  .LinkRight(11)));

            (int height, bool isBalanced) CheckBalancedNode(BinaryTree<int> node)
            {
                int leftHeight = 0, rightHeight = 0;
                if (node.Left != null)
                {
                    var result = CheckBalancedNode(node.Left);
                    if (!result.isBalanced)
                        return result;

                    leftHeight = result.height;
                }

                if (node.Right != null)
                {
                    var result = CheckBalancedNode(node.Right);
                    if (!result.isBalanced)
                        return result;

                    rightHeight = result.height;
                }

                var diff = leftHeight - rightHeight;
                if (diff < -1 || diff > 1)
                    return (default, false);

                return (Math.Max(leftHeight, rightHeight) + 1, true);
            }

            var _result = CheckBalancedNode(root);

            // Checker
            Assert.True(_result.isBalanced);
        }

        #endregion

        #region .:: 4.5 - Check BST ::.

        // Implement a function to check if a binary tree is a binary search tree.

        public class BSTResponse
        {
            public readonly bool IsBST;
            public readonly int MinValue;
            public readonly int MaxValue;

            public BSTResponse(bool isBst, int minValue, int maxValue)
            {
                IsBST = isBst;
                MinValue = minValue;
                MaxValue = maxValue;
            }
        }

        private static BSTResponse InvalidBst = new BSTResponse(false, 0, 0);

        [Fact]
        public void CheckBST()
        {
            // Time Solution O(N)
            // Space Solution (IDK) => Studing this!!

            // Arrange
            var tree = Tree(31).LinkLeft(Tree(12).LinkLeft(9).LinkRight(32))
                               .LinkRight(Tree(43).LinkLeft(37).LinkRight(50));

            BSTResponse CheckBSTNode(BinaryTree<int> node)
            {
                if (node == null)
                    return InvalidBst;

                var thisNodeResponse = new BSTResponse(true, node.Value, node.Value);

                var leftResponse = node.Left == null ? thisNodeResponse : CheckBSTNode(node.Left);
                if (!leftResponse.IsBST)
                    return InvalidBst;

                var rightResponse = node.Right == null ? thisNodeResponse : CheckBSTNode(node.Right);
                if (!rightResponse.IsBST)
                    return InvalidBst;

                return new BSTResponse
                (
                    node.Value >= leftResponse.MaxValue && node.Value <= rightResponse.MinValue,
                    leftResponse.MinValue,
                    rightResponse.MaxValue
                );
            }

            var output = CheckBSTNode(tree);
            Assert.True(output.IsBST);
        }

        // Implements the inorder approach

        // Implements the book's solution using the ranges

        #endregion

        // Check Score
        #region .:: 4.6 - Successor ::.

        // Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a
        // binary search tree.You may assume that each node has a link to its parent.

        [Fact]
        public void Sucessor()
        {
            // Arrange
            var tree = GetBST();

            BinaryTree<int> GetSucessor(BinaryTree<int> node)
            {
                // Get "leftest" node
                if (node.Right != null)
                {
                    var current = node.Right;
                    while (current.Left != null)
                        current = current.Left;

                    return current;
                }
                else if (node.Parent != null)
                {
                    var current = node.Parent;
                    while (current != null)
                    {
                        if (current.Value > node.Value)
                            return current;

                        current = current.Parent;
                    }
                }

                return null;
            }

            Assert.Equal(10, GetSucessor(tree.Left.Left).Value);
            Assert.Equal(15, GetSucessor(tree.Left.Right.Left).Value);
            Assert.Equal(31, GetSucessor(tree.Left.Right.Right).Value);
            Assert.Equal(43, GetSucessor(tree.Right.Left).Value);
            Assert.Equal(37, GetSucessor(tree).Value);
            Assert.Equal(13, GetSucessor(tree.Left).Value);
            Assert.Null(GetSucessor(tree.Right.Right.Right));
        }

        #endregion

        // REVIEW
        #region .:: 4.7 - Build Order ::.

        // You are given a list of projects and a list of dependencies (which is a list of pairs of
        // projects, where the second project is dependent on the first project). All of a project's dependencies
        // must be built before the project is. Find a build order that will allow the projects to be built.If there
        // is no valid build order, return an error

        // EXAMPLE
        // Input:
        // projects: a, b, c, d, e, f
        // dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
        // Output: f, e, a, b, d, c

        private class Project
        {
            public readonly char Name;

            public bool Builded { get; set; }
            public LinkedList<Project> DependProjects { get; set; }

            public Project(char name)
            {
                Name = name;
                Builded = false;
                DependProjects = new LinkedList<Project>();
            }
        }

        [Fact]
        public void GetBuildOrder()
        {
            // Arrange
            var _projects = new char[] { 'a', 'b', 'c', 'd', 'e', 'f' };
            var _dependecies = new (char dependecy, char project)[] { ('a', 'd'), ('f', 'b'), ('b', 'd'), ('f', 'a'), ('d', 'c') };

            // Code
            var graph = new Dictionary<char, Project>(_projects.Length);
            for (byte i = 0; i < _projects.Length; i++)
                graph.Add(_projects[i], new Project(_projects[i]));

            foreach (var buildConfig in _dependecies)
            {
                var _project = graph[buildConfig.project];
                var _dependency = graph[buildConfig.dependecy];

                if (_project.DependProjects.Contains(_dependency))
                    Assert.True(false);

                _dependency.DependProjects.AddLast(_project);
            }

            var output = new Stack<char>();
            void TopologicalSort(Project project)
            {
                project.Builded = true;
                foreach (var dependProject in project.DependProjects)
                    if (!dependProject.Builded)
                        TopologicalSort(dependProject);

                output.Push(project.Name);
            }

            foreach (var project in graph.Values)
                if (!project.Builded)
                    TopologicalSort(project);
        }

        #endregion

        // REVIEW
        #region .:: 4.8 - First Common Ancestor ::.

        // Design an algorithm and write code to find the first common ancestor
        // of two nodes in a binary tree.Avoid storing additional nodes in a data structure.NOTE: This is not
        // necessarily a binary search tree.

        // Tooo HARD

        #endregion

        // REVIEW
        #region .:: 4.9 - BST Sequences ::.

        // A binary search tree was created by traversing through an array from left to right
        // and inserting each element.Given a binary search tree with distinct elements, print all possible
        // arrays that could have led to this tree.

        // Wrong: I didnt understand the problem correctly
        [Fact]
        public void BSTSequences()
        {
            // Arrange
            //var tree = GetBST();
            var tree = Tree(2).LinkLeft(1).LinkRight(3);

            (BinaryTree<int> first, BinaryTree<int> second) GetNodeOrder(BinaryTree<int> node, bool startByLeft) =>
                startByLeft ? (node.Left, node.Right) : (node.Right, node.Left);

            // Code
            int[] FillBSTSequence(bool startByLeft = true)
            {
                var output = new List<int>();
                var bfsAux = new Queue<BinaryTree<int>>();
                bfsAux.Enqueue(tree);

                while (bfsAux.Count > 0)
                {
                    var current = bfsAux.Dequeue();
                    if (current != null)
                    {
                        output.Add(current.Value);

                        var (first, second) = GetNodeOrder(current, startByLeft);
                        bfsAux.Enqueue(first);
                        bfsAux.Enqueue(second);
                    }
                }

                return output.ToArray();
            }

            var out1 = FillBSTSequence();
            var out2 = FillBSTSequence(false);
        }


        #endregion

        #region .:: 4.10 - Check Subtree ::.

        // Tl and T2 are two very large binary trees, with Tl much bigger than T2.Create an algorithm to determine if T2 is a subtree of Tl.
        // A tree T2 is a subtree of Tl if there exists a node n in Tl such that the subtree of n is identical to T2.
        // That is, if you cut off the tree at node n, the two trees would be identical. 

        [Fact]
        public void CheckSubtree()
        {
            #region .:: Arrange ::.

            var left = Tree(2).LinkRight(Tree(5).LinkLeft(12).LinkRight(13));
            var right = Tree(3).LinkLeft(Tree(6).LinkLeft(8).LinkRight(Tree(9).LinkRight(Tree(16).LinkLeft(17))))
                               .LinkRight(Tree(7).LinkLeft(11).LinkRight(10));

            var t1 = Tree(1).LinkLeft(left).LinkRight(right);
            var t2 = Tree(6).LinkLeft(8).LinkRight(Tree(9).LinkRight(Tree(16).LinkLeft(17)));

            #endregion

            // Compare each node of T1 with respctive node from T2
            bool CheckSubtree(BinaryTree<int> node1, BinaryTree<int> node2)
            {
                if (node1 == null && node2 == null)
                    return true;

                if ((node1 != null && node2 == null) || (node2 != null && node1 == null))
                    return false;

                return node1.Value == node2.Value
                    && CheckSubtree(node1.Left, node2.Left)
                    && CheckSubtree(node1.Right, node2.Right);
            }

            // Found root T2 inside T1 using BFS
            var auxStack = new Stack<BinaryTree<int>>();
            auxStack.Push(t1);

            while (auxStack.Count != 0)
            {
                var current = auxStack.Pop();
                if (current.Value == t2.Value && CheckSubtree(current, t2))
                    return;

                if (current.Left != null)
                    auxStack.Push(current.Left);

                if (current.Right != null)
                    auxStack.Push(current.Right);
            }

            Assert.True(false);
        }

        #endregion

        #region .:: 4.11 - Random Node ::.

        // You are implementing a binary tree class from scratch which, in addition to
        // insert, find, and delete, has a method getRandomNode() which returns a random node
        // from the tree.All nodes should be equally likely to be chosen. Design and implement an algorithm
        // for getRandomNode, and explain how you would implement the rest of the methods.

        public class RandomTree<T>
        {
            private int Count { get; set; }
            private Random Randomizer { get; }
            private RandomNode Root { get; set; }

            public class RandomNode
            {
                public T Value { get; set; }
                public RandomNode Left { get; set; }
                public RandomNode Right { get; set; }

                public RandomNode(T value)
                {
                    Value = value;
                }

                public bool IsFull() =>
                    Left != null && Right != null;

                public bool IsEmpty() =>
                    Left == null && Right == null;
            }

            public RandomTree(T rootValue)
            {
                Count = 1;
                Root = new RandomNode(rootValue);
                Randomizer = new Random(DateTime.Now.Millisecond);
            }

            public void Insert(T newValue)
            {
                var aux = new Stack<RandomNode>();
                aux.Push(Root);

                while (aux.Count != 0)
                {
                    var current = aux.Pop();
                    if (current.Left == null)
                    {
                        current.Left = new RandomNode(newValue);
                        return;
                    }

                    else if (current.Right == null)
                    {
                        current.Right = new RandomNode(newValue);
                        return;
                    }

                    aux.Push(current.Left);
                    aux.Push(current.Right);
                }

                Count++;
            }

            public RandomNode _Find(RandomNode node, T value)
            {
                if (node == null)
                    return null;

                var output = _Find(node.Left, value);
                if (output != null)
                    return output;

                if (node.Value.Equals(value))
                    return node;

                return _Find(node.Right, value);
            }

            public RandomNode Find(T value) =>
                _Find(Root, value);

            public RandomNode GetRandomNode()
            {
                var count = 0;
                var nodeIndex = Randomizer.Next(Count);
                var auxDs = new Stack<RandomNode>();
                auxDs.Push(Root);

                while (count < nodeIndex)
                {
                    var current = auxDs.Pop();

                    auxDs.Push(current.Left);
                    auxDs.Push(current.Right);

                    count++;
                }

                return auxDs.Pop();
            }
        }

        public void RandomNode()
        {
        }

        #endregion

        #region .:: 4.12 - Paths with Sum ::.

        // You are given a binary tree in which each node contains an integer value(which
        // might be positive or negative). Design an algorithm to count the number of paths that sum to a
        // given value.The path does not need to start or end at the root or a leaf, but it must go downwards
        // (traveling only from parent nodes to child nodes). 

        [Fact]
        public void GetPathWithSum()
        {
            #region .:: Arrange ::.

            var left = Tree(2).LinkLeft(Tree(4).LinkLeft(9).LinkRight(5)).LinkRight(-3);
            var rigth = Tree(10).LinkLeft(Tree(3).LinkRight(4)).LinkRight(-8);
            var tree = Tree(7).LinkLeft(left).LinkRight(rigth);

            #endregion

            var value = 6;

            // code
            var sumPaths = 0;
            var sumPath = new List<int>();
            void GetPath(BinaryTree<int> node, int depth)
            {
                for (int i = 0; i < sumPath.Count; i++)
                {
                    sumPath[i] += node.Value;
                    if (sumPath[i] == value)
                        sumPaths++;
                }

                sumPath.Add(node.Value);

                if (node.Left != null)
                    GetPath(node.Left, depth + 1);

                if (node.Right != null)
                    GetPath(node.Right, depth + 1);

                for (int i = 0; i < sumPath.Count - 1; i++)
                    sumPath[i] -= node.Value;

                sumPath.RemoveAt(depth);
            }

            GetPath(tree, 0);
        }

        #endregion
    }
}
