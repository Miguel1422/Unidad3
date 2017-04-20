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
    public partial class PathFrm : Form
    {
        public PathFrm(List<string> text)
        {
            InitializeComponent();
            richTextBox1.Lines = text.ToArray();
        }
    }
}
