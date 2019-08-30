using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3.Graph;

namespace GPS.Graph
{
    public class BFS<T> : PathResolver<T>
    {
        private double[] distTo;
        private Edge[] edgeTo;
        private bool[] visited;
        public BFS(WeightedGraph<T> graph, T source, T destination) : base(graph, source, destination)
        {
            distTo = new double[graph.V];
            visited = new bool[graph.V];
            edgeTo = new Edge[graph.V];
            Queue<int> q = new Queue<int>();
            q.Enqueue(graph.GetVertex(source));
            visited[graph.GetVertex(source)] = true;

            int exploredNodes = 0;
            int dest = graph.GetVertex(destination);
            while (q.Count > 0)
            {
                int curr = q.Dequeue();
                exploredNodes++;
                if (curr == dest)
                {
                    ExploredNodes = exploredNodes;
                    break;
                }
                foreach (var edge in graph.Adj(curr))
                {
                    int other = edge.Other(curr);
                    if (!visited[other])
                    {
                        edgeTo[other] = edge;
                        distTo[other] = distTo[curr] + edge.Weight;
                        visited[other] = true;
                        q.Enqueue(other);
                    }
                }
            }
        }

        public override bool HasPathTo(T v)
        {
            return visited[graph.GetVertex(v)];
        }
        public override IEnumerable<Edge> PathTo(T v)
        {
            if (!HasPathTo(v)) return null;
            int nodeV = graph.GetVertex(v);
            Stack<Edge> path = new Stack<Edge>();
            int x = nodeV;
            for (Edge e = edgeTo[nodeV]; e != null; e = edgeTo[x])
            {
                path.Push(e);
                x = e.Other(x);
            }
            return path;
        }

        public override double DistTo(T v)
        {
            return distTo[graph.GetVertex(v)];
        }
    }
}
