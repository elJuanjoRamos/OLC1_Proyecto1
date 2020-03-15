using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class Transicion
    {
        private Estado inicio;
        private Estado fin;
        private String simbolo;

        public Transicion(Estado inicio, Estado fin, String simbolo)
        {
            this.Inicio = inicio;
            this.Fin = fin;
            this.Simbolo = simbolo;
        }

        public Transicion()
        {
        }

        public String Simbolo { get => simbolo; set => simbolo = value; }
        internal Estado Inicio { get => inicio; set => inicio = value; }
        internal Estado Fin { get => fin; set => fin = value; }
        
        public override string ToString()
        {
            return "(" + Inicio.IdEstado + "-" + Simbolo + "-" + Fin.IdEstado + ")";
        }

        public String DOT_String()
        {
            return (Inicio + " -> " + Fin + " [label=\"" + Simbolo.Replace("\"", "") + "\"];");
        }
    }
}
