using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Automata
{
    class AFN
    {
        private Automata afn;
        private String regex;

        internal Automata Afn { get => afn; set => afn = value; }
        public string Regex { get => regex; set => regex = value; }

        public AFN(string regex)
        {
            this.Regex = regex;
        }

        public AFN()
        {
           
        }

        public void construirAutomata(ArrayList arrayList)
        {
            Stack pilaAFN = new Stack();

            foreach (String c in arrayList)
            {
                //Console.WriteLine(c);
                switch (c)
                {
                    case "*":
                        Automata kleene = cerraduraKleene((Automata)pilaAFN.Pop());
                        pilaAFN.Push(kleene);
                        this.Afn = kleene;
                        break;
                    case ".":
                        Automata concat_param1 = (Automata)pilaAFN.Pop();
                        Automata concat_param2 = (Automata)pilaAFN.Pop();

                        Automata concat_result = concatenacion(concat_param2, concat_param1);

                        pilaAFN.Push(concat_result);
                        this.Afn = concat_result;
                        break;
                    case "|":

                        Automata union_param1 = (Automata)pilaAFN.Pop();
                        Automata union_param2 = (Automata)pilaAFN.Pop();
                        Automata union_result = union(union_param1, union_param2);
                        pilaAFN.Push(union_result);

                        this.Afn = union_result;
                        break;
                    case "?":

                        Automata s = afnSimple("ε");
                        Automata union_q1 = (Automata)pilaAFN.Pop();
                        Automata qtion = union(union_q1, s);

                        pilaAFN.Push(qtion);

                        this.Afn = qtion;
                        break;
                    default:
                        //crear un automata con cada simbolo
                        Automata simple = afnSimple(c);
                        pilaAFN.Push(simple);
                        this.Afn = simple;
                        break;
                }
            }
            this.Afn.CrearAlfabeto(arrayList);
            this.Afn.Tipo = "AFN";
        }

        public Automata afnSimple(String simboloRegex)
        {
            Automata automataFN = new Automata();
            //definir los nuevos estados
            Estado inicial = new Estado(0);
            Estado aceptacion = new Estado(1);
            //crear una transicion unica con el simbolo
            Transicion tran = new Transicion(inicial, aceptacion, simboloRegex);
            inicial.agregarTransicion(tran);
            //agrega los estados creados
            automataFN.AgregarEstado(inicial);
            automataFN.AgregarEstado(aceptacion);
            //colocar los estados iniciales y de acpetacion
            automataFN.Inicio = (inicial);
            automataFN.AgregarEstadoAceptacion(aceptacion);
            automataFN.LenguajeR = (simboloRegex + "");
            return automataFN;
        }

        public Automata cerraduraKleene(Automata automataFN)
        {
            Automata afn_kleene = new Automata();

            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            afn_kleene.AgregarEstado(nuevoInicio);
            afn_kleene.Inicio = (nuevoInicio);

            //agregar todos los estados intermedio
            for (int i = 0; i < automataFN.Estados.Count; i++)
            {
                Estado tmp = (Estado)automataFN.Estados[i];
                tmp.IdEstado = (i + 1);
                afn_kleene.AgregarEstado(tmp);
            }

            //Se crea un nuevo estado de aceptacion
            Estado nuevoFin = new Estado(automataFN.Estados.Count + 1);
            afn_kleene.AgregarEstado(nuevoFin);
            afn_kleene.AgregarEstadoAceptacion(nuevoFin);

            //definir estados clave para realizar la cerraduras
            Estado anteriorInicio = automataFN.Inicio;

            List<Estado> anteriorFin = automataFN.Aceptacion;

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, anteriorInicio, "ε"));
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, nuevoFin, "ε"));

            // agregar transiciones desde el anterior estado final
            for (int i = 0; i < anteriorFin.Count; i++)
            {
                anteriorFin[i].Transiciones.Add(new Transicion((Estado)anteriorFin[i], anteriorInicio, "ε"));
                anteriorFin[i].Transiciones.Add(new Transicion((Estado)anteriorFin[i], nuevoFin, "ε"));
            }
            afn_kleene.Alfabeto = (automataFN.Alfabeto);
            afn_kleene.LenguajeR = (automataFN.LenguajeR);
            return afn_kleene;
        }




        public Automata concatenacion(Automata AFN1, Automata AFN2)
        {

            
            Automata afn_concat = new Automata();

            //se utiliza como contador para los estados del nuevo automata
            int i = 0;
            for (i = 0; i < AFN1.Estados.Count; i++)
            {
                Estado tmp = AFN1.Estados[i];
                tmp.IdEstado = i;
                //se define el estado inicial
                if (i == 0)
                {
                    afn_concat.Inicio = (tmp);
                }
                //cuando llega al último, concatena el ultimo con el primero del otro automata con un epsilon
                if (i == AFN1.Estados.Count - 1)
                {
                    //se utiliza un ciclo porque los estados de aceptacion son un array
                    for (int k = 0; k < AFN1.Aceptacion.Count; k++)
                    {
                        tmp.agregarTransicion(new Transicion(AFN1.Aceptacion[k], AFN2.Inicio, "ε"));
                    }
                }
                afn_concat.AgregarEstado(tmp);

            }


            //termina de agregar los estados y transiciones del segundo automata
            for (int j = 0; j < AFN2.Estados.Count; j++)
            {
                Estado tmp = AFN2.Estados[j];
                tmp.IdEstado = i;

                //define el ultimo con estado de aceptacion
                if (AFN2.Estados.Count - 1 == j)
                {
                    afn_concat.AgregarEstadoAceptacion(tmp);
                }
                afn_concat.AgregarEstado(tmp);
                i++;
            }

            HashSet<String> alfabeto = new HashSet<String>();
            alfabeto.UnionWith(AFN1.Alfabeto);
            alfabeto.UnionWith(AFN2.Alfabeto);
            afn_concat.Alfabeto = (alfabeto);
            afn_concat.LenguajeR = (AFN1.LenguajeR + " " + AFN2.LenguajeR);


            /*//se utiliza como contador para los estados del nuevo automata
            int i = 0;
            //agregar los estados del primer automata
            for (i = 0; i < AFN2.Estados.Count; i++)
            {
                Estado tmp = AFN2.Estados[i];
                tmp.IdEstado = i;
                //se define el estado inicial
                if (i == 0)
                {
                    afn_concat.Inicio = (tmp);
                }
                //cuando llega al último, concatena el ultimo con el primero del otro automata con un epsilon
                if (i == AFN2.Estados.Count - 1)
                {
                    //se utiliza un ciclo porque los estados de aceptacion son un array
                    for (int k = 0; k < AFN2.Aceptacion.Count; k++)
                    {
                        tmp.agregarTransicion(new Transicion(AFN2.Aceptacion[k], AFN1.Inicio, "ε"));
                    }
                }
                afn_concat.AgregarEstado(tmp);

            }
            //termina de agregar los estados y transiciones del segundo automata
            for (int j = 0; j < AFN1.Estados.Count; j++)
            {
                Estado tmp = AFN1.Estados[j];
                tmp.IdEstado = i;  

                //define el ultimo con estado de aceptacion
                if (AFN1.Estados.Count - 1 == j)
                    afn_concat.AgregarEstadoAceptacion(tmp);
                afn_concat.AgregarEstado(tmp);
                i++;
            }

            HashSet<String> alfabeto = new HashSet<String>();
            alfabeto.UnionWith(AFN1.Alfabeto);
            alfabeto.UnionWith(AFN2.Alfabeto);
            afn_concat.Alfabeto = (alfabeto);
            afn_concat.LenguajeR = (AFN1.LenguajeR + " " + AFN2.LenguajeR);
            */
            return afn_concat;
        }

        public Automata union(Automata AFN1, Automata AFN2)
        {
            Automata afn_union = new Automata();
            //se crea un nuevo estado inicial
            Estado nuevoInicio = new Estado(0);
            //se crea una transicion del nuevo estado inicial al primer automata
            nuevoInicio.agregarTransicion(new Transicion(nuevoInicio, AFN2.Inicio, "ε"));

            afn_union.AgregarEstado(nuevoInicio);
            afn_union.Inicio = (nuevoInicio);
            int i = 0;//llevar el contador del identificador de estados
                      //agregar los estados del segundo automata
            for (i = 0; i < AFN1.Estados.Count; i++)
            {
                Estado tmp = AFN1.Estados[i];
                tmp.IdEstado = i + 1;
                afn_union.AgregarEstado(tmp);
            }
            //agregar los estados del primer automata
            for (int j = 0; j < AFN2.Estados.Count; j++)
            {
                Estado tmp = AFN2.Estados[j];
                tmp.IdEstado = i + 1;
                afn_union.AgregarEstado(tmp);
                i++;
            }

            //se crea un nuevo estado final
            Estado nuevoFin = new Estado(AFN1.Estados.Count + AFN2.Estados.Count + 1);
            afn_union.AgregarEstado(nuevoFin);
            afn_union.AgregarEstadoAceptacion(nuevoFin);


            Estado anteriorInicio = AFN1.Inicio;
            List<Estado> anteriorFin = AFN1.Aceptacion;
            List<Estado> anteriorFin2 = AFN2.Aceptacion;

            // agregar transiciones desde el nuevo estado inicial
            nuevoInicio.Transiciones.Add(new Transicion(nuevoInicio, anteriorInicio, "ε"));

            // agregar transiciones desde el anterior AFN 1 al estado final
            for (int k = 0; k < anteriorFin.Count; k++)
                anteriorFin[k].Transiciones.Add(new Transicion(anteriorFin[k], nuevoFin, "ε"));
            // agregar transiciones desde el anterior AFN 2 al estado final
            for (int k = 0; k < anteriorFin.Count; k++)
                anteriorFin2[k].Transiciones.Add(new Transicion(anteriorFin2[k], nuevoFin, "ε"));

            HashSet<String> alfabeto = new HashSet<String>();
            alfabeto.UnionWith(AFN1.Alfabeto);
            alfabeto.UnionWith(AFN2.Alfabeto);
            afn_union.Alfabeto = (alfabeto);
            afn_union.LenguajeR = (AFN1.LenguajeR + " " + AFN2.LenguajeR);
            return afn_union;
        }



    }
}
