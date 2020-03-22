using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    class EvaluatorController
    {
        private readonly static EvaluatorController instance = new EvaluatorController();
        ArrayList arrayAutomatas = new ArrayList();
        String error = "";
        private EvaluatorController()
        {
        }

        public static EvaluatorController Instance
        {
            get
            {
                return instance;
            }
        }

        //Insert
        public void Insert(string name, Automata.Automata afd)
        {
            Evaluator e = new Evaluator(name, afd);
            arrayAutomatas.Add(e);

        }


        /// <summary>
        /// Metodos de evaluacion
        /// </summary>
        /// <param name="expressionName"></param>
        /// <param name="strToEvaluate"></param>
        /// <returns></returns>

            //Evaluacion de caracteres, verifica que todos los caracretes coincidan
        public bool SimulateExpression(string expressionName, string strToEvaluate)
        {
            //Se crea un array que tendra el alfabeto modificado
            ArrayList new_alphabet = new ArrayList();
            bool validate = true;
            //Itera en los automatas guardados
            foreach (Evaluator item in arrayAutomatas)
            {
                //Busca el automata cuyo nombre coincida con el recivido por el metodo
                if (item.ExpressionName.Equals(expressionName))
                {
                    Automata.Automata afd_temp = item.Afd;

                    //Varibale para definir que tipo de alfabeto tiene el automata
                    //es decir: si tiene solo simbolos, conjuntos, cadenas, o mezcla entre ellas
                    int swcase = 0;

                    //Se itera en el afabeto;
                    foreach (var alphabet in afd_temp.Alfabeto)
                    {
                        //Quita las comillas y los espacios;
                        String alphabetChar = alphabet.Replace('"', ' ');
                        alphabetChar = alphabetChar.Trim();
                        
                        //Intenta convertir el alfabeto a char
                        Char value;
                        bool result;
                        result = Char.TryParse(alphabetChar, out value);


                        if (result)
                        {
                            //Si logra converit a char, significa que solo es un simbolo
                            //y lo agrega al nuevo alfabeto
                            new_alphabet.Add(alphabetChar);
                        }
                        else
                        {
                            //Si no logra convertir, significa que lo que vino fue una cadena o un conjunto


                            //Se busca los elementos del conjunto
                            ArrayList listChar = SetController.Instance.GetElemntsOfSet(alphabetChar);
                            //Verifica que la lista no venga vacia
                            if (listChar != null)
                            {
                                //se itera sobre los elementos del conjuntos
                                foreach (var letter in listChar)
                                {
                                    //Se agregan los elemenentos al alfabeto
                                    if (!new_alphabet.Contains(letter.ToString()))
                                    {
                                        new_alphabet.Add(letter.ToString());
                                    }
                                }


                            }
                            //Si la lista vienen vacia es por que vino una cadena
                            else if (listChar == null)
                            {
                                //la cadena se agrega directamente
                                new_alphabet.Add(alphabetChar);

                                swcase = 1;
                            }
                            else 
                            {
                                //Si no coincide con ninguno, el elemento no se encuentra dentro del alfabeto y marca error
                                error = "El conjunto '" + alphabetChar + "' no ha sido declarado. No se puede evaluar: " + strToEvaluate +".\n";
                                return false;
                            }
                        }
   
                    }

                    //Casos de desicion
                    //Caso 0: el alfabeto solo tiene simbolos, solo conjuntos o mezcla entre los dos.
                    //Caso 1; el alfabeto tiene solo cadenas o mezcla entre los demas


                    //Se itera sobre la cadena de entrada
                    //Quita las comillas y los espacios;
                    String str_temp = strToEvaluate.Replace('"', ' ');
                    str_temp = str_temp.Trim();


                    switch (swcase)
                    {
                        //Cuando solo vienen simbolos o conjuntos
                        case 0:

                            
                            //Variable 'booleana' sirve para determinar si continuar evaluando o no;
                            int countAux = 1;
                            Char ch = ' ';
                            for (int i = 0; i < str_temp.Length; i++)
                            {
                                //Verifica que todos los elementos esten dentro del alfabeto
                                ch = str_temp[i];

                                if (!new_alphabet.Contains(ch.ToString()))
                                {
                                    countAux = 0;
                                    break;
                                }
                            }
                            //Si contador sigue en uno es por que el alfabeto contiene todos los caracteres del 
                            //string a evaluar
                            if (countAux == 1)
                            {
                                //Se envia a evaluar la expresion
                                validate = ThompsonControlador.Instance.EvaluateExpression(str_temp, afd_temp, null, false);
                                error = "X La cadena " + strToEvaluate + " contiene errores.\n";
                            }
                            else
                            {
                                error = "X Error en " + strToEvaluate + ". El caracter " + ch + ", no se encuentra dentro del alfabeto\n";
                                return false;
                            }

                            break;


                        //Cuando vienen cadenas;
                        case 1:


                            String[] new_a = str_temp.Split(' ');
                            ArrayList nuevaCadenaTemp = new ArrayList();
                            ArrayList nuevaCadena = new ArrayList();

                            //VERIFICA QUE TODAS LAS CADENAS ESTA DENTRO DEL ALFABETO
                            for (int i = 0; i < new_a.Length; i++)
                            {
                                //elemento en la cadena de entrada
                                String elementInString = new_a[i];

                                int cont = 0;
                                int index = 0;



                                for (int j = 0; j < new_alphabet.Count; j++)
                                {
                                    String evaluator = (String)new_alphabet[j];
                                    if (evaluator.Contains(elementInString))
                                    {

                                        index = j;
                                        cont = 1;
                                        break;
                                    }
                                }
                                //Significa que encontro el elemento
                                if (cont == 1)
                                {
                                    String e = (String)new_alphabet[index];
                                    if (!nuevaCadena.Contains(e))
                                    {
                                        nuevaCadena.Add(e);
                                    }
                                }
                                else
                                {
                                    error = "X Error en " + strToEvaluate + ". La cadena " + i + ", no se encuentra dentro del alfabeto\n";
                                    return false;
                                }


                            }
                            validate = ThompsonControlador.Instance.EvaluateExpression(str_temp, afd_temp, nuevaCadena, true);
                            if (!validate)
                            {
                                error = "X La cadena " + strToEvaluate + " contiene errores.\n";
                                return false;
                            }

                            break;
                        default:
                            break;
                    }

                    break;
                }

            }
            return validate;
        }



        //Simula la expresion con cadenas en lugar de caracteres, es decir, en el alfabeto del automata exiten
        //cadenas como "hola", "adios" en lugar de solo elementos tipo "a", "b", "c"
        public bool SimulateExpressionWhitString(string expressionName, string strToEvaluate)
        {

            
            
            //Se crea un array que tendra el alfabeto modificado
            HashSet<String> new_alphabet = new HashSet<String>();
            bool validate = true;



            //Itera en los automatas guardados
            foreach (Evaluator item in arrayAutomatas)
            {
                //Busca el automata cuyo nombre coincida con el recivido por el metodo
                if (item.ExpressionName.Equals(expressionName))
                {
                    Automata.Automata afd_temp = item.Afd;


                    //Recreando el nuevo alfabeto


                    foreach (var element in afd_temp.Alfabeto)
                    {

                        Char value;
                        bool result;
                        result = Char.TryParse(element, out value);



                    }






























                    /*//Se itera sobre la cadena de entrada

                    //Quita las comillas y los espacios;
                    String str_temp = strToEvaluate.Replace('"', ' ');
                    ArrayList elements_to_Evaluate = LexicoController.Instance.AnalizerStringToEvaluate(str_temp);

                    //Variable 'booleana' sirve para determinar si continuar evaluando o no;
                    int countAux = 0;
                    String strNotValid = "";

                    foreach (var alfabeto in afd_temp.Alfabeto)
                    {
                        foreach (String element in elements_to_Evaluate)
                        {
                            if (alfabeto.Contains(element))
                            {
                                countAux++;
                                Console.WriteLine("entro");
                            }
                        }
                    }
                    //Si countAux es igual que el tama;o el arreglo significa que todos los elementos estan en el alfabeto
                    if (countAux == elements_to_Evaluate.Count)
                    {
                        validate = ThompsonControlador.Instance.EvaluateString(elements_to_Evaluate, afd_temp);
                        error = "X La cadena " + strToEvaluate + " contiene errores.\n";

                    }
                    else
                    {
                        error = "X Error en " + strToEvaluate + ". Uno o mas elementos no se encuentran dentro del alfabeto\n";
                        return false;
                    }*/


                    break;
                }

            }
            return validate;
        }





        public String GetError()
        {
            return error;
        }
        public void clearList()
        {
            arrayAutomatas.Clear();
        }

    }
}
