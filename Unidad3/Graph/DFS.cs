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
        private int destinationIndx;
        public DFS(WeightedGraph<T> G, T s, T des) : base(G, s, des)
        {
            this.visited = new bool[G.V];
            this.distTo = new double[G.V];
            this.graph = G;
            this.destinationIndx = graph.GetVertex(des);
            edgeTo = new Edge[G.V];

            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = false;
            }

            // dfs(graph.GetVertex(s));
            dfs(s);
        }

        private bool[] visited;
        private int exploredNodes = 0;
        private void dfs2(int curr)
        {
            exploredNodes++;
            visited[curr] = true;
            if (curr == destinationIndx) ExploredNodes = exploredNodes;
            foreach (var node in graph.Adj(curr))
            {
                int other = node.Other(curr);
                if (!visited[other])
                {
                    edgeTo[other] = node;
                    distTo[other] = node.Weight + distTo[curr];
                    dfs2(node.Other(curr));
                }
            }
        }

        private void dfs(T curr)
        {
            Stack<int> st = new Stack<int>();
            st.Push(graph.GetVertex(curr));
            visited[graph.GetVertex(curr)] = true;
            while (st.Count != 0)
            {
                int act = st.Pop();
                foreach (var edge in graph.Adj(act))
                {
                    int other = edge.Other(act);
                    if (!visited[other])
                    {
                        edgeTo[other] = edge;
                        distTo[other] = distTo[act] + edge.Weight;
                        visited[other] = true;
                        st.Push(other);
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
