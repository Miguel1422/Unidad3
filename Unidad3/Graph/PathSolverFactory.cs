using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3.GPS.Util;
using Unidad3.Graph;

namespace GPS.Graph
{
    public class PathSolverFactory
    {
        public static PathResolver<Node> Solver(WeightedGraph<Node> graph, Node source, Node destination, SearchTypes search)
        {
            switch (search)
            {
                case SearchTypes.DFS:
                    return new DFS<Node>(graph, source, destination);
                case SearchTypes.Dijkstra:
                    return new Dijkstra<Node>(graph, source, destination);
                case SearchTypes.BFS:
                    return new BFS<Node>(graph, source, destination);
                case SearchTypes.AStar:
                    return new AStar(graph, source, destination);
                default:
                    throw new Exception();
            }
        }
    }

    public enum SearchTypes
    {
        Dijkstra,
        DFS,
        BFS,
        AStar
    }
}
