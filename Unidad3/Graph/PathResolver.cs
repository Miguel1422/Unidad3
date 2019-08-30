using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3.Graph;

namespace GPS.Graph
{
    public abstract class PathResolver<T>
    {
        protected readonly WeightedGraph<T> graph;
        protected readonly T source;
        protected readonly T destination;

        public PathResolver(WeightedGraph<T> graph, T source, T destination = default(T))
        {
            this.graph = graph;
            this.source = source;
            this.destination = destination;
        }

        public int ExploredNodes { get; protected set; }
        public abstract double DistTo(T v);
        public abstract bool HasPathTo(T v);
        public abstract IEnumerable<Edge> PathTo(T v);
    }
}
