using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unidad3.Viajero
{
    public partial class Salesman : Form
    {
        private List<Point> points;
        private bool finding;
        private double dist;
        private long fact;
        private bool greedy;
        private int actu;
        private int[] ind;
        private int[] bestI;
        private bool[] ap;
        private List<int> gPoints;
        private int gAct;

        public Salesman()
        {
            InitializeComponent();
            points = new List<Point>();
            finding = false;
            actu = 1;
            dist = double.PositiveInfinity;
            fact = 0;
            greedy = false;
            button1.Enabled = false;
            button2.Enabled = false;
            points = new List<Point>();

        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X)) + ((p1.Y - p2.Y) * (p1.Y - p2.Y)));
        }

        private double Best(List<Point> points)
        {
            ind = new int[points.Count - 1];
            bestI = new int[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                bestI[i] = i;
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                ind[i] = i + 1;
            }
            double distance = double.PositiveInfinity;

            do
            {
                double distA = 0;
                distA += Distance(points[0], points[ind[0]]);
                for (int i = 0; i < points.Count - 2; i++)
                {
                    distA += Distance(points[ind[i]], points[ind[i + 1]]);
                }

                distA += Distance(points[ind[ind.Length - 1]], points[0]);

                if (distA < distance)
                {
                    Array.Copy(ind, 0, bestI, 1, ind.Length);
                    distance = distA;
                }
            } while (Permutation.NextPermutation(ind));

            return distance;
        }

        private double BestG(List<Point> points)
        {
            ind = new int[points.Count - 1];
            bestI = new int[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                bestI[i] = i;
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                ind[i] = i + 1;
            }
            double distance = 0;

            bool[] ap = new bool[points.Count];
            ap[0] = true;
            int coun = 0;
            int act = 0;
            while (true)
            {
                double less = double.PositiveInfinity;
                int ind = -1;
                for (int i = 0; i < points.Count; i++)
                {
                    if (!ap[i])
                    {
                        if (Distance(points[act], points[i]) < less)
                        {
                            ind = i;
                            less = Distance(points[act], points[i]);
                        }
                    }
                }
                if (ind == -1)
                {
                    distance += Distance(points[0], points[act]);
                    break;
                }
                distance += less;
                ap[ind] = true;
                act = ind;
                coun++;
            }

            return distance;
        }

        private void UpdateAlll()
        {
            if (!finding)
            {
                return;
            }
            if (!greedy)
            {


                double distA = 0;
                distA += Distance(points[0], points[ind[0]]);
                for (int i = 0; i < points.Count - 2; i++)
                {
                    distA += Distance(points[ind[i]], points[ind[i + 1]]);
                }

                distA += Distance(points[ind[ind.Length - 1]], points[0]);

                if (distA < dist)
                {
                    //Array.Copy(ind, bestI, points.Count);
                    Array.Copy(ind, 0, bestI, 1, ind.Length);
                    dist = distA;
                }

            }
            else
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            doubleBufferedPanel1.Invalidate();
        }

        private void doubleBufferedPanel1_Paint(object sender, PaintEventArgs e)
        {

            UpdateAlll();
            Graphics g = e.Graphics;
            int z = 0;
            foreach (Point item in points)
            {
                g.FillEllipse(Brushes.Black, item.X - 5, item.Y - 5, 10, 10);
                g.DrawString((char)('A' + z) + "", new Font("Arial", 12), Brushes.Black, item.X, item.Y);
                z++;
            }
            if (!greedy)
            {
                if (!finding)
                {
                    return;
                }
                if (!Permutation.NextPermutation(ind))
                {
                    //g.DrawString("Termino " + dist, new Font("Arial", 12), Brushes.Black, 80, 80);
                    string res = "";

                    for (int i = 0; i < bestI.Length; i++)
                    {
                        res += (char)(bestI[i] + 'A');
                    }
                    res += (char)(bestI[0] + 'A');

                    g.DrawString("Termino " + res, new Font("Arial", 12), Brushes.Black, 80, 80);
                }
                else
                {
                    int per = (int)((actu / (double)fact) * 100);
                    g.DrawString("Buscando " + per + "%", new Font("Arial", 12), Brushes.Black, 80, 80);
                    actu++;
                }

                for (int i = 0; i < points.Count - 1; i++)
                {
                    g.DrawLine(Pens.Black, points[bestI[i]], points[bestI[i + 1]]);
                }
                g.DrawLine(Pens.Black, points[bestI[0]], points[bestI[bestI.Length - 1]]);
                if (!checkBox1.Checked) return;
                int max = -100;
                int min = 9999999;
                foreach (Point p in points)
                {
                    max = Math.Max(max, p.Y);
                    min = Math.Min(min, p.Y);
                }

                int offset = max - min + 15;
                Point p1 = points[0];
                Point p2 = points[ind[0]];
                g.DrawLine(Pens.Blue, p1.X, p1.Y + offset, p2.X, p2.Y + offset);
                for (int i = 0; i < points.Count - 2; i++)
                {
                    p1 = points[ind[i]];
                    p2 = points[ind[i + 1]];
                    g.DrawLine(Pens.Blue, p1.X, p1.Y + offset, p2.X, p2.Y + offset);
                }
            }
            else
            {
                double dis = double.PositiveInfinity;
                int less = -1;
                for (int i = 0; i < points.Count; i++)
                {
                    if (!ap[i])
                    {
                        if (dis > Distance(points[gAct], points[i]))
                        {
                            dis = Distance(points[gAct], points[i]);
                            less = i;
                        }
                    }
                }
                if (less != -1)
                {
                    gPoints.Add(less);
                    ap[less] = true;
                    gAct = less;
                }
                else
                {
                    g.DrawLine(Pens.Black, points[0], points[gPoints[gPoints.Count - 1]]);
                    if (dist == double.PositiveInfinity || true)
                    {
                        double sz = 0;
                        for (int i = 0; i < gPoints.Count - 1; i++)
                        {
                            sz += Distance(points[gPoints[i]], points[gPoints[i + 1]]);
                        }
                        sz += Distance(points[0], points[gPoints[gPoints.Count - 1]]);
                        dist = sz;
                    }
                    //g.DrawString("Termino " + dist, new Font("Arial", 12), Brushes.Black, 80, 80);
                    string res = "";

                    for (int i = 0; i < gPoints.Count; i++)
                    {
                        res += (char)(gPoints[i] + 'A');
                    }
                    res += (char)(gPoints[0] + 'A');
                    g.DrawString("Termino " + res, new Font("Arial", 12), Brushes.Black, 80, 80);
                    g.DrawString("Termino", new Font("Arial", 12), Brushes.Black, 80, 80);
                }

                for (int i = 0; i < gPoints.Count - 1; i++)
                {
                    g.DrawLine(Pens.Black, points[gPoints[i]], points[gPoints[i + 1]]);
                }
            }
        }

        private void doubleBufferedPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (finding)
            {
                return;
            }
            points.Add(new Point(e.X, e.Y));
            if (points.Count > 2)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            doubleBufferedPanel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            finding = true;

            ind = new int[points.Count - 1];
            bestI = new int[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                bestI[i] = i;
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                ind[i] = i + 1;
            }

            for (int i = 0; i < points.Count; i++)
            {
                bestI[i] = i;
            }

            fact = 1;
            for (int i = 2; i < points.Count; i++)
            {
                fact *= i;
            }

            if (!checkBox1.Checked)
            {
                dist = Best(points);
                finding = true;
                doubleBufferedPanel1.Invalidate();

                return;
            }


        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            finding = true;

            greedy = true;
            gAct = 0;
            ap = new bool[points.Count];
            gPoints = new List<int>();
            gPoints.Add(0);
            ap[0] = true;
            timer1.Interval = 100;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            finding = false;
            points = new List<Point>();
            actu = 1;
            dist = double.PositiveInfinity;
            fact = 0;
            greedy = false;
            doubleBufferedPanel1.Invalidate();
        }
    }
}
