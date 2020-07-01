using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructs.Graphs
{
    public class ListGraph
    {
        private readonly GraphTypes Type;
        private int Vertices { get; set; }
        private LinkedList<int>[] Graph { get; set; }

        public ListGraph(GraphTypes type, int vertices)
        {
            Type = type;
            Vertices = vertices;
            Graph = new LinkedList<int>[vertices];
        }

        /// <summary>
        /// Add a new vertice on graph
        /// </summary>
        /// <remarks>
        /// Time Complexity: O(V) ~= O(V)
        /// Space Complexity: O(2V+1) ~= O(V)
        /// </remarks>
        /// <returns>the nem vertice number</returns>
        public int AddNewVertice()
        {
            Vertices++;
            var aux = new LinkedList<int>[Vertices];

            for (int i = 0; i < Vertices - 1; i++)
                aux[i] = Graph[i];

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
        private void AddEdge(int source, int destiny)
        {
            if (source >= Vertices || destiny >= Vertices || source == destiny)
                throw new ArgumentException("Source or Desiny is wrong");

            Graph[source].AddLast(destiny);

            if (Type == GraphTypes.Undirected)
                Graph[destiny].AddLast(source);
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
                foreach (var linkedVertice in Graph[currentVertice])
                    if (!visited[linkedVertice])
                    {
                        auxDs.Enqueue(linkedVertice);
                        visited[linkedVertice] = true;
                        output[printed] = linkedVertice;
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
                foreach (var linkedVertice in Graph[currentVertice])
                    if (!visited[linkedVertice])
                    {
                        auxDs.Push(linkedVertice);
                        visited[linkedVertice] = true;
                        output[printed] = linkedVertice;
                        printed++;
                        break;
                    }
            }

            return output;
        }
    }
}
