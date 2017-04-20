using Unidad3.GPS.Util;
using Unidad3.GPS.Util.KdTree;
using Unidad3.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GPS.GPS;

namespace Unidad3.GPS
{
    public partial class GPSForm : Form
    {
        private Parser parser;
        private List<City> ciudades;
        private WeightedGraph<Node> graph;
        private Dictionary<long, Node> nodeById;
        private List<Node> nodos;
        private List<Way> carreteras;
        private Dictionary<Node, List<Way>> waysByNode;
        private Dictionary<string, City> ciudad;
        private KdTree kd;
        private List<Edge> path;
        private Dijkstra<Node> dijk;
        private Image mapC;
        private Node from;
        private Node to;
        private string fromS;
        private string toS;
        private double mapX;
        private double mapY;
        private double minX;
        private double maxX;
        private double minY;
        private double maxY;
        /***
         * Drag and zoom
         */
        private bool dragging = false;

        private double xA = 0;
        private double yA = 0;

        private int xI = 0;
        private int yI = 0;
        private bool moving = false;
        private float zoom = 0.4f;
        private float dx = 0;
        private float dy = 0;
        private DateTime prev;

        public GPSForm()
        {
            InitializeComponent();
            parser = new Parser();

            minX = parser.XMin;
            minY = parser.YMin;
            maxX = parser.XMax;
            maxY = parser.YMax;

            mapC = Image.FromFile(Parser.IMG_MAP);

            mapX = mapC.Width;
            mapY = mapC.Height;


            ciudad = new Dictionary<string, City>();

            kd = new KdTree();
            ciudades = parser.Cities;
            dijk = null;
            path = new List<Edge>();
            graph = parser.Graph;
            nodeById = parser.NodeById;
            nodos = parser.Nodes;
            carreteras = parser.Ways;
            waysByNode = parser.WaysByNode;
            foreach (City item in parser.Cities)
            {
                string name = item.Name;
                int i = 2;
                while (ciudad.ContainsKey(name))
                {
                    name = item.Name + " (" + i++ + ")";
                }
                ciudad.Add(name, item);
                comboBox1.Items.Add(name);
                comboBox2.Items.Add(name);
            }


            foreach (Node item in graph.Vertices())
            {
                kd.insert(new Point2D(item));
            }
            comboBox1.Sorted = true;
            comboBox2.Sorted = true;

            prev = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string c1 = comboBox1.SelectedItem.ToString();
            string c2 = comboBox2.SelectedItem.ToString();
            City ca = ciudad[c1];
            City cb = ciudad[c2];
            Node n1 = kd.nearest(new Point2D(ca.NodePosition)).GetNode;
            Node n2 = kd.nearest(new Point2D(cb.NodePosition)).GetNode;
            DateTime a = DateTime.Now;
            from = n1;
            to = n2;
            fromS = c1;
            toS = c2;
            dijk = GeneratePath(n1, n2);
            path = GetPath(dijk, n2);
            Console.WriteLine("Distancia " + graph.DistanceTo(dijk, n2));
            Console.WriteLine("Tiempo " + (DateTime.Now - a));
            foreach (Edge item in path)
            {
                //Console.WriteLine(item);
            }
            doubleBufferedPanel1.Invalidate();

        }

        private Dijkstra<Node> GeneratePath(Node from, Node to)
        {
            Dijkstra<Node> temp = graph.GetDijkstra(from, to);
            return temp;
        }

        private List<Edge> GetPath(Dijkstra<Node> dijkstra, Node to)
        {
            List<Edge> l = graph.Path(dijk, to);
            return l;
        }

        public static double map(double min, double max, double rMin, double rMax, double val)
        {

            if (val < min || val > max)
            {
                //throw new Exception("Fuera de rango");
            }

            double range = max - min;
            double resRange = rMax - rMin;

            double res = resRange * (val - min) / range;

            return res + rMin;
        }

        private void DrawWays(Graphics g)
        {
            foreach (Way w in carreteras)
            {

                for (int i = 1; i < w.Nodes.Count; i++)
                {
                    Node a = nodeById[w.Nodes[i - 1]];
                    Node b = nodeById[w.Nodes[i]];

                    float x1 = (float)map(minX, maxX, 0, mapX, a.Longitude);
                    float y1 = (float)map(minY, maxY, 0, mapY, a.Latitude);

                    float x2 = (float)map(minX, maxX, 0, mapX, b.Longitude);
                    float y2 = (float)map(minY, maxY, 0, mapY, b.Latitude);

                    g.DrawLine(Pens.Black, x1, (float)(mapY - y1), x2, (float)(mapY - y2));
                }

            }
        }
        private void DrawPath(Graphics g)
        {
            Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 6);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Edge item in path)
            {
                Node a = graph.GetVertex(item.Either());
                Node b = graph.GetVertex(item.Other(item.Either()));
                float x1 = (float)map(minX, maxX, 0, mapX, a.Longitude);
                float y1 = (float)map(minY, maxY, 0, mapY, a.Latitude);

                float x2 = (float)map(minX, maxX, 0, mapX, b.Longitude);
                float y2 = (float)map(minY, maxY, 0, mapY, b.Latitude);

                g.DrawLine(redPen, x1, (float)(mapY - y1), x2, (float)(mapY - y2));
            }
        }
        private void DrawCities(Graphics g)
        {
            foreach (City item in ciudades)
            {
                Node a = (item.NodePosition);

                float x1 = (float)map(minX, maxX, 0, mapX, a.Longitude);
                float y1 = (float)map(minY, maxY, 0, mapY, a.Latitude);
                g.FillEllipse(Brushes.Blue, x1, (float)(mapY - y1), 5, 5);
            }

        }
        private void DrawDestination(Graphics g)
        {
            float mult = (float)map(0, 10, 2.5, 1, trackBar1.Value);
            float tam = 10 * mult;
            if (from != null)
            {
                float x1 = (float)map(minX, maxX, 0, mapX, from.Longitude);
                float y1 = (float)map(minY, maxY, 0, mapY, from.Latitude);

                g.FillEllipse(Brushes.Green, x1 - tam / 2, (float)(mapY - y1 - tam / 2), tam, tam);
                g.DrawString("Desde: " + fromS, new Font("Arial", 12 * mult), Brushes.Black, x1, (float)(mapY - y1));
            }
            if (to != null)
            {
                float x1 = (float)map(minX, maxX, 0, mapX, to.Longitude);
                float y1 = (float)map(minY, maxY, 0, mapY, to.Latitude);
                g.FillEllipse(Brushes.Blue, x1 - tam / 2, (float)(mapY - y1 - tam / 2), tam, tam);
                g.DrawString("Hacia: " + toS, new Font("Arial", 12 * mult), Brushes.Black, x1, (float)(mapY - y1));
            }
        }

        private void doubleBufferedPanel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(dx * zoom, dy * zoom);
            g.ScaleTransform(zoom, zoom);
            g.DrawImage(mapC, 0, 0, (float)mapX, (float)mapY);
            DrawPath(g);
            DrawDestination(g);
            //DrawCities(g);
            //DrawWays(g);
        }

        private void doubleBufferedPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {

                float x1 = (float)map(0, mapX, minX, maxX, ((e.X) / zoom) - xA);
                float y1 = (float)map(0, mapY, minY, maxY, mapY - ((e.Y) / zoom - yA));
                Point2D pp = kd.nearest(new Point2D(y1, x1));
                Node n1 = pp.GetNode;
                from = n1;
                fromS = WaysByNode(n1);

                if (to != null)
                {
                    dijk = graph.GetDijkstra(from, to);
                    path = GetPath(dijk, to);
                    Console.WriteLine(graph.DistanceTo(dijk, to));
                }
            }
            else
            {
                float x1 = (float)map(0, mapX, minX, maxX, ((e.X) / zoom) - xA);
                float y1 = (float)map(0, mapY, minY, maxY, mapY - ((e.Y) / zoom - yA));

                Point2D pp = kd.nearest(new Point2D(y1, x1));
                Node n2 = pp.GetNode;
                to = n2;
                toS = WaysByNode(n2);
                if (from != null)
                {
                    dijk = graph.GetDijkstra(from, to);
                    path = GetPath(dijk, to);
                    Console.WriteLine(graph.DistanceTo(dijk, to));
                }

            }
            doubleBufferedPanel1.Invalidate();

        }

        private void doubleBufferedPanel1_MouseDown(object sender, MouseEventArgs e)
        {

            switch (e.Button)
            {
                case MouseButtons.Left:
                    dragging = true;
                    xI = e.X;
                    yI = e.Y;
                    Cursor = Cursors.SizeAll;
                    break;
                
            }

            base.OnMouseDown(e);
        }

        private void doubleBufferedPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                dx = (int)(e.X - xI + xA);
                dy = (int)(e.Y - yI + yA);
                moving = true;
                DateTime ac = DateTime.Now;
                if ((ac - prev).TotalMilliseconds > 40)
                {
                    doubleBufferedPanel1.Invalidate();
                    prev = ac;
                }
            }


            base.OnMouseMove(e);
        }

        private void doubleBufferedPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            // if move mode on
            if (dragging)
            {
                xA = e.X - xI + xA;
                yA = e.Y - yI + yA;
                dragging = false;
                Cursor = Cursors.Default;
                moving = false;
            }
            moving = false;
            base.OnMouseUp(e);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            doubleBufferedPanel1.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoom = (float)map(0, 10, 0.4, 1.2, trackBar1.Value);
            doubleBufferedPanel1.Invalidate();
        }

        private bool NodeContains(Node n, string calle)
        {
            foreach (Way item in waysByNode[n])
            {
                if (item.Name.Equals(calle))
                {
                    return true;
                }
            }
            return false;
        }

        private string StreetBetween(Node a, Node b)
        {
            List<Way> pr = waysByNode[a];
            List<Way> ac = waysByNode[b];
            string calle = null;
            foreach (Way w in pr)
            {
                string s1 = w.Name.Equals("null") ? w.ID + "" : w.Name;
                foreach (Way w2 in ac)
                {
                    string s2 = w2.Name.Equals("null") ? w2.ID + "" : w2.Name;
                    if (s2.Equals(s1))
                    {
                        calle = s2;
                        return calle;
                    }
                }
            }

            throw new Exception("No hay calles");
        }
        private string WaysByNode(Node n)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Way w in waysByNode[n])
            {
                sb.Append(w.Name + ", ");
            }
            return sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                Node prev = from;
                List<string> camino = new List<string>();
                double totalDistance = 0;
                //foreach (Edge item in path)
                for (int i = 0; i < path.Count;)
                {
                    Edge item = path[i];
                    Node a = graph.GetVertex(item.Either());
                    Node b = graph.GetVertex(item.Other(item.Either()));
                    Node nue = (prev == a) ? b : a;
                    string calle = StreetBetween(prev, nue);
                    //Console.WriteLine(calle);
                    int k = i + 1;
                    double distance = 0;//parser.Distance(prev, last);
                    distance += parser.Distance(prev, nue);
                    Node ant = nue;
                    while (k < path.Count)
                    {
                        Edge siguiente = path[k];
                        Node n1 = graph.GetVertex(siguiente.Either());
                        Node n2 = graph.GetVertex(siguiente.Other(siguiente.Either()));

                        Node nueAux = n1 == ant ? n2 : n1;

                        if (NodeContains(nueAux, calle))
                        {
                            distance += parser.Distance(ant, nueAux);
                            ant = nueAux;
                        }
                        else
                        {
                            break;
                        }

                        k++;
                    }
                    i = k;
                    StringBuilder sb = new StringBuilder();
                    //Console.Write("Desde ");
                    sb.Append("Desde ");
                    foreach (Way w in waysByNode[prev])
                    {
                        //Console.Write(w.Name + ", ");
                        sb.Append(w.Name + ", ");
                    }
                    //Console.WriteLine("\nContinue por{0} Hasta: ", calle);
                    string dis = distance > 1000 ? String.Format("{0:0.00}Km", distance / 1000) : String.Format("{0:0.00}m", distance);
                    sb.AppendFormat("\nContinue por {0} {1} Hasta: \n", calle, dis);
                    sb.Append(WaysByNode(ant));
                    /*foreach (Way w in waysByNode[last])
                    {
                        //Console.Write(w.Name + ", ");
                        sb.Append(w.Name + ", ");
                    }*/
                    //Console.WriteLine();
                    sb.Append("\n");

                    //Console.WriteLine(sb.ToString());
                    //Console.WriteLine("************");
                    camino.Add(sb.ToString());
                    camino.Add("********************************");
                    prev = ant;
                    totalDistance += distance;
                }
                double asd = graph.DistanceTo(dijk, to);
                string disS = totalDistance > 1000 ? String.Format("{0:0.00}Km", totalDistance / 1000) : String.Format("{0:0.00}m", totalDistance);
                camino.Add(String.Format("Ha llegado a su destino en {0}", disS));

                if (Math.Abs(totalDistance - asd) > 1)
                {
                    Console.WriteLine("Error");
                }
                else
                {
                    Console.WriteLine("todo bien " + asd);
                }

                PathFrm p = new PathFrm(camino);
                p.ShowDialog();
            }
        }

    }
}
