using System;
using System.Collections.Generic;

namespace Unidad3.Graph
{
    public class Dijkstra<T>
    {
        private double[] distTo;
        private Edge[] edgeTo;
        private Heap<Aux> pq;

        public Dijkstra(WeightedGraph<T> G, int s, int des = -1)
        {
            pq = new MinHeap<Aux>();
            distTo = new double[G.V];
            edgeTo = new Edge[G.V];

            for (int i = 0; i < G.V; i++)
            {
                distTo[i] = double.PositiveInfinity;
            }
            distTo[s] = 0;

            pq.Add(new Aux(s, distTo[s]));

            while (pq.Count > 0)
            {
                Aux v = pq.ExtractDominating();
                if (v.V == des)
                {
                    break;
                }
                foreach (Edge e in G.Adj(v.V))
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

        public double DistTo(int v)
        {
            return distTo[v];
        }

        public bool HasPathTo(int v)
        {
            return distTo[v] < double.PositiveInfinity;
        }

        public Stack<Edge> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            Stack<Edge> path = new Stack<Edge>();
            int x = v;
            for (Edge e = edgeTo[v]; e != null; e = edgeTo[x])
            {
                path.Push(e);
                x = e.Other(x);
            }
            return path;
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