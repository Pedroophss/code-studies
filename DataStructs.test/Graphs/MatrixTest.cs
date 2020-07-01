using DataStructs.Graphs;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructs.test.Graphs
{
    public class MatrixTest
    {
        [Fact]
        public void TestUndirected()
        {
            var graph = new MatrixGraph(GraphTypes.Undirected, 5);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 4);

            graph.AddEdge(1, 4);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 2);

            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);

            var print = graph.PrintBFS(3);
        }

        [Fact]
        public void TestDirected()
        {
            var graph = new MatrixGraph(GraphTypes.Directed, 6);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 4);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 1);
            graph.AddEdge(5, 0);

            var print = graph.PrintBFS(3);
        }

        [Fact]
        public void TestDirected_DFS()
        {
            var graph = new MatrixGraph(GraphTypes.Directed, 7);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 2);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 3);

            var print = graph.PrintDFS(0);
        }
    }
}