using Unidad3.Arbol;
using Unidad3.GPS.Util;
using Unidad3.Viajero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unidad3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GPS.GPSForm());
            //Application.Run(new Viajero.Salesman());
            Application.Run(new Form1());
            //Application.Run(new ArbolC());
        }
    }
}
