using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3.GPS.Util
{
    public class Way
    {
        private List<long> nodes;
        private string name;
        private long id;

        public Way(string name, long id)
        {
            this.name = name;
            this.id = id;
            nodes = new List<long>();
        }

        public void Add(long n)
        {
            nodes.Add(n);
        }

        public string Name
        {
            get { return name; }
        }





        public long ID
        {
            get { return id; }
        }

        public List<long> Nodes
        {
            get { return nodes; }
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Way o = (Way)obj;
            return id == o.ID;
        }
    }
}
