using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3.Graph
{
    public class Edge : IComparable<Edge>
    {
        private int v, w;
        private double weight;

        public Edge(int v, int w, double weight)
        {
            this.v = v;
            this.w = w;
            this.weight = weight;
        }

        public int CompareTo(Edge other)
        {
            return weight.CompareTo(other.weight);
        }

        public int Either()
        {
            return v;
        }

        public int Other(int vertex)
        {
            if (vertex == v)
            {
                return w;
            }
            if (vertex == w)
            {
                return v;
            }
            throw new Exception("Error men");
        }

        public double Weight
        {
            get { return weight; }
        }


    }
}
