using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3.Graph;

namespace GPS.Graph
{
    public class DFS<T> : PathResolver<T>
    {

        private double[] distTo;
        private Edge[] edgeTo;
        private WeightedGraph<T> graph;
        public DFS(WeightedGraph<T> G, T s, T des = default(T)) : base(G, s, des)
        {
            this.visited = new bool[G.V];
            this.distTo = new double[G.V];
            this.graph = G;
            edgeTo = new Edge[G.V];

            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }

            dfs(graph.GetVertex(s));
        }

        private bool[] visited;
        private void dfs(int curr)
        {
            visited[curr] = true;

            foreach (var node in graph.Adj(curr))
            {
                int other = node.Other(curr);
                if (!visited[other])
                {
                    edgeTo[other] = node;
                    distTo[other] = node.Weight + distTo[curr];
                    dfs(node.Other(curr));
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
