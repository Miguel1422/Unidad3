using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3.Graph;

namespace GPS.Graph
{
    public class PathSolverFactory
    {
        public static PathResolver<T> Solver<T>(WeightedGraph<T> graph, T source, T destination)
        {
            return new Dijkstra<T>(graph, source, destination);
        }
    }
}
