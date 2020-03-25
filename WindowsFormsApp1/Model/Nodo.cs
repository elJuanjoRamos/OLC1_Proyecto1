using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class Nodo : IComparable<Nodo>
    {
        private Nodo izquierda, derecha;
        private bool isLeaf;
        private int id;
        private ArrayList regex;
        private int numeroNodo;

        public Nodo()
        {
        }

        public Nodo(ArrayList regex)
        {
            this.Izquierda = new Nodo();
            this.derecha = new Nodo();
            this.Regex = regex;
        }

        public bool IsLeaf { get => isLeaf; set => isLeaf = value; }
        public int Id { get => id; set => id = value; }
        public ArrayList Regex { get => regex; set => regex = value; }
        public int NumeroNodo { get => numeroNodo; set => numeroNodo = value; }
        internal Nodo Izquierda { get => Izquierda1; set => Izquierda1 = value; }
        internal Nodo Izquierda1 { get => izquierda; set => izquierda = value; }

        public int CompareTo(Nodo other)
        {
            return NumeroNodo.CompareTo(other.NumeroNodo);
        }
    }
}
