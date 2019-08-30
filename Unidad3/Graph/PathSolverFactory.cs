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
        public static PathResolver<T> Solver<T>(WeightedGraph<T> graph, T source, T destination, SearchTypes search)
        {
            switch (search)
            {
                case SearchTypes.DFS:
                    return new DFS<T>(graph, source, destination);
                case SearchTypes.Dijkstra:
                    return new Dijkstra<T>(graph, source, destination);
                case SearchTypes.BFS:
                    return new BFS<T>(graph, source, destination);
                default:
                    throw new Exception();
            }
        }
    }

    public enum SearchTypes
    {
        Dijkstra,
        DFS,
        BFS
    }
}
