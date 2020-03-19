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
            HashSet<String> new_alphabet = new HashSet<String>();
            bool validate = true;
            //Itera en los automatas guardados
            foreach (Evaluator item in arrayAutomatas)
            {
                //Busca el automata cuyo nombre coincida con el recivido por el metodo
                if (item.ExpressionName.Equals(expressionName))
                {
                    Automata.Automata afd_temp = item.Afd;

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
                            new_alphabet.Add(alphabetChar);
                        }
                        else
                        {
                            //Si no logra convertir, significa que lo que vino fue un dientificador de conjunto
                            //Se busca los elementos del conjunto
                            ArrayList listChar = SetController.Instance.GetElemntsOfSet(alphabetChar);
                            //Verifica que la lista no venga vacia
                            if (listChar != null)
                            {
                                foreach (var letter in listChar)
                                {
                                    //Se agregan los elemenentos al alfabeto
                                    if (!new_alphabet.Contains(letter.ToString()))
                                    {
                                        new_alphabet.Add(letter.ToString());
                                    }
                                }
                            } else
                            {
                                error = "El conjunto '" + alphabetChar + "' no ha sido declarado. No se puede evaluar: " + strToEvaluate +".\n";
                                return false;
                            }
                        }
   
                    }


                    //Se itera sobre la cadena de entrada
                   
                    //Quita las comillas y los espacios;
                    String str_temp = strToEvaluate.Replace('"', ' ');
                    str_temp = str_temp.Trim();

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
                        validate = ThompsonControlador.Instance.EvaluateExpression(str_temp, afd_temp);
                        error = "X La cadena " + strToEvaluate + " contiene errores.";
                    }
                    else
                    {
                        error = "X Error en " + strToEvaluate + ". El caracter " + ch + ", no se encuentra dentro del alfabeto";
                        return false;
                    }

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
