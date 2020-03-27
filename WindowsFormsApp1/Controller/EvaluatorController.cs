using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    class EvaluatorController
    {
        private readonly static EvaluatorController instance = new EvaluatorController();
        ArrayList arrayAutomatas = new ArrayList();
        ArrayList arrayTokensEvaluados = new ArrayList();
        ArrayList arrayErroresEvaluados = new ArrayList();

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
            clearTokens();

            if (strToEvaluate.Contains("\\n"))
            {
                strToEvaluate.Replace("\\n", ('\n').ToString());
            }
            if (strToEvaluate.Contains("\\r"))
            {
                strToEvaluate.Replace("\\r", ('\n').ToString());
            }
            if (strToEvaluate.Contains("\\t"))
            {
                strToEvaluate.Replace("\\t", ('\t').ToString());
            }

            //Se crea un array que tendra el alfabeto modificado
            ArrayList new_alphabet = new ArrayList();
            bool validate = false;
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
                        String alphabetChar = alphabet.Trim('"');
                        
                        //Intenta convertir el alfabeto a char
                        Char value;
                        bool result;
                        result = Char.TryParse(alphabetChar, out value);



                        
                        if (result && (SetController.Instance.GetElemntsOfSet(alphabetChar) == null))
                        {
                            if (result || alphabetChar.Equals("\\n") || alphabetChar.Equals("\\t") || alphabetChar.Equals("\\r")
                            || alphabetChar.Equals("\"") || alphabetChar.Equals("\'"))
                            {
                                if (alphabetChar.Equals("\\n"))
                                {

                                    new_alphabet.Add(('\n').ToString()); ;
                                }
                                else if (alphabetChar.Equals("\\t"))
                                {
                                    new_alphabet.Add(('\t').ToString()); ;
                                }
                                else if (alphabetChar.Equals("\\r"))
                                {
                                    new_alphabet.Add(('\r').ToString()); ;
                                }
                                else
                                {
                                    new_alphabet.Add(alphabetChar);
                                }
                                //Si logra converit a char o si es un caracter especial, significa que solo es un simbolo
                                //y lo agrega al nuevo alfabeto
                            }
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
                                    String letter_temp = letter.ToString().Trim('"');
                                    
                                    //Se agregan los elemenentos al alfabeto
                                    if (!new_alphabet.Contains(letter_temp))
                                    {
                                        new_alphabet.Add(letter_temp.ToString());
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
                    String str_temp = strToEvaluate.Trim('"');




                    //Variable 'booleana' sirve para determinar si continuar evaluando o no;
                    int countAux = 1;
                    Char ch = ' ';
                    for (int i = 0; i < str_temp.Length; i++)
                    {
                        //Verifica que todos los elementos esten dentro del alfabeto
                        ch = str_temp[i];

                        if (!new_alphabet.Contains(ch.ToString()))
                        {
                            //sirve para ver si las cadenas contienen el elemnto
                            int contadorInterno = 0;
                            foreach (string str in new_alphabet)
                            {
                                if (str.Length > 1)
                                {
                                    if (str.Contains(ch.ToString()))
                                    {
                                        contadorInterno = 1;
                                        break;
                                    }
                                }
                            }

                            if (contadorInterno != 1)
                            {
                                countAux = 0;
                                //Si algun elemento no se encuentra del alfabeto lo guarda en un array de errores
                                Token t = new Token(0, str_temp[i].ToString(), "Carcter_" + str_temp[i].ToString(), i, 0);
                                arrayErroresEvaluados.Add(t);
                                break;
                            }
                        }
                    }

                    //Uno o mas caracteres no se encuentran dentro del alfabeto del automata
                    if (countAux == 0)
                    {
                        error = "X Error en " + strToEvaluate + ". El caracter " + ch + ", no se encuentra dentro del alfabeto\n";
                        return false;
                    }


                    switch (swcase)
                    {
                        //Cuando solo vienen simbolos o conjuntos
                        case 0:
                            //Se envia a evaluar la expresion
                            validate = ThompsonControlador.Instance.EvaluateExpression(str_temp, afd_temp, null, false);
                            break;


                        //Cuando vienen cadenas;
                        case 1:

                            //Array que va a guardar unicamente las cadenas
                            ArrayList cadenas = new ArrayList();

                            ArrayList cadenas2 = new ArrayList();

                            ArrayList cabezas = new ArrayList();

                            //Esta variable va a ser un contador de letras 
                            String iteradorCadena = "";
                            int cont = 0;

                            foreach (String i in new_alphabet)
                            {
                                if (i.Length > 1)
                                {
                                    if (i.Contains("\\n"))
                                    {
                                        cadenas.Add(i.Replace("\\n", ('\n').ToString()));
                                    }
                                    if (i.Contains("\\r"))
                                    {
                                        
                                        cadenas.Add(i.Replace("\\r", ('\n').ToString()));
                                    }
                                    if (i.Contains("\\t"))
                                    {
                                        cadenas.Add(i.Replace("\\t", ('\t').ToString()));
                                    }
                                    else
                                    {
                                        cadenas.Add(i);
                                    }
                                    cabezas.Add(i[0].ToString());
                                }
                            }


                            for (int i = 0; i < str_temp.Length; i++)
                            {

                                char a = str_temp[i];

                                if (cabezas.Contains(a.ToString()))
                                {
                                    int contInterno = 0;
                                    for (int j = i; j < str_temp.Length; j++)
                                    {
                                        iteradorCadena = iteradorCadena + str_temp[j];
                                        if (cadenas.Contains(iteradorCadena))
                                        {
                                            i = j;
                                            contInterno = 1;
                                            break;
                                        }
                                    }
                                    if (contInterno != 0)
                                    {
                                        cadenas2.Add(iteradorCadena);
                                        iteradorCadena = "";
                                    }
                                    else
                                    {
                                        cadenas2.Add(str_temp[i].ToString());
                                    }
                                }
                                else
                                {
                                    cadenas2.Add(str_temp[i].ToString());
                                }
                                iteradorCadena = "";
                            }
                            validate = ThompsonControlador.Instance.EvaluateExpression(str_temp, afd_temp, cadenas2, true);
                            break;
                        default:
                            break;
                    }








                    //si la validacion fue exitosa significa que la cadena fue valida
                    if (validate)
                    {
                        //Se agregan los tokens evaluados a la lista que se va a imprimir
                        for (int i = 0; i < str_temp.Length; i++)
                        {
                            Token t = new Token(0, str_temp[i].ToString(), "Carcter_" + str_temp[i].ToString(), i, 0);
                            arrayTokensEvaluados.Add(t);
                        }

                    }
                    else
                    {

                        error = "X " +ThompsonControlador.Instance.getError() + "\n";
                        for (int i = 0; i < str_temp.Length; i++)
                        {
                            Token t = new Token(0, str_temp[i].ToString(), "Carcter_" + str_temp[i].ToString(), i, 0);
                            arrayErroresEvaluados.Add(t);
                        }
                    }
                    break;
                }

            }
            return validate;
        }


        public void getError(String token, int col)
        {
            Token t = new Token(0, token, "Carcter_" + token, col, 0);
            arrayErroresEvaluados.Add(t);
        }

        public String GetError()
        {
            return error;
        }
        public void clearList()
        {
            arrayAutomatas.Clear();
            arrayTokensEvaluados.Clear();
            arrayErroresEvaluados.Clear();
        }
        public void clearTokens()
        {
            arrayTokensEvaluados.Clear();
            arrayErroresEvaluados.Clear();
        }


        public void reportToken(String path, String nombre)
        {
            XDocument d = new XDocument(new XDeclaration("1.0", "utf-8", null));
            XElement root = new XElement("ListaToken");
            d.Add(root);
            foreach (Token t in arrayTokensEvaluados)
            {
                XElement element = new XElement("Token");
                XElement n = new XElement("Nombre");
                XElement v = new XElement("Valor");
                XElement f = new XElement("Fila");
                XElement c = new XElement("Columna");
                element.Add(n);
                n.Add(t.Description);
                element.Add(v);
                v.Add(t.Lexema);
                element.Add(f);
                f.Add(t.Row);
                element.Add(c);
                c.Add(t.Column);
                root.Add(element);
            }

            // Determine whether the directory exists.
            if (!Directory.Exists(path + "\\Reports\\Tokens"))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path + "\\Reports\\Tokens");
            }

            String tokenPath = path + "\\Reports\\Tokens\\XMLTokens " + nombre + ".xml";

            d.Save(tokenPath);
        }

        public void reportError(String path, String nombre)
        {
            XDocument d = new XDocument(new XDeclaration("1.0", "utf-8", null));
            XElement root = new XElement("ListaErrores");
            d.Add(root);
            foreach (Token t in arrayErroresEvaluados)
            {
                XElement element = new XElement("Error");
                XElement n = new XElement("Nombre");
                XElement v = new XElement("Valor");
                XElement f = new XElement("Fila");
                XElement c = new XElement("Columna");
                element.Add(n);
                n.Add(t.Description);
                element.Add(v);
                v.Add(t.Lexema);
                element.Add(f);
                f.Add(t.Row);
                element.Add(c);
                c.Add(t.Column);
                root.Add(element);
            }

            // Determine whether the directory exists.
            if (!Directory.Exists(path + "\\Reports\\Errors"))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path + "\\Reports\\Errors");
            }

            String errorPath = path + "\\Reports\\Errors\\XmlError " + nombre +".xml";

            d.Save(errorPath);
        }
    }
}
