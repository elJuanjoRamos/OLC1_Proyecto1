using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Automata
{
    class Automata
    {
        private readonly static Automata instance = new Automata();
        //PROPIEDADES 
        private Estado inicio;
        private List<Estado> aceptacion = new List<Estado>();
        private List<Estado> estados = new List<Estado>();
        private HashSet<String> alfabeto = new HashSet<String>();
        private String tipo;
        private String[] resultadoRegex;
        private String lenguajeR;
        
        public Automata()
        {
        }

        public List<Estado> Aceptacion { get => aceptacion; set => aceptacion = value; }
        public List<Estado> Estados { get => estados; set => estados = value; }
        public HashSet<string> Alfabeto { get => alfabeto; set => alfabeto = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string[] ResultadoRegex { get => resultadoRegex; set => resultadoRegex = value; }
        public string LenguajeR { get => lenguajeR; set => lenguajeR = value; }
        internal Estado Inicio { get => inicio; set => inicio = value; }

        //AGREGAR ESTADO FINAL
        public void AgregarEstadoAceptacion(Estado estado)
        {
            Aceptacion.Add(estado);
        }

        //AGREGAR ESTADO FINAL
        public void AgregarEstado(Estado estado)
        {
            Estados.Add(estado);
        }

        //DEFINIR ALFABETO
        public void CrearAlfabeto(ArrayList elementos)
        {
            foreach (String x in elementos)
            {
                if (x != "|" && x != "." && x != "*" )
                {
                    Alfabeto.Add(x);
                }
            }
        }

        public void addResultadoRegex(int key, String value)
        {
            ResultadoRegex[key] = value;
        }

        public override string ToString()
        {
            String res = "";
            res += "-------" + Tipo + "---------\r\n";
            res += "Alfabeto "; 
            foreach(String a in Alfabeto)
            {
                res += a + ", ";
            }
            res += "\r\n";
            res += "Estado inicial " + Inicio + "\r\n";
            res += "Conjutos de estados de aceptacion ";
            foreach (Estado a in Aceptacion)
            {
                res += a.IdEstado + ", ";
            }
            res += "\r\n";
            res += "Conjunto de Estados ";
            foreach (Estado a in Estados)
            {
                res += a.IdEstado + ", ";
            }
            res += "\r\n";
            res += "Conjunto de transiciones ";
            foreach (Estado a in Estados)
            {
                foreach (Transicion b in a.Transiciones)
                {
                    res += "(" + b.Inicio.IdEstado + "-" + b.Simbolo + "-" + b.Fin.IdEstado + ")";
                }
            }
            res += "\r\n";
            res += "Lenguaje r: " + LenguajeR + "\r\n";

            return res;
        }

    }
}
