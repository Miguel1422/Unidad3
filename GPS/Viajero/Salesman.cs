using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS.Viajero
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
        public Salesman()
        {
            InitializeComponent();
            points = new List<Point>();
            finding = false;
            actu = 1;
            dist = double.PositiveInfinity;
            fact = 0;
            greedy = false;
            /*Random r = new Random();
            for (int z = 0; z < 100000; z++)
            {

                for (int i = 0; i < 5; i++)
                {
                    points.Add(new Point(r.Next(30), r.Next(30)));
                }

                double best = Best(points);
                double bestG = BestG(points);

                if (bestG < best)
                {
                    Console.WriteLine(best);
                    Console.WriteLine(bestG);
                    Console.WriteLine();
                    throw new Exception();
                }

                points.Clear();
            }*/


        }

        private double SqrDistance(Point p1, Point p2)
        {
            return ((p1.X - p2.X) * (p1.X - p2.X)) + ((p1.Y - p2.Y) * (p1.Y - p2.Y));
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
                distA += SqrDistance(points[0], points[ind[0]]);
                for (int i = 0; i < points.Count - 2; i++)
                {
                    distA += SqrDistance(points[ind[i]], points[ind[i + 1]]);
                }

                distA += SqrDistance(points[ind[ind.Length - 1]], points[0]);

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
                        if (SqrDistance(points[act], points[i]) < less)
                        {
                            ind = i;
                            less = SqrDistance(points[act], points[i]);
                        }
                    }
                }
                if (ind == -1)
                {
                    distance += SqrDistance(points[0], points[act]);
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
                distA += SqrDistance(points[0], points[ind[0]]);
                for (int i = 0; i < points.Count - 2; i++)
                {
                    distA += SqrDistance(points[ind[i]], points[ind[i + 1]]);
                }

                distA += SqrDistance(points[ind[ind.Length - 1]], points[0]);

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
            foreach (Point item in points)
            {
                g.FillEllipse(Brushes.Black, item.X - 5, item.Y - 5, 10, 10);
            }
            if (!greedy)
            {
                if (!finding)
                {
                    return;
                }
                if (!Permutation.NextPermutation(ind))
                {
                    g.DrawString("Termino " + dist, new Font("Arial", 12), Brushes.Black, 80, 80);
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
                        if (dis > SqrDistance(points[gAct], points[i]))
                        {
                            dis = SqrDistance(points[gAct], points[i]);
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
                            sz += SqrDistance(points[gPoints[i]], points[gPoints[i + 1]]);
                        }
                        sz += SqrDistance(points[0], points[gPoints[gPoints.Count - 1]]);
                        dist = sz;
                    }
                    g.DrawString("Termino " + dist, new Font("Arial", 12), Brushes.Black, 80, 80);
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
            doubleBufferedPanel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
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

                /*do
                {
                    double distA = 0;
                    distA += SqrDistance(points[0], points[ind[0]]);
                    for (int i = 0; i < points.Count - 2; i++)
                    {
                        distA += SqrDistance(points[ind[i]], points[ind[i + 1]]);
                    }

                    distA += SqrDistance(points[ind[ind.Length - 1]], points[0]);

                    if (distA < dist)
                    {
                        //Array.Copy(ind, bestI, points.Count);
                        Array.Copy(ind, 0, bestI, 1, ind.Length);
                        dist = distA;
                    }
                } while (Permutation.NextPermutation(ind));*/
                dist = Best(points);
                finding = true;
                doubleBufferedPanel1.Invalidate();

                return;
            }

            timer1.Enabled = true;
            button1.Enabled = false;
            finding = true;
        }

        private bool[] ap;
        private List<int> gPoints;
        private int gAct;
        private void button2_Click(object sender, EventArgs e)
        {
            greedy = true;
            gAct = 0;
            ap = new bool[points.Count];
            gPoints = new List<int>();
            gPoints.Add(0);
            ap[0] = true;
            timer1.Enabled = true;
            button1.Enabled = false;
            finding = true;

            timer1.Interval = 100;

        }
    }
}
