using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class Estado    {
        private int idEstado;
        private List<Transicion> transiciones = new List<Transicion>();

        public Estado(int idEstado, List<Transicion> transiciones)
        {
            this.IdEstado = idEstado;
            this.Transiciones = transiciones;
        }

        public Estado(int idEstado)
        {
            this.IdEstado = idEstado;
        }

        public Estado()
        {
        }

        public int IdEstado { get => idEstado; set => idEstado = value; }
        public List<Transicion> Transiciones { get => transiciones; set => transiciones = value; }

        public void agregarTransicion(Transicion t)
        {
            Transiciones.Add(t);
        }

        public override string ToString()
        {
            return IdEstado.ToString();
        }
    }
}
