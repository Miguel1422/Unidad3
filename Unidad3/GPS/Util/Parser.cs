using Unidad3.Graph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3.GPS.Util
{
    public class Parser
    {
        public static string FILE_DIRECTORY = @"./Data/";
        public static string GRAFO = FILE_DIRECTORY + "Grafo.txt";
        public static string CIUDADES = FILE_DIRECTORY + "Ciudades.txt";
        public static string CARRETERAS = FILE_DIRECTORY + "Carreteras.txt";
        public static string NODOS = FILE_DIRECTORY + "Nodes.txt";
        public static string IMG_MAP = FILE_DIRECTORY + "asd2.png";
        private List<Node> nodes;
        private List<City> ciudades;
        private List<Way> ways;
        private Dictionary<long, Node> nodeById;
        private Dictionary<Node, List<Way>> waysByNode;
        private WeightedGraph<Node> G;
        private double xMin;
        private double xMax;
        private double yMin;
        private double yMax;
        
        public Parser()
        {
        
            xMin = double.PositiveInfinity;
            xMax = double.NegativeInfinity;
            yMin = double.PositiveInfinity;
            yMax = double.NegativeInfinity;
            nodes = new List<Node>();
            ciudades = new List<City>();
            ways = new List<Way>();
            G = new WeightedGraph<Node>();
            nodeById = new Dictionary<long, Node>();
            waysByNode = new Dictionary<Node, List<Way>>();
            string[] nodeLines = File.ReadAllLines(NODOS);
            for (int i = 0; i < nodeLines.Length; i++)
            {
                string[] tok = nodeLines[i].Split('|');
                long id = long.Parse(tok[0]);
                double lat = double.Parse(tok[1]);
                double lon = double.Parse(tok[2]);
                Node n = new Node(id, lat, lon);
                nodes.Add(n);
                nodeById.Add(id, n);
                yMin = Math.Min(yMin, lat);
                yMax = Math.Max(yMax, lat);
                xMin = Math.Min(xMin, lon);
                xMax = Math.Max(xMax, lon);
            }
            nodeLines = null;
            GC.Collect();

            string[] cityLines = File.ReadAllLines(CIUDADES);
            for (int i = 0; i < cityLines.Length; i++)
            {
                string[] tok = cityLines[i].Split('|');
                long id = long.Parse(tok[0]);
                double lat = double.Parse(tok[1]);
                double lon = double.Parse(tok[2]);
                string name = tok[3];
                Node n = new Node(id, lat, lon);
                ciudades.Add(new City(name, n));
            }
            cityLines = null;
            GC.Collect();

            string[] wayLines = File.ReadAllLines(CARRETERAS);
            for (int i = 0; i < wayLines.Length; i++)
            {
                string[] tok = wayLines[i].Split('|');
                long id = long.Parse(tok[0]);
                string name = tok[tok.Length - 1];
                Way w = new Way(name, id);
                for (int j = 1; j < tok.Length - 1; j++)
                {
                    long idA = long.Parse(tok[j]);
                    w.Add(idA);
                    Node n = nodeById[idA];
                    if (!waysByNode.ContainsKey(n))
                    {
                        waysByNode.Add(n, new List<Way>());
                    }
                    waysByNode[n].Add(w);

                }
                ways.Add(w);

            }
            wayLines = null;
            GC.Collect();

            string[] graphLines = File.ReadAllLines(GRAFO);

            for (int i = 0; i < graphLines.Length; i++)
            {
                string[] tok = graphLines[i].Split('|');
                long id = long.Parse(tok[0]);
                Node a = nodeById[id];
                for (int j = 1; j < tok.Length; j++)
                {
                    long idA = long.Parse(tok[j]);
                    Node b = nodeById[idA];
                    G.AddEdge(a, b, Distance(a, b));
                }


            }
            graphLines = null;
            GC.Collect();

        }

        private double ToRadians(double n)
        {
            return (n * Math.PI / 180);
        }

        public double Distance(Node a, Node b)
        {
            double ans = 0;
            double lat1 = ToRadians(a.Latitude);
            double lon1 = ToRadians(a.Longitude);
            double lat2 = ToRadians(b.Latitude);
            double lon2 = ToRadians(b.Longitude);

            double difLat = (ToRadians(a.Latitude - b.Latitude));
            double diflon = (ToRadians(a.Longitude - b.Longitude));
            double aa = Math.Pow(Math.Sin(difLat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(diflon/2) , 2);
            double c = 2 * Math.Atan2(Math.Sqrt(aa), Math.Sqrt(1 - aa));
            ans = 6371 * c * 1000;

            return ans;


        }

        public List<Node> Nodes
        {
            get { return nodes; }
        }

        public List<City> Cities
        {
            get { return ciudades; }
        }

        public List<Way> Ways
        {
            get { return ways; }
        }

        public WeightedGraph<Node> Graph
        {
            get { return G; }
        }

        public Dictionary<Node, List<Way>> WaysByNode
        {
            get { return waysByNode; }
        }

        public Dictionary<long, Node> NodeById
        {
            get { return nodeById; }
        }

        public double XMin
        {
            get { return xMin; }
        }
        public double XMax
        {
            get { return xMax; }
        }
        public double YMin
        {
            get { return yMin; }
        }
        public double YMax
        {
            get { return yMax; }
        }
    }
}
