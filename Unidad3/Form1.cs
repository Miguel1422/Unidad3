using Unidad3.Arbol;
using Unidad3.GPS;
using Unidad3.Viajero;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unidad3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void arbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArbolC a = new ArbolC();

            a.MdiParent = this;
            a.Show();
        }

        private void gPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GPSForm a = new GPSForm();

            a.MdiParent = this;
            a.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void viajeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salesman a = new Salesman();

            a.MdiParent = this;
            a.Show();
        }
    }
}
