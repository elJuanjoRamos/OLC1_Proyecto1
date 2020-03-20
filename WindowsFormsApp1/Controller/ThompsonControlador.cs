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
            resultado.Add(eClosureEstado); //la operacion e-Closure debe tener el estado aplicado

            while (pilaClosure.Count > 0)
            {
                actual = pilaClosure.Pop();
                
                foreach (Transicion t in (ArrayList)actual.Transiciones)
                {
                    if (t.Simbolo.Equals("ε") && !resultado.Contains(t.Fin))
                    {
                        resultado.Add(t.Fin);
                        pilaClosure.Push(t.Fin);
                    }
                }
            }
            return resultado;
        }


        public HashSet<Estado> move(HashSet<Estado> estados, String simbolo)
        {
            
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

        public HashSet<Estado> moveInSet(HashSet<Estado> estados, String simbolo)
        {

            HashSet<Estado> alcanzados = new HashSet<Estado>();

            //Console.WriteLine("ESTADOS");
            foreach (Estado iterador in estados)
            {

                foreach (Transicion t in (ArrayList)iterador.Transiciones)
                {
                    //Console.WriteLine(t.Simbolo);

                    Estado siguiente = t.Fin;
                    String simb = (String)t.Simbolo;

                    //Trata de convertir el simbolo del estado en char
                    Char value;
                    bool result;
                    result = Char.TryParse(simb, out value);

                    //Si se puede, agrega el estado siguiente al arreglo de alcanzados
                    if (result)
                    {
                        if (simb.Equals(simbolo))
                        {
                            alcanzados.Add(siguiente);
                        }
                    }
                    //Si no puede, es por que vienen un conjunto
                    else
                    {
                        //Va a buscar a la lista de conjuntos el nombre y retorna sus elementos
                        ArrayList listChar = SetController.Instance.GetElemntsOfSet(simb);
                        //Verifica que la lista no venga vacia
                        if (listChar != null)
                        {
                            foreach (String letter in listChar)
                            {
                                //Evalua si en el conjunto se encuentra el elemento a evaluar
                                //Ejemplo> si se evalua 'd' y el conjunto es 'letras' entonces el if es verdadero
                                if (letter.Equals(simbolo))
                                {
                                    alcanzados.Add(siguiente);
                                }
                            }
                        }
                        else if (listChar == null && simb.Equals(simbolo))
                        {

                            alcanzados.Add(siguiente);
                        }
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

        public Boolean EvaluateExpression(String regex, Automata.Automata automata)
        {
            Console.WriteLine(automata);
            Estado inicial = automata.Inicio;
            List<Estado> estados = automata.Estados;
            List<Estado> aceptacion = new List<Estado>(automata.Aceptacion);

            HashSet<Estado> conjunto = eClosure(inicial);
            foreach (Char ch in regex)
            {
                //Move in sets es un metodo para moverme entre conjuntos
                conjunto = moveInSet(conjunto, ch.ToString());
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


            bool response = false;

            foreach (Estado aceptation_State in aceptacion)
            {
                if (conjunto.Contains(aceptation_State))
                {
                    response = true;
                }
            }
            if (response)
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

        public void generarDOT(String nombreArchivo, String pngname, Automata.Automata automataFinito)
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
            var command = "dot -Tpng \"" + path + "\\" + nombreArchivo + ".dot\"  -o \"" + path+ "\\" + nombreArchivo  + pngname + ".png\"   ";
            //Console.WriteLine(command);
            var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStarInfo;
            proc.Start();
            proc.WaitForExit();

        }
    
    
    
    
        public void TableConstructor(String fileName,String path, Automata.Automata afd)
        {

            try
            {
                System.IO.File.WriteAllText(path + "\\" + "TableTransition.dot", GetCodeGraphviz(afd, fileName));
                var command = "dot -Tpng \"" + path + "\\" + "TableTransition.dot\"  -o \"" + path + "\\" + fileName + "TableTransition.png\"   ";
                var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();

            }
            catch (Exception)
            {
                MessageBox.Show("Error al escribir el archivo aux_grafico.dot", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public String GetCodeGraphviz(Automata.Automata afd, String nombre) {
            String[,] tabla = new String[20, 20];
            ArrayList states = new ArrayList();


            Automata.Automata temp = afd;

            String texto = "";

            for (int i = 0; i < temp.Estados.Count; i++)
            {
                
                ArrayList ar = (ArrayList)temp.Estados[i].Transiciones;
                for (int j = 0; j < ar.Count; j++)
                {
                    RowTable rowTable = new RowTable();

                    Transicion t = (Transicion)ar[j];
                    rowTable.Start = t.Inicio.IdEstado;
                    rowTable.End = t.Fin.IdEstado;
                    rowTable.Symbol = t.Simbolo.Replace('"', ' ');
                    states.Add(rowTable);
                    texto = texto + "{" + t.Inicio.IdEstado + "|" + t.Fin.IdEstado + "|" + t.Simbolo.Replace('"', ' ') + "}|\n";

                }
            }

            texto = texto + "{<here> " + "---" + "|---}|";

            return "digraph grafica{\n" +
              "rankdir=TB;\n" +
              "node [shape = record, style=filled, fillcolor=white];\n"
               +
               "nodo1"  + "[label=\""
               + "\n{ Transition Table " + nombre + "|{E Inicio | E Final | Simb}|" + texto + 

                "}\"];\n}";
        }


    }
}
