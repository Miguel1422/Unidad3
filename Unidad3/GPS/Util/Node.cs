using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3.GPS.Util
{
    public class Node
    {
        private long id;
        private double lat;
        private double lon;
        public Node(long id, double lat, double lon)
        {
            this.id = id;
            this.lat = lat;
            this.lon = lon;
        }

        public long ID
        {
            get { return id; }
        }
        public double Latitude
        {
            get { return lat; }
        }

        public double Longitude
        {
            get { return lon; }
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Node o = (Node)obj;
            return id == o.ID;
        }

        public override string ToString()
        {
            return id+"";
        }
    }
}

