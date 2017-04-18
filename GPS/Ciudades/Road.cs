using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Ciudades
{
    public class Road
    {
        private Image i;
        private float x;
        private float y;
        private float x2;
        private float y2;
        private int id;

        public Road(int id, float x, float y, float x2, float y2)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.x2 = x2;
            this.y2 = y2;
        }

        public int ID
        {
            get { return id; }
        }

        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }

        public float X2
        {
            get { return x2; }
        }

        public float Y2
        {
            get { return y2; }
        }
    }
}
