using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbol
{
    public class BST<T> where T : IComparable<T>
    {
        private Node raiz;
        public BST()
        {
            raiz = null;
        }

        public void Insert(T dato)
        {
            raiz = Insert(raiz, dato);
        }

        private Node Insert(Node n, T dato)
        {
            if (n == null)
            {
                return new Node(dato);
            }
            int comp = n.Dato.CompareTo(dato);
            if (comp < 0)
            {
                n.Right = Insert(n.Right, dato);
            }
            else if (comp > 0)
            {
                n.Left = Insert(n.Left, dato);
            }
            return n;
        }
        public void Draw(Graphics g, float x, float y)
        {
            Draw(g, raiz, x, y);
        }
        public void Draw(Graphics g, Node n, float x, float y)
        {
            if (n == null)
            {
                return;
            }

            g.DrawString(n.Dato.ToString(), new Font("Arial", 12), Brushes.Black, x, y);
            g.DrawEllipse(Pens.Black, x - 2, y, 20, 20);
            if (n.Left != null)
            {

                g.DrawLine(Pens.Black, x + 5, y + 20, x - 50 + 5, y + 50);
                Draw(g, n.Left, x - 50, y + 50);
            }
            if (n.Right != null)
            {

                g.DrawLine(Pens.Black, x + 5, y + 20, x + 50 + 5, y + 50);
                Draw(g, n.Right, x + 50, y + 50);
            }

        }

        public void Remove(T dato)
        {
            raiz = Remove(raiz, dato);
        }

        

        public Node Remove(Node n, T dato)
        {
            if (n == null)
            {
                return null;
            }
            int cmp = n.Dato.CompareTo(dato);
            if (cmp > 0)
            {
                n.Left = Remove(n.Left, dato);
            }
            else if (cmp < 0)
            {
                n.Right = Remove(n.Right, dato);
            }
            else
            {
                if (n.Left == null)
                {
                    return n.Right;
                }
                if (n.Right == null)
                {
                    return n.Left;
                }
                Node t = n;
                n = Min(t.Right);
                n.Right = DelMin(t.Right);
                n.Left = t.Left;

            }
            return n;
        }
        private Node Min(Node n)
        {
            if (n.Left == null)
            {
                return n;
            }
            return Min(n.Left);
        }
        private Node DelMin(Node n)
        {
            if (n.Left == null) return n.Right;
            n.Left = DelMin(n.Left);
            return n;
        }

        public void Recorre()
        {
            Recorre(raiz);
        }

        public void Recorre(Node n)
        {
            if (n == null)
            {
                return;
            }
            Recorre(n.Left);
            Console.WriteLine(n.Dato);
            Recorre(n.Right);
        }


        public class Node
        {
            private Node izq;
            private Node der;

            private T dato;

            public Node(T dato)
            {
                this.dato = dato;
            }

            public T Dato
            {
                get { return dato; }
            }

            public Node Left
            {
                get { return izq; }
                set { izq = value; }
            }

            public Node Right
            {
                get { return der; }
                set { der = value; }
            }


        }
    }
}
