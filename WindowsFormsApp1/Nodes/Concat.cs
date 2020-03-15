using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Nodes
{
    class Concat
    {
        private String data;
        private Concat next;

        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public Concat Next
        {
            get { return next; }
            set { next = value; }
        }

        public Concat(String d)
        {
            this.Data = d;
            this.Next = null;
        }

        public Concat() { }
    }
}
