using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsFormsApp1.Nodes;

namespace WindowsFormsApp1.Lists
{
    class ConcatList
    {
        private Concat First = new Concat();
        private Concat Last = new Concat();

        public ConcatList()
        {
            First = Last = null;
        }


        public void Insert(String data)
        {
            Concat c = new Concat();
            c.Data = data;
            c.Next = null;

            if (First == null)
            {
                First = c;
                Last = c;
            } else
            {
                First.Next = c;
                c.Next = null;
                Last = c;
            }

        }

        public void Show()
        {
            Concat temp = First;

            if (First != null)
            {
                while (temp != null)
                {
                    Console.WriteLine("->"+temp.Data.ToString());
                    temp = temp.Next;
                }
            } else
            {
                Console.WriteLine("Empty");
            }
        }

        //return the list
        public Concat getFirst()
        {
            return First;
        }

        //Clear the list
        public void Clear()
        {
            First = Last = null;
        }

    }
}
