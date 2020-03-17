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
        /* public void conversionAFN(Automata afn)
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
                 Console.WriteLine("ACEPTACION");
                 Console.WriteLine(aceptacion);
                 if (array_inicial.Contains(aceptacion))
                     automata.AgregarEstadoAceptacion(inicial);
             }

             //lo agregamos a la pila
             cola.Enqueue(array_inicial);
             //variable temporal para guardar el resultado todos los subconjuntos creados
             List<HashSet<Estado>> temporal = new List<HashSet<Estado>>();
             //se utilizan esetos indices para saber el estado actuales y anterior
             int indexEstadoInicio = 0;
             while (cola.Count > 0)
             {
                 //actual subconjunto
                 HashSet<Estado> actual = cola.Dequeue();
                 //se recorre el subconjunto con cada simbolo del alfabeto del AFN
                 foreach (String simbolo in afn.Alfabeto)
                 {
                     //se realiza el move con el subconjunto
                     HashSet<Estado> move_result = simulador.move(actual, (String)simbolo);

                     HashSet<Estado> resultado = new HashSet<Estado>();
                     //e-Closure con cada estado del resultado del move y 
                     //se guarda en un solo array (merge)
                     foreach (Estado e in move_result)
                     {
                         Console.WriteLine("ESTADO");
                         Console.WriteLine(e);
                         resultado.UnionWith(simulador.eClosure(e));
                     }

                     Estado anterior = (Estado)automata.Estados[indexEstadoInicio];
                     /*Si el subconjunto ya fue creado una vez, solo se agregan
                     transiciones al automata*
                     if (temporal.Contains(resultado))
                     {
                         List<Estado> array_viejo = automata.Estados;
                         Estado estado_viejo = anterior;
                         //se busca el estado correspondiente y se le suma el offset
                         Estado estado_siguiente = array_viejo[temporal.IndexOf(resultado) + 1];
                         estado_viejo.agregarTransicion(new Transicion(estado_viejo, estado_siguiente, simbolo));

                     }
                     //si el subconjunto no existe, se crea un nuevo estado
                     else
                     {
                         temporal.Add(resultado);
                         cola.Enqueue(resultado);

                         Estado nuevo = new Estado(temporal.IndexOf(resultado) + 1);
                         anterior.agregarTransicion(new Transicion(anterior, nuevo, simbolo));
                         automata.AgregarEstado(nuevo);
                         //se verifica si el estado tiene que ser de aceptacion
                         foreach (Estado aceptacion in afn.Aceptacion)
                         {
                             Console.WriteLine("ACEPTACION");
                             Console.WriteLine(aceptacion);
                             if (resultado.Contains(aceptacion))
                                 automata.AgregarEstadoAceptacion(nuevo);
                         }
                     }


                 }
                 indexEstadoInicio++;

             }

             this.Afd = automata;
             //metodo para definir el alfabeto, se copia el del afn
             //definirAlfabeto(afn);
             this.Afd.Tipo = ("AFD");
             Console.WriteLine(this.Afd.Tipo);

         }

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
            Queue<Object> cola = new Queue<Object>();
            //se crea un nuevo estado inicial
            Estado inicial = new Estado(0);
            automata.Inicio = inicial;
            automata.AgregarEstado(inicial);


            //el algoritmo empieza con el e-Closure del estado inicial del AFN
            HashSet<Estado> array_inicial = simulador.eClosure(afn.Inicio);


            Console.WriteLine("aRRAY INICIAL");

            foreach (Object item in array_inicial)
            {
                Console.WriteLine("incial " +  item);
            }

            Console.WriteLine("ESTADO FINAL");

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
                 Console.WriteLine("cola lenght antes " + cola.Count());

                 //actual subconjunto
                 HashSet<Estado> actual = (HashSet<Estado>)cola.Dequeue();

                
                Console.WriteLine("SIMBOLO");
                //se recorre el subconjunto con cada simbolo del alfabeto del AFN

                foreach (Object simbolo in afn.Alfabeto)
                {
                    //se realiza el move con el subconjunto
                    Console.WriteLine("el simbolo es " + simbolo);

                    HashSet<Estado> move_result = simulador.move(actual, (String)simbolo);


                    foreach (Object item in move_result)
                    {
                        Console.WriteLine(item);
                    }


                    HashSet<Estado> resultado = new HashSet<Estado>();
                    //e-Closure con cada estado del resultado del move y 
                    //se guarda en un solo array (merge)
                    Console.WriteLine("ESTADOS");
                    Console.WriteLine("__________________");
                    foreach (Estado e in move_result)
                    {
                        Console.WriteLine(e);
                        resultado.UnionWith(simulador.eClosure2(e));
                    }
                    Console.WriteLine("resultado");
                    foreach (Object e in resultado)
                    {
                        Console.WriteLine(e);
                    }





                    Console.WriteLine("__________________");
                    Estado anterior = automata.Estados[indexEstadoInicio];
                    Console.WriteLine("el estado anterior es " + anterior);

                    //Estado anterior = (Estado)automata.getEstados().get(indexEstadoInicio);
                    //Si el subconjunto ya fue creado una vez, solo se agregan transiciones al automata
                    if (temporal.Contains(resultado))
                    {
                        Console.WriteLine("entro");
                        List<Estado> array_viejo = automata.Estados;
                        Estado estado_viejo = anterior;
                        //se busca el estado correspondiente y se le suma el offset
                        Estado estado_siguiente = array_viejo[temporal.IndexOf(resultado) + 1];
                        estado_viejo.agregarTransicion(new Transicion(estado_viejo, estado_siguiente, simbolo.ToString()));
                    }
                    //si el subconjunto no existe, se crea un nuevo estado
                    else
                    {
                        Console.WriteLine("entro al else");
                        Console.WriteLine("la cola tiene tama;o " + cola.Count());
                        temporal.Add(resultado);
                        cola.Enqueue(resultado);
                        Console.WriteLine("la cola tiene tama;o " + cola.Count());
                        Estado nuevo = new Estado(temporal.IndexOf(resultado) + 1);
                        anterior.agregarTransicion(new Transicion(anterior, nuevo, simbolo.ToString()));
                        automata.AgregarEstado(nuevo);
                        //se verifica si el estado tiene que ser de aceptacion
                        Console.WriteLine("ESTADO DE ACEPTACION VERIFICAR");
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
             //metodo para definir el alfabeto, se copia el del afn
             //definirAlfabeto(afn);
             this.afd.Tipo = ("AFD");
             //Console.WriteLine(afd);

     
        }

        private void definirAlfabeto(Automata afn)
        {
            this.afd.Alfabeto = afn.Alfabeto;
        }


    }
}
