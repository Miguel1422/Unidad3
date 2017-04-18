﻿using GPS.Arbol;
using GPS.GPS.Util;
using GPS.Viajero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS
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
            Application.Run(new GPS.GPSForm());
            //Application.Run(new Form1());
            //Application.Run(new ArbolC());
        }
    }
}