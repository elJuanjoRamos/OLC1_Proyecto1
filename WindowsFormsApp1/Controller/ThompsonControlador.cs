using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    class ThompsonControlador
    {
        private readonly static ThompsonControlador instance = new ThompsonControlador();
        private String resultado;

        public ThompsonControlador()
        {
        }

        public static ThompsonControlador Instance
        {
            get
            {
                return instance;
            }
        }

        public HashSet<Estado> eClosure(Estado eClosureEstado)
        {
            Stack<Estado> pilaClosure = new Stack<Estado>();
            Estado actual = eClosureEstado;
            HashSet<Estado> resultado = new HashSet<Estado>();

            
            pilaClosure.Push(actual);
            
            while (pilaClosure.Count > 0)
            {
                actual = pilaClosure.Pop();

                foreach (Transicion t in (ArrayList)actual.Transiciones)
                {
                    //Console.WriteLine(t);
                    if (t.Simbolo.Equals("ε") && !resultado.Contains(t.Fin))
                    {
                        //Console.WriteLine("VALOR : " + t);
                        resultado.Add(t.Fin);
                        pilaClosure.Push(t.Fin);
                    }
                }
            }
            resultado.Add(eClosureEstado); //la operacion e-Closure debe tener el estado aplicado
            return resultado;
        }


        public HashSet<Estado> eClosure2(Estado eClosureEstado)
        {
            Stack<Estado> pilaClosure = new Stack<Estado>();
            Estado actual = eClosureEstado;
            HashSet<Estado> resultado = new HashSet<Estado>();


            pilaClosure.Push(actual);

            while (pilaClosure.Count > 0)
            {
                actual = pilaClosure.Pop();

                foreach (Transicion t in (ArrayList)actual.Transiciones)
                {
                    //Console.WriteLine(t);
                    if (t.Simbolo.Equals("ε") && !resultado.Contains(t.Fin))
                    {
                        //Console.WriteLine("VALOR : " + t);
                        resultado.Add(t.Fin);
                        pilaClosure.Push(t.Fin);
                    }
                }
            }
            resultado.Add(eClosureEstado); //la operacion e-Closure debe tener el estado aplicado
            return resultado;
        }

        public HashSet<Estado> move(HashSet<Estado> estados, String simbolo)
        {
            Console.WriteLine("el simbolo es " + simbolo);

            HashSet<Estado> alcanzados = new HashSet<Estado>();
           
            //Console.WriteLine("ESTADOS");
            foreach (Estado iterador in estados)
            {
                
                foreach (Transicion t in (ArrayList)iterador.Transiciones)
                {
                    //Console.WriteLine(t.Simbolo);

                    Estado siguiente = t.Fin;
                    String simb = (String)t.Simbolo;
                    if (simb.Equals(simbolo))
                    {
                        alcanzados.Add(siguiente);
                    }
                }
            }
            return alcanzados;
        }

        public Estado move(Estado estado, String simbolo)
        {
            List<Estado> alcanzados = new List<Estado>();

            foreach (Transicion t in (ArrayList)estado.Transiciones)
            {
                Estado siguiente = t.Fin;
                String simb = (String)t.Simbolo;

                if (simb.Equals(simbolo) && !alcanzados.Contains(siguiente))
                {
                    alcanzados.Add(siguiente);
                }

            }

            return alcanzados[0];
        }

        /*public HashSet<Estado> move(HashSet<Estado> estados, String simbolo)
        {
            HashSet<Estado> alcanzados = new HashSet<Estado>();
            //IEnumerator<Estado> iterador = estados.GetEnumerator();

            foreach (Estado iterador in estados)
            {
                foreach (Transicion t in (List<Transicion>)iterador.Transiciones)
                {
                    Estado siguiente = t.Fin;
                    String simb = (String)t.Simbolo;
                    if (simb.Equals(simbolo))
                    {
                        alcanzados.Add(siguiente);
                    }
                }
            }
            /*while (iterador.MoveNext())
            {

                foreach(Transicion t in (List<Transicion>)iterador.Current.Transiciones)
                {
                    Estado siguiente = t.Fin;
                    String simb = (String)t.Simbolo;
                    if (simb.Equals(simbolo))
                    {
                        alcanzados.Add(siguiente);
                    }
                }
            }*/
        /*    return alcanzados;
        }

       /* public Estado moves(Estado estado, String simbolo)
        {
            List<Estado> alcanzados = new List<Estado>();

            foreach (Transicion t in (List<Transicion>)estado.Transiciones)
            {
                Estado siguiente = t.Fin;
                String simb = (String)t.Simbolo;

                if (simb.Equals(simbolo) && !alcanzados.Contains(siguiente))
                {
                    alcanzados.Add(siguiente);
                }

            }

            return alcanzados[0];
        }*/

        public Boolean simular(ArrayList regex, Automata.Automata automata)
        {
            Estado inicial = automata.Inicio;
            List<Estado> estados = automata.Estados;
            List<Estado> aceptacion = new List<Estado>(automata.Aceptacion);

            HashSet<Estado> conjunto = eClosure(inicial);
            foreach (String ch in regex)
            {
                conjunto = move(conjunto, ch);
                HashSet<Estado> temp = new HashSet<Estado>();
                IEnumerator<Estado> iter = conjunto.GetEnumerator();

                //Estado aux = iter.Current;
                while (iter.MoveNext())
                {
                    Estado siguiente = iter.Current;
                    //aux = iter.MoveNext();
                    /**
                     * En esta parte es muy importante el metodo addAll
                     * porque se tiene que agregar el eClosure de todo el conjunto
                     * resultante del move y se utiliza un hashSet temporal porque
                     * no se permite la mutacion mientras se itera
                     */
                    temp.UnionWith(eClosure(siguiente));

                }
                conjunto = temp;

            }


            bool res = false;

            foreach (Estado estado_aceptacion in aceptacion)
            {
                if (conjunto.Contains(estado_aceptacion))
                {
                    res = true;
                }
            }
            if (res)
            {
                //System.out.println("Aceptado");
                //this.resultado = "Aceptado";
                return true;
            }
            else
            {
                //System.out.println("NO Aceptado");
                // this.resultado = "No Aceptado";
                return false;
            }
        }

        public void generarDOT(String nombreArchivo, Automata.Automata automataFinito)
        {
            String texto = "digraph automata_finito {\n";

            texto += "\trankdir=LR;" + "\n";

            texto += "\tgraph [label=\"" + nombreArchivo + "\", labelloc=t, fontsize=20]; \n";
            texto += "\tnode [shape=doublecircle, style = filled,color = mediumseagreen];";
            //listar estados de aceptación
            for (int i = 0; i < automataFinito.Aceptacion.Count; i++)
            {
                texto += " " + automataFinito.Aceptacion[i];
            }
            //
            texto += ";" + "\n";
            texto += "\tnode [shape=circle];" + "\n";
            texto += "\tnode [color=midnightblue,fontcolor=white];\n" + "	edge [color=red];" + "\n";

            texto += "\tsecret_node [style=invis];\n" + "	secret_node -> " + automataFinito.Inicio + " [label=\"inicio\"];" + "\n";
            //transiciones
            for (int i = 0; i < automataFinito.Estados.Count; i++)
            {

                ArrayList t = (automataFinito.Estados[i]).Transiciones;
                for (int j = 0; j < t.Count; j++)
                {
                    texto += "\t" + ((Transicion)t[j]).DOT_String() + "\n";
                }

            }
            texto += "}";


            System.IO.File.WriteAllText(nombreArchivo + ".dot", texto);
            //Application.StartupPath
            String path = Application.StartupPath;
            var command = "dot -Tpng \"" + path + "\\" + nombreArchivo + ".dot\"  -o \"" + path + "\\" + nombreArchivo + ".png\"   ";
            //Console.WriteLine(command);

            var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStarInfo;
            proc.Start();
            proc.WaitForExit();

        }
    }
}
