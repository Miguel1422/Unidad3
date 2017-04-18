using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.GPS.Util
{

    public class City
    {
        private Node position;
        private string name;

        public City(string name, Node position)
        {
            this.name = name;
            this.position = position;
        }

        public Node Position
        {
            get { return position; }

        }
        public string Name
        {
            get { return name; }
        }

        public double Latitude
        {
            get { return position.Latitude; }
        }

        public double Longitude
        {
            get { return position.Longitude; }
        }

        public override int GetHashCode()
        {
            return position.ID.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Node o = ((City)obj).Position;
            return position.ID == o.ID;
        }
    }
}
