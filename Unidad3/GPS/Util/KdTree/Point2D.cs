using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3.GPS.Util.KdTree
{
    public class Point2D
    {
        private double x;    // x coordinate
        private double y;    // y coordinate
        private Node node;

        /**
         * Initializes a new point (x, y).
         * @param x the x-coordinate
         * @param y the y-coordinate
         * @throws IllegalArgumentException if either {@code x} or {@code y}
         *    is {@code Double.NaN}, {@code Double.POSITIVE_INFINITY} or
         *    {@code Double.NEGATIVE_INFINITY}
         */
        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point2D(Node n)
        {
            this.x = n.Latitude;
            this.y = n.Longitude;
            this.node = n;
        }

        public Node GetNode
        {
            get { return node; }
            set { node = value; }
        }
        /**
         * Returns the x-coordinate.
         * @return the x-coordinate
         */
        public double X
        {
            get { return x; }
        }

        /**
         * Returns the y-coordinate.
         * @return the y-coordinate
         */
        public double Y
        {
            get { return y; }
        }

        /**
         * Returns the polar radius of this point.
         * @return the polar radius of this point in polar coordiantes: sqrt(x*x + y*y)
         */
        public double r()
        {
            return Math.Sqrt(x * x + y * y);
        }

        /**
         * Returns the angle of this point in polar coordinates.
         * @return the angle (in radians) of this point in polar coordiantes (between â€“&pi;/2 and &pi;/2)
         */
        public double theta()
        {
            return Math.Atan2(y, x);
        }

        /**
         * Returns the angle between this point and that point.
         * @return the angle in radians (between â€“&pi; and &pi;) between this point and that point (0 if equal)
         */
        private double angleTo(Point2D that)
        {
            double dx = that.x - this.x;
            double dy = that.y - this.y;
            return Math.Atan2(dy, dx);
        }

        /**
         * Returns true if aâ†’bâ†’c is a counterclockwise turn.
         * @param a first point
         * @param b second point
         * @param c third point
         * @return { -1, 0, +1 } if aâ†’bâ†’c is a { clockwise, collinear; counterclocwise } turn.
         */
        public static int ccw(Point2D a, Point2D b, Point2D c)
        {
            double area2 = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
            if (area2 < 0) return -1;
            else if (area2 > 0) return +1;
            else return 0;
        }

        /**
         * Returns twice the signed area of the triangle a-b-c.
         * @param a first point
         * @param b second point
         * @param c third point
         * @return twice the signed area of the triangle a-b-c
         */
        public static double area2(Point2D a, Point2D b, Point2D c)
        {
            return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        }

        /**
         * Returns the Euclidean distance between this point and that point.
         * @param that the other point
         * @return the Euclidean distance between this point and that point
         */
        public double distanceTo(Point2D that)
        {
            double dx = this.x - that.x;
            double dy = this.y - that.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /**
         * Returns the square of the Euclidean distance between this point and that point.
         * @param that the other point
         * @return the square of the Euclidean distance between this point and that point
         */
        public double distanceSquaredTo(Point2D that)
        {
            double dx = this.x - that.x;
            double dy = this.y - that.y;
            return dx * dx + dy * dy;
        }

        /**
         * Compares two points by y-coordinate, breaking ties by x-coordinate.
         * Formally, the invoking point (x0, y0) is less than the argument point (x1, y1)
         * if and only if either {@code y0 < y1} or if {@code y0 == y1} and {@code x0 < x1}.
         *
         * @param  that the other point
         * @return the value {@code 0} if this string is equal to the argument
         *         string (precisely when {@code equals()} returns {@code true});
         *         a negative integer if this point is less than the argument
         *         point; and a positive integer if this point is greater than the
         *         argument point
         */
        public int compareTo(Point2D that)
        {
            if (this.y < that.y) return -1;
            if (this.y > that.y) return +1;
            if (this.x < that.x) return -1;
            if (this.x > that.x) return +1;
            return 0;
        }

        public override bool Equals(Object other)
        {
            if (other == this) return true;
            if (other == null) return false;
            if (other.GetType() != GetType())
            {
                return false;
            }
            Point2D that = (Point2D)other;
            return this.x == that.x && this.y == that.y;
        }

        /**
         * Return a string representation of this point.
         * @return a string representation of this point in the format (x, y)
         */

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        /**
         * Returns an integer hash code for this point.
         * @return an integer hash code for this point
         */

        public override int GetHashCode()
        {
            
            int hashX = ((Double)x).GetHashCode();
            int hashY = ((Double)y).GetHashCode();
            return 31 * hashX + hashY;
        }
    }






}
