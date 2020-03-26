using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        ArrayList arrayErrores = new ArrayList();
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
                    simb = simb.Trim('"');
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

      

        public Boolean EvaluateExpression(String regex, Automata.Automata automata, ArrayList ar, bool isString)
        {
            Console.WriteLine(automata);
            Estado inicial = automata.Inicio;
            List<Estado> estados = automata.Estados;
            List<Estado> aceptacion = new List<Estado>(automata.Aceptacion);

            HashSet<Estado> conjunto = eClosure(inicial);

            if (isString)
            {

                foreach (String ch in ar)
                {
                    //Move in sets es un metodo para moverme entre conjuntos

                    conjunto = moveInSet(conjunto, ch);
                    
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


            }
            else
            {
                for (int i = 0; i < regex.Length; i++)
                {
                    //Move in sets es un metodo para moverme entre conjuntos
                    Char ch = regex[i];
                    conjunto = moveInSet(conjunto, ch.ToString());
                    if (conjunto.Count == 0)
                    {
                        EvaluatorController.Instance.getError(ch.ToString(), i);
                    }
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
                return true;
            }
            else
            {
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

                        
            try
            {
                
                // Determine whether the directory exists.
                if (!Directory.Exists(path + "\\" + nombreArchivo))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path + "\\" + nombreArchivo);
                }

                String pngPath = path + "\\" + nombreArchivo;
                var command = "dot -Tpng \"" + path + "\\" + nombreArchivo + ".dot\"  -o \"" + pngPath + "\\" + nombreArchivo + " " + pngname + ".png\"   ";
                //Console.WriteLine(command);
                var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception e)
            {
            }

        }
    
    
    
    
        public void TableConstructor(String fileName,String path, Automata.Automata afd)
        {

            try
            {

                // Determine whether the directory exists.
                if (!Directory.Exists(path + "\\" + "Transitions Table"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path + "\\" + "Transitions Table");
                }

                String pngPath = path + "\\" + "Transitions Table";



                System.IO.File.WriteAllText(path + "\\" + "TableTransition.dot", GetCodeGraphviz(afd, fileName));
                var command = "dot -Tpng \"" + path + "\\" + "TableTransition.dot\"  -o \"" + pngPath + "\\" + fileName + "Table.png\"   ";
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
            Automata.Automata temp = afd;
            String texto = "";

            List<String> lista = new List<String>();
            List<int> estados = new List<int>();





            foreach (var item in temp.Estados)
            {

                foreach (Transicion o in item.Transiciones)
                {
                    //Token t = new Token(0, o.Simbolo, o.Simbolo, 0,0);
                    if (!lista.Contains(o.Simbolo))
                    {
                        lista.Add(o.Simbolo);

                    }

                    int start = o.Inicio.IdEstado;
                    int end = o.Fin.IdEstado;

                    if (!estados.Contains(start))
                    {
                        estados.Add(start);
                    }
                    if (!estados.Contains(end))
                    {
                        estados.Add(end);
                    }
                }
            }



            string[,] matriz = new string[estados.Count() + 1, lista.Count() + 1];
            for (int i = 0; i < lista.Count(); i++)
            {
                matriz[0, i + 1] = lista[i];
            }
            estados.Sort();
            for (int i = 0; i < estados.Count(); i++)
            {
                matriz[i + 1, 0] = estados[i].ToString();
            }
            int indexState = 0;
            int indexTerminal = 0;


            foreach (var item in afd.Estados)
            {
                foreach (Transicion transicion in item.Transiciones)
                {
                    int a = transicion.Inicio.IdEstado;
                    string b = transicion.Simbolo;
                    indexState = estados.FindIndex(e => e == a);
                    indexTerminal = lista.FindIndex(e => e == b);

                    String end = transicion.Fin.IdEstado.ToString();
                    /**/

                    foreach (var i in temp.Aceptacion)
                    {
                        
                        if (i.IdEstado == transicion.Fin.IdEstado)
                        {
                            end = end + "#";
                        }
                    }

                     matriz[indexState + 1, indexTerminal +1] = end;

                }
            }


            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                texto = texto + "\n<TR>\n";
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == null)
                    {
                        texto = texto + "\t<TD width=\"75\">-</TD>\n";  
                    } else
                    {
                        texto = texto + "\t<TD width=\"75\">" + matriz[i, j].Replace('"', ' ') + "</TD>\n";
                    }
                }
                texto = texto + "</TR>\n";
            }

        
        
            return "digraph G {\n" +
              "\n\tgraph [rankdir=LR, label=\"Transition Table " + nombre +"\", labelloc=t, fontsize=30, pad=0.5, nodesep=0.5, ranksep=2];\n"
               + "\n\tnode[shape=none];\n"+
               "\n\ttable[label =<\n  <TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\"> " + texto+ "\n</TABLE>>];\n}";
        }


    }
}
