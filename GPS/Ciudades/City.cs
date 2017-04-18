using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Ciudades
{
    public class City
    {
        private string name;
        private float x;
        private float y;

        public City(string name, float x, float y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public string Name
        {
            get { return name; }
        }

        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }
    }
}
