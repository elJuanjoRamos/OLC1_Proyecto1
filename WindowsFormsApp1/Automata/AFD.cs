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
    class AFD
    {
        private Automata afd;
        private readonly ThompsonControlador simulador;
  
    
        public AFD()
        {
            this.simulador =  new ThompsonControlador();
            Afd = new Automata();
        }

        internal Automata Afd { get => afd; set => afd = value; }

        /**
     * Conversion de un automata AFN a uno AFD por el
     * metodo de subconjuntos
     * @param afn AFN
     */
        public void conversionAFN(Automata afn)
        {
            //se crea una estructura vacia
            Automata automata = new Automata();
            //se utiliza una cola como la estructura para guardar los subconjuntos a analizar
            Queue<HashSet<Estado>> cola = new Queue<HashSet<Estado>>();
            //se crea un nuevo estado inicial
            Estado inicial = new Estado(0);
            automata.Inicio = (inicial);
            automata.AgregarEstado(inicial);


            //el algoritmo empieza con el e-Closure del estado inicial del AFN
            HashSet<Estado> array_inicial = simulador.eClosure(afn.Inicio);
            
            //si el primer e-closure contiene estados de aceptacion hay que agregarlo
            foreach (Estado aceptacion in afn.Aceptacion)
            {
                if (array_inicial.Contains(aceptacion))
                {
                    automata.AgregarEstadoAceptacion(inicial);

                }
            }

            //lo agregamos a la pila
            cola.Enqueue(array_inicial);
            //variable temporal para guardar el resultado todos los subconjuntos creados
            ArrayList temporal = new ArrayList();
            //se utilizan esetos indices para saber el estado actuales y anterior
            int indexEstadoInicio = 0;

            while (cola.Count > 0)
            {
                //actual subconjunto
                HashSet<Estado> actual = cola.Dequeue();
                //se recorre el subconjunto con cada simbolo del alfabeto del AFN

                foreach (Object simbolo in afn.Alfabeto)
                {
                    //se realiza el move con el subconjunto
                    HashSet<Estado> move_result = simulador.move(actual, (String)simbolo);

                    HashSet<Estado> resultado = new HashSet<Estado>();
                    //e-Closure con cada estado del resultado del move y 
                    //se guarda en un solo array (merge)
                
                    foreach (Estado e in move_result)
                    {
                        resultado.UnionWith(simulador.eClosure(e));
                    }
                    Estado anterior = automata.Estados[indexEstadoInicio];
                    //Estado anterior = (Estado)automata.getEstados().get(indexEstadoInicio);
                    /*Si el subconjunto ya fue creado una vez, solo se agregan
                    transiciones al automata*/


                    int contador = 0;
                    int indexOf = 0;
                    if (temporal.Count > 0)
                    {
                        for (int i = 0; i < temporal.Count; i++)
                        {
                            string texto = "";
                            string texto2 = "";
                            HashSet<Estado> a = (HashSet<Estado>)temporal[i];
                            foreach (Estado item in a)
                            {
                                texto = texto + item.IdEstado;
                            }

                            foreach (Estado item in resultado)
                            {
                                texto2 = texto2 + item.IdEstado;
                            }

                            if (texto.Equals(texto2))
                            {
                                indexOf = i;
                                contador = 1;
                                break;
                            }
                        }
                    }

                    if (contador == 1)
                    {
                        List<Estado> array_viejo = automata.Estados;
                        Estado estado_viejo = anterior;
                        //se busca el estado correspondiente y se le suma el offset
                        Estado estado_siguiente = array_viejo[indexOf + 1];
                        estado_viejo.agregarTransicion(new Transicion(estado_viejo, estado_siguiente, simbolo.ToString()));

                    }
                    //si el subconjunto no existe, se crea un nuevo estado
                    else
                    {
                        temporal.Add(resultado);
                        cola.Enqueue(resultado);
                        Estado nuevo = new Estado(temporal.IndexOf(resultado) + 1);
                        anterior.agregarTransicion(new Transicion(anterior, nuevo, simbolo.ToString()));
                        automata.AgregarEstado(nuevo);
                        //se verifica si el estado tiene que ser de aceptacion
                        foreach (Estado aceptacion in afn.Aceptacion)
                        {
                            if (resultado.Contains(aceptacion))
                            {
                                automata.AgregarEstadoAceptacion(nuevo);
                            }

                        }
                    }


                }

                indexEstadoInicio++;
            }

            this.afd = automata;
            definirAlfabeto(afn);
            this.afd.Tipo = ("AFD");
        }




        private void definirAlfabeto(Automata afn)
        {
            this.afd.Alfabeto = afn.Alfabeto;
        }



        //QUITAR ESTADOS TRAMPA
        public Automata RemoveCheatStates(Automata afd)
        {
            ArrayList removeState = new ArrayList();
            /* 1. primero se calcula los estados que son de trampa
            * Se considera de trampa los estados que tienen transiciones
            * con todas las letras del alfabeto hacia si mismos
            */
            for (int i = 0; i < afd.Estados.Count; i++)
            {

                int verifyTransitionQuantity = afd.Estados[i].Transiciones.Count;


                int transitionCount = 0;
                foreach (Transicion t in (ArrayList)(afd.Estados[i]).Transiciones)
                {

                    if (afd.Estados[i] == t.Fin)
                    {
                        transitionCount++;
                    }

                }
                if (verifyTransitionQuantity == transitionCount && transitionCount != 0)
                {

                    removeState.Add(afd.Estados[i]);
                }
            }
            /*2. una vez ya sabido que estados son los de trampa
            * se quitan las transiciones que van hacia ese estado
            * y al final se elimina el estado del autómata
            */
            for (int i = 0; i < removeState.Count; i++)
            {
                for (int j = 0; j < afd.Estados.Count; j++)
                {
                    ArrayList arraytransition = afd.Estados[j].Transiciones;
                    int cont = 0;
                    // System.out.println(arrayT);
                    while (arraytransition.Count > cont)
                    {
                        Transicion t = (Transicion)arraytransition[cont];
                        //se verifican todas las transiciones que de todos los estados
                        //que van hacia el estado a eliminar
                        if (t.Fin == removeState[i])
                        {
                            afd.Estados[j].Transiciones.Remove(t);
                            cont--;
                        }
                        cont++;

                    }
                }
                //eliminar el estao al final
                afd.Estados.Remove((Estado)removeState[i]);
            }
            //3. arreglar la numeración cuando se quita un estado
            for (int i = 0; i < afd.Estados.Count; i++)
            {
                afd.Estados[i].IdEstado = i;
            }


            return afd;
        }


    }
}
