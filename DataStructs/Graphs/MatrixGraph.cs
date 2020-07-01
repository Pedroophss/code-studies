using System;
using System.Collections.Generic;

namespace DataStructs.Graphs
{
    public class MatrixGraph
    {
        private readonly GraphTypes Type;

        private int Vertices { get; set; }
        private int[,] Graph { get; set; }

        public MatrixGraph(GraphTypes type, int verticesNumber = 0)
        {
            Type = type;
            Vertices = verticesNumber;

            if (verticesNumber != 0)
                Graph = new int[verticesNumber, verticesNumber];
        }

        // Just for practice
        public int[,] GetGraphRepresentation() => Graph;

        /// <summary>
        /// Add a new vertice on graph
        /// </summary>
        /// <remarks>
        /// Time Complexity: O(V^2) ~= O(V^2)
        /// Space Complexity: O(V^2 + (v+1)^2) ~= O(V^2)
        /// </remarks>
        /// <returns>the nem vertice number</returns>
        public int AddNewVertice()
        {
            Vertices++;
            var aux = new int[Vertices, Vertices];

            for (int i = 0; i < Vertices - 1; i++)
                for (int j = 0; j < Vertices - 1; j++)
                    aux[i, j] = Graph[i, j];

            Graph = aux;
            return Vertices;
        }

        /// <summary>
        /// Link two vertices on graph
        /// </summary>
        /// <remarks>
        /// Time Complexity: O(1)
        /// </remarks>
        /// <param name="source">The source vertice</param>
        /// <param name="destiny">the destine vertice</param>
        public void AddEdge(int source, int destiny)
        {
            if (source >= Vertices || destiny >= Vertices || source == destiny)
                throw new ArgumentException("Source or Desiny is wrong");

            Graph[source, destiny] = 1;

            if (Type == GraphTypes.Undirected)
                Graph[destiny, source] = 1;
        }

        public int[] PrintBFS(int startSeach)
        {
            int printed = 1;
            var output = new int[Vertices];
            output[0] = startSeach;

            var visited = new bool[Vertices];
            visited[startSeach] = true;

            var auxDs = new Queue<int>();
            auxDs.Enqueue(startSeach);

            while (auxDs.Count != 0 && printed != Vertices)
            {
                var currentVertice = auxDs.Dequeue();
                for (int i = 0; i < Vertices; i++)
                    if (Graph[currentVertice, i] == 1 && !visited[i])
                    {
                        auxDs.Enqueue(i);
                        visited[i] = true;
                        output[printed] = i;
                        printed++;
                    }
            }

            return output;
        }

        public int[] PrintDFS(int startSeach)
        {
            int printed = 1;
            var output = new int[Vertices];
            output[0] = startSeach;

            var visited = new bool[Vertices];
            visited[startSeach] = true;

            var auxDs = new Stack<int>();
            auxDs.Push(startSeach);

            while (auxDs.Count != 0 && printed != Vertices)
            {
                var currentVertice = auxDs.Pop();
                for (int i = 0; i < Vertices; i++)
                    if (Graph[currentVertice, i] == 1 && !visited[i])
                    {
                        auxDs.Push(i);
                        visited[i] = true;
                        output[printed] = i;
                        printed++;
                        break;
                    }
            }

            return output;
        }
    }
}
