
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Graph
{

    public class WeightedGraph<T>
    {
        private List<List<Edge>> vertex;
        private Dictionary<T, int> names;
        private List<T> ids;
        private int edges;

        public WeightedGraph()
        {

            vertex = new List<List<Edge>>();
            names = new Dictionary<T, int>();
            ids = new List<T>();
            edges = 0;
            for (int i = 0; i < 1000; i++)
            {
                vertex.Add(new List<Edge>());
            }
        }

        public WeightedGraph(T[] ciudades)
        {
            int i = 0;
            foreach (T ciudad in ciudades)
            {
                names.Add(ciudad, i++);
                ids.Add(ciudad);
            }
        }

        public int V
        {
            get { return names.Count; }
        }

        public int E
        {
            get { return edges; }
        }


        private void AddVertex(T v)
        {
            if (!names.ContainsKey(v))
            {
                int u = names.Count;
                names.Add(v, u);
                ids.Add(v);
                if (u >= vertex.Count)
                {
                    vertex.Add(new List<Edge>());
                }
            }

        }

        public void AddEdge(T a, T b, double weight)
        {
            AddVertex(a);
            AddVertex(b);
            if (names.Count == 100)
            {
                Console.WriteLine();
            }
            int v = names[a];
            int w = names[b];

            Edge e = new Edge(v, w, weight);

            vertex[v].Add(e);
            vertex[w].Add(e);
            edges++;
        }

        public List<Edge> Adj(T v)
        {
            if (names.ContainsKey(v))
            {
                return vertex[names[v]];
            }
            return null;
        }

        public List<Edge> Adj(int v)
        {
            return Adj(ids[v]);
        }

        public Dijkstra<T> Path(T from, T to)
        {
            Dijkstra<T> d = new Dijkstra<T>(this, names[from], names[to]);
            return d;
        }

        public List<Edge> Path(Dijkstra<T> d, T to)
        {
            List<Edge> p = new List<Edge>();

            Stack<Edge> st = d.PathTo(names[to]);

            foreach (Edge item in st)
            {
                p.Add(item);
            }

            return p;
        }

        public double DistanceTo(Dijkstra<T> d, T to)
        {
            return d.DistTo(names[to]);
        }


        public List<T> AdjS(T v)
        {
            List<Edge> aa = Adj(v);
            List<T> ed = new List<T>();

            int w = names[v];
            foreach (Edge item in aa)
            {
                ed.Add(ids[item.Other(w)]);
            }
            return ed;
            /*if (names.ContainsKey(v))
            {
                int w = names[v];
                //return vertex[names[v]];
                foreach (Edge item in vertex[w])
                {
                    int ot = item.Other(w);
                    ed.Add(ids[ot]);
                }
                return ed;
            }
            return null;*/
        }

        public List<Edge> Edges()
        {
            List<Edge> list = new List<Edge>();
            foreach (List<Edge> vertices in vertex)
            {
                foreach (Edge edge in vertices)
                {
                    list.Add(edge);
                }
            }
            return list;
        }

        public List<T> Vertices()
        {
            List<T> list = new List<T>();
            foreach (KeyValuePair<T, int> item in names)
            {
                list.Add(item.Key);
            }
            return list;
        }

        public List<string> EdgesS()
        {
            List<Edge> list = Edges();
            List<string> list2 = new List<string>();
            foreach (Edge v in list)
            {
                int u = v.Either();
                int w = v.Other(u);
                list2.Add(ids[u] + "->" + ids[w] + ": " + v.Weight);
            }
            return list2;
        }

        public void AddEdge(Edge e)
        {
            int u = e.Either();
            int w = e.Other(u);
            while (vertex.Count <= u || vertex.Count <= w)
            {
                vertex.Add(new List<Edge>());
            }
            vertex[u].Add(e);
            vertex[w].Add(e);
            edges++;
        }

        public T GetVertex(int v)
        {
            return ids[v];
        }

        public int GetVertex(T v)
        {
            return names[v];
        }

    }
}
