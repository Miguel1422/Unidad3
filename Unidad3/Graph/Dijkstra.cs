using GPS.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unidad3.Graph
{
    public class Dijkstra<T> : PathResolver<T>
    {
        private double[] distTo;
        private Edge[] edgeTo;
        private Heap<Aux> pq;

        public Dijkstra(WeightedGraph<T> g, T s, T des = default(T)) : base(g, s, des)
        {
            pq = new MinHeap<Aux>();
            distTo = new double[g.V];
            edgeTo = new Edge[g.V];

            for (int i = 0; i < g.V; i++)
            {
                distTo[i] = double.PositiveInfinity;
            }
            distTo[g.GetVertex(s)] = 0;

            pq.Add(new Aux(g.GetVertex(s), distTo[ g.GetVertex(s)]));

            while (pq.Count > 0)
            {
                Aux v = pq.ExtractDominating();
                if (v.V == g.GetVertex(des))
                {
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
                distTo[w] = distTo[v] + e.Weight;
                edgeTo[w] = e;
                pq.Add(new Aux(w, distTo[w]));

            }
        }

        public override double DistTo(T v)
        {
            return distTo[ graph.GetVertex(v)];
        }

        public override bool HasPathTo(T v)
        {
            return distTo[ graph.GetVertex(v)] < double.PositiveInfinity;
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
            return path.Reverse();
        }

        internal class Aux : IComparable<Aux>
        {
            private int v;
            private double weight;
            public Aux(int v, double weight)
            {
                this.v = v;
                this.weight = weight;
            }
            public int CompareTo(Aux other)
            {
                return weight.CompareTo(other.weight);
            }

            public int V
            {
                get { return v; }
            }
            public double Weight
            {
                get { return weight; }
            }
        }

    }
}