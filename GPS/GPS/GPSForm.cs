using GPS.GPS.Util;
using GPS.GPS.Util.KdTree;
using GPS.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS.GPS
{
    public partial class GPSForm : Form
    {
        private Parser p;
        private List<City> ciudades;
        private WeightedGraph<Node> G;
        private Dictionary<long, Node> nodeById;
        private List<Node> nodos;
        private List<Way> carreteras;
        private Dictionary<Node, List<Way>> waysByNode;
        private Dictionary<string, City> ciudad;
        private KdTree kd;
        private List<Edge> path;
        private Dijkstra<Node> dijk;
        private Image mapC;
        public GPSForm()
        {
            InitializeComponent();
            mapC = Image.FromFile(Parser.IMG);
            ciudad = new Dictionary<string, City>();
            p = new Parser();
            kd = new KdTree();
            ciudades = p.Cities;
            dijk = null;
            path = new List<Edge>();
            G = p.Graph;
            nodeById = p.NodeById;
            nodos = p.Nodes;
            carreteras = p.Ways;
            waysByNode = p.WaysByNode;
            foreach (City item in p.Cities)
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


            foreach (Node item in G.Vertices())
            {
                kd.insert(new Point2D(item));
            }
            kd.run();
            comboBox1.Sorted = true;
            comboBox2.Sorted = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string c1 = comboBox1.SelectedItem.ToString();
            string c2 = comboBox2.SelectedItem.ToString();
            City ca = ciudad[c1];
            City cb = ciudad[c2];
            Node n1 = kd.nearest(new Point2D(ca.Position)).GetNode;
            Node n2 = kd.nearest(new Point2D(cb.Position)).GetNode;
            DateTime a = DateTime.Now;
            dijk = G.Path(n1, n2);
            selected = ca.Position;
            selected2 = cb.Position;
            path = G.Path(dijk, n2);
            Console.WriteLine("Distancia " + G.DistanceTo(dijk, n2));
            Console.WriteLine("Tiempo " + (DateTime.Now - a));
            foreach (Edge item in path)
            {
                //Console.WriteLine(item);
            }
            doubleBufferedPanel1.Invalidate();

            Console.WriteLine(c1);


        }


        public static double map(double min, double max, double rMin, double rMax, double val)
        {

            if (val < min || val > max)
            {
                throw new Exception("Fuera de rango");
            }

            double range = max - min;
            double resRange = rMax - rMin;

            double res = resRange * (val - min) / range;

            return res + rMin;
        }

        private void doubleBufferedPanel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            /*foreach (Way w in carreteras)
            {

                for (int i = 1; i < w.Nodes.Count; i++)
                {
                    Node a = nodeById[w.Nodes[i - 1]];
                    Node b = nodeById[w.Nodes[i]];

                    float x1 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, a.Longitude);
                    float y1 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, a.Latitude);

                    float x2 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, b.Longitude);
                    float y2 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, b.Latitude);

                    g.DrawLine(Pens.Black, x1, doubleBufferedPanel1.Height - y1, x2, doubleBufferedPanel1.Height - y2);
                }

            }*/

            g.DrawImage(mapC, 0, 0);


            foreach (Edge item in path)
            {
                Node a = G.GetVertex(item.Either());
                Node b = G.GetVertex(item.Other(item.Either()));
                float x1 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, a.Longitude);
                float y1 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, a.Latitude);

                float x2 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, b.Longitude);
                float y2 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, b.Latitude);

                g.DrawLine(Pens.Red, x1, doubleBufferedPanel1.Height - y1, x2, doubleBufferedPanel1.Height - y2);
            }

            foreach (City item in ciudades)
            {
                Node a = (item.Position);

                float x1 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, a.Longitude);
                float y1 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, a.Latitude);


                //g.FillEllipse(Brushes.Blue, x1, doubleBufferedPanel1.Height - y1, 5, 5);
            }
            if (selected != null)
            {
                float x1 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, selected.Longitude);
                float y1 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, selected.Latitude);
                g.FillEllipse(Brushes.Green, x1, doubleBufferedPanel1.Height - y1, 10, 10);
            }
            if (selected2 != null)
            {
                float x1 = (float)map(p.XMin, p.XMax, 0, doubleBufferedPanel1.Width, selected2.Longitude);
                float y1 = (float)map(p.YMin, p.YMax, 0, doubleBufferedPanel1.Height, selected2.Latitude);
                g.FillEllipse(Brushes.Blue, x1, doubleBufferedPanel1.Height - y1, 10, 10);
            }
        }
        private Node selected = null;
        private Node selected2 = null;
        private void doubleBufferedPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float x1 = (float)map(0, doubleBufferedPanel1.Width, p.XMin, p.XMax, e.X);
                float y1 = (float)map(0, doubleBufferedPanel1.Height, p.YMin, p.YMax, doubleBufferedPanel1.Height - e.Y);

                Point2D pp = kd.nearest(new Point2D(y1, x1));
                Node n1 = pp.GetNode;
                //Node n1 = new Node(1232, y1, x1);
                selected = n1;
                string c1 = "Torreón";
                /*City ca = ciudad[c1];
                Node n2 = kd.nearest(new Point2D(ca.Position)).GetNode;
                dijk = G.Path(n1, n2);
                path = G.Path(dijk, n2);*/
            }
            else
            {
                float x1 = (float)map(0, doubleBufferedPanel1.Width, p.XMin, p.XMax, e.X);
                float y1 = (float)map(0, doubleBufferedPanel1.Height, p.YMin, p.YMax, doubleBufferedPanel1.Height - e.Y);

                Point2D pp = kd.nearest(new Point2D(y1, x1));
                Node n2 = pp.GetNode;
                selected2 = n2;
                //Node n1 = new Node(1232, y1, x1);
                dijk = G.Path(selected, n2);
                path = G.Path(dijk, n2);
                Console.WriteLine(G.DistanceTo(dijk, n2));
            }
            doubleBufferedPanel1.Invalidate();

        }
    }
}
