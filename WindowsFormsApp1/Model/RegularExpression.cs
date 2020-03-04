using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class RegularExpression
    {
        private String name;
        private ArrayList elements;

        public RegularExpression()
        {
            Elements = new ArrayList();
        }

        public RegularExpression(String n, ArrayList ar)
        {
            this.Name = n;
            this.Elements = ar;
        }

        public void toString()
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Elements:");
            foreach (object item in elements)
            {
                Console.WriteLine("\t-" + item);
            }
        }

        public string Name { get => name; set => name = value; }
        public ArrayList Elements { get => elements; set => elements = value; }
    }
}
