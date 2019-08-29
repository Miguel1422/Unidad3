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

        public double Distance(Node other)
        {
            double ans = 0;
            double lat1 = ToRadians(this.Latitude);
            double lon1 = ToRadians(this.Longitude);
            double lat2 = ToRadians(other.Latitude);
            double lon2 = ToRadians(other.Longitude);

            double difLat = (ToRadians(this.Latitude - other.Latitude));
            double diflon = (ToRadians(this.Longitude - other.Longitude));
            double aa = Math.Pow(Math.Sin(difLat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(diflon/2) , 2);
            double c = 2 * Math.Atan2(Math.Sqrt(aa), Math.Sqrt(1 - aa));
            ans = 6371 * c * 1000;
            return ans;
        }
        private double ToRadians(double n)
        {
            return (n * Math.PI / 180);
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

