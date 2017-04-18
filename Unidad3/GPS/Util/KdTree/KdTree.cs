using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3.GPS.Util.KdTree
{
    public class KdTree
    {
        private Node root;

        public KdTree()
        {
            this.root = null;
        }

        public void run()
        {
            run(root);
        }
        private void run(Node node)
        {
            if (node == null)
            {
                return;
            }
            if (node.p.GetNode == null)
            {
                int a = 3;
            }
            
            run(node.left);
            run(node.right);
            
        }

        public bool isEmpty()
        {
            return root == null;
        }

        public int size()
        {
            return size(root);
        }

        private int size(Node n)
        {
            if (n == null)
            {
                return 0;
            }
            return n.size;
        }

        public void insert(Point2D p)
        {
            if (p == null)
            {
                throw new Exception();
            }
            root = insert(root, p, true);
        }

        private Node insert(Node n, Point2D p, bool vertical)
        {
            if (n == null)
            {
                return new Node(p, vertical);
            }
            if (p.Equals(n.p))
            {
                return n;
            }
            if (n.compareTo(p) > 0)
            {
                n.left = insert(n.left, p, !vertical);
            }
            else
            {
                n.right = insert(n.right, p, !vertical);
            }
            n.size = 1 + size(n.left) + size(n.right);
            return n;
        }

        public bool contains(Point2D p)
        {
            if (p == null)
            {
                throw new Exception();
            }
            Node aux = root;
            while (aux != null)
            {

                if (p.Equals(aux.p))
                {
                    return true;
                }
                if (aux.compareTo(p) > 0)
                {
                    aux = aux.left;
                }
                else if (aux.compareTo(p) < 0)
                {
                    aux = aux.right;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        
        public IEnumerator<Point2D> range(RectHV rect)
        {
            if (rect == null)
            {
                throw new Exception();
            }
            Queue<Point2D> q = new Queue<Point2D>();
            rangeM(root, rect, new RectHV(0, 0, 1, 1), q);
            foreach (Point2D item in q)
            {
                yield return item;
            }
        }

        private void rangeM(Node n, RectHV range, RectHV subrange, Queue<Point2D> q)
        {
            if (n == null)
            {
                return;
            }
            if (!range.intersects(subrange))
            {
                return;
            }
            if (range.contains(n.p))
            {
                q.Enqueue(n.p);
            }
            if (n.vertical)
            {
                RectHV one = new RectHV(subrange.Xmin, subrange.Ymin, n.x, subrange.Ymax);
                RectHV tw = new RectHV(n.x, subrange.Ymin, subrange.Xmax, subrange.Ymax);
                if (one.intersects(range))
                {
                    rangeM(n.left, range, one, q);
                }
                if (tw.intersects(range))
                {
                    rangeM(n.right, range, tw, q);
                }

            }
            else
            {
                RectHV one = new RectHV(subrange.Xmin, subrange.Ymin, subrange.Xmax, n.y);
                RectHV tw = new RectHV(subrange.Xmin, n.y, subrange.Xmax, subrange.Ymax);
                if (one.intersects(range))
                {
                    rangeM(n.left, range, one, q);
                }
                if (tw.intersects(range))
                {
                    rangeM(n.right, range, tw, q);
                }
            }
        }

        public Point2D nearest(Point2D p)
        {
            run();
            if (root == null)
            {
                return null;
            }
            if (p == null)
            {
                throw new Exception();
            }
            Node near = new Node(root.x, root.y, root.vertical);
            near.p.GetNode = root.p.GetNode;
            nearest(root, p, new RectHV(-200, -200, 200, 200), near);
            return near.p;
        }

        private void nearest(Node candidate, Point2D goal, RectHV subrange, Node neares)
        {
            if (candidate == null)
            {
                return;
            }
            if (candidate.p.distanceTo(goal) < neares.p.distanceTo(goal))
            {
                neares.p = candidate.p;
                neares.p.GetNode = candidate.p.GetNode;
            }
            if (candidate.vertical)
            {
                RectHV one = new RectHV(subrange.Xmin, subrange.Ymin, candidate.x, subrange.Ymax);
                RectHV tw = new RectHV(candidate.x, subrange.Ymin, subrange.Xmax, subrange.Ymax);
                if (one.contains(goal))
                {
                    nearest(candidate.left, goal, one, neares);
                    if (tw.distanceTo(goal) < neares.p.distanceTo(goal))
                    {
                        nearest(candidate.right, goal, tw, neares);
                    }

                }
                else
                {
                    nearest(candidate.right, goal, tw, neares);
                    if (one.distanceTo(goal) < neares.p.distanceTo(goal))
                    {
                        nearest(candidate.left, goal, one, neares);
                    }
                }

            }
            else
            {
                RectHV one = new RectHV(subrange.Xmin, subrange.Ymin, subrange.Xmax, candidate.y);
                RectHV tw = new RectHV(subrange.Xmin, candidate.y, subrange.Xmax, subrange.Ymax);
                if (one.contains(goal))
                {
                    nearest(candidate.left, goal, one, neares);

                    if (tw.distanceTo(goal) < neares.p.distanceTo(goal))
                    {
                        nearest(candidate.right, goal, tw, neares);
                    }
                }
                else
                {
                    nearest(candidate.right, goal, tw, neares);

                    if (one.distanceTo(goal) < neares.p.distanceTo(goal))
                    {
                        nearest(candidate.left, goal, one, neares);
                    }
                }
            }

        }

        private class Node
        {

            public Node left;
            public Node right;

            public double x;
            public double y;
            public Point2D p;

            public int size;

            public bool vertical;

            public Node(double x, double y, bool vertical) : this(new Point2D(x, y), vertical)
            {
            }

            public Node(Point2D p, bool vertical)
            {
                this.left = null;
                this.right = null;
                this.x = p.X;
                this.y = p.Y;
                this.vertical = vertical;
                this.size = 1;

                this.p = p;
            }

            public int compareTo(Point2D o)
            {
                double eps = 1e-12;
                if (vertical)
                {
                    if (this.x > o.X)
                    {
                        return 1;
                    }
                    if (this.x < o.X)
                    {
                        return -1;
                    }
                    if (Math.Abs(this.y - o.Y) < eps)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }

                if (this.y > o.Y)
                {
                    return 1;
                }
                if (this.y < o.Y)
                {
                    return -1;
                }
                if (Math.Abs(this.x - o.X) < eps)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }

            }
        }
    }
}
