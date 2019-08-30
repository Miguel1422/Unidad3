using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3.GPS.Util;
using Unidad3.Graph;

namespace GPS.Graph
{
    public class AStar : PathResolver<Node>
    {
        private double[] distTo;
        private Edge[] edgeTo;
        private Heap<Aux> pq;

        public AStar(WeightedGraph<Node> g, Node s, Node des) : base(g, s, des)
        {
            pq = new MinHeap<Aux>();
            distTo = new double[g.V];
            edgeTo = new Edge[g.V];

            for (int i = 0; i < g.V; i++)
            {
                distTo[i] = double.PositiveInfinity;
            }
            distTo[g.GetVertex(s)] = 0;

            pq.Add(new Aux(g.GetVertex(s), distTo[g.GetVertex(s)], 0));
            int exploredNodes = 0;
            while (pq.Count > 0)
            {
                exploredNodes++;
                Aux v = pq.ExtractDominating();
                if (v.V == g.GetVertex(des))
                {
                    ExploredNodes = exploredNodes;
                    break;
                }
                foreach (Edge e in g.Adj(v.V))
                {
                    Relax(e, v.V);
                }
            }
        }


        private void Relax(Edge e, int v)
        {
            int w = e.Other(v);
            if (distTo[w] > distTo[v] + e.Weight)
            {
                double h = destination.Distance(graph.GetVertex(v));
                distTo[w] = distTo[v] + e.Weight;
                edgeTo[w] = e;
                pq.Add(new Aux(w, distTo[w], h));

            }
        }

        public override double DistTo(Node v)
        {
            return distTo[graph.GetVertex(v)];
        }

        public override bool HasPathTo(Node v)
        {
            return distTo[graph.GetVertex(v)] < double.PositiveInfinity;
        }

        public override IEnumerable<Edge> PathTo(Node v)
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

        internal class Aux : IComparable<Aux>
        {
            private const float factor = 0.7f;
            private readonly int v;
            private readonly double weight;
            private readonly double heuristic;

            public Aux(int v, double weight, double heuristic)
            {
                this.v = v;
                this.weight = weight;
                this.heuristic = heuristic;
            }
            public int CompareTo(Aux other)
            {
                return (weight + factor * heuristic).CompareTo(other.weight + factor * other.heuristic);
            }

            public int V
            {
                get { return v; }
            }
        }

    }
}
