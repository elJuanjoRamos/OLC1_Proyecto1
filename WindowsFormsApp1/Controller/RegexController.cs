using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Controller
{
    class RegexController
    {
        /** Mapa de precedencia de los operadores. */
        private Dictionary<Char, int> precedenciaOperadores;


        private readonly static RegexController instance = new RegexController();
        public RegexController()
        {

            
        }
        public static RegexController Instance
        {
            get
            {
                return instance;
            }
        }



        /**
	     * Obtener la precedencia del caracter
	     * 
	     * @param c character
	     * @return corresponding precedence
	     */
        private int getPrecedencia(String c)
        {

            if (c.Equals("("))
            {
                return 1;
            } else if (c.Equals("|"))
            {
                return 2;
            }
            else if (c.Equals("."))
            {
                return 3;
            }
            else if (c.Equals("*") || c.Equals("+") || c.Equals("?"))
            {
                return 4;
            } else
            {
                return 6;
            }
        }

        /**
        * Insertar caracter en una posicion deseada
        * @param s string deseado
        * @param pos indice del caracter
        * @param ch caracter  o String deseado
        * @return nuevo string con el caracter deseado
        */
        private String InsertCharAt(String s, int pos, Object ch)
        {
            return s.Substring(0, pos) + ch + s.Substring(pos + 1);
        }


        /**
         * Agregar caracter en la posicion deseada (no elimina el caracter anterior)
         * @param s string deseado
         * @param pos posicion del caracter
         * @param ch caracter deseado
         * @return nuevo string con el caracter agregado
         */
        private String AppendCharAt(String s, int pos, Object ch)
        {
            String val = s.Substring(pos, pos + 1);
            return s.Substring(0, pos) + val + ch + s.Substring(pos + 1);

        }

        /**
         * Metodo para abreviar el operador ? 
         * equivalente a |€
         * @param regex expresion regular
         * @return expresion regular modificada sin el operador ?
         */
        public String QAvreviature(ArrayList regex)
        {
            return null;
            /*for (int i = 0; i < regex.Count; i++)
            {
                
                String ch = (String)regex[i];

                if (ch.Equals("?"))
                {
                    if (((String)regex[i - 1]) == ")")
                    {
                        regex = InsertCharAt(regex, i, "|" + "ε" + ")");
                        int j = i;
                        while (j != 0)
                        {
                            if ((String)regex[j] == "(")
                            {
                                break;
                            }

                            j--;

                        }

                        regex = AppendCharAt(regex, j, "(");

                    }
                    else
                    {
                        regex = InsertCharAt(regex, i, "|" + "ε" + ")");
                        regex = InsertCharAt(regex, i - 1, "(" + regex[i - 1]);
                    }
                }
            }
            regex = BalanceParentheses(regex);
            return regex;*/
        }

        /**
         * Método para contar los parentesis izquierdos '('
         * @param regex String expresion regular
         * @return int contador
         */
        private int ParentesisIzq(String regex)
        {
            int P1 = 0;
            for (int i = 0; i < regex.Length; i++)
            {
                char ch = regex[i];
                if (ch.Equals('('))
                {
                    P1++;
                }

            }
            return P1;
        }
        /**
         * Método para contar los parentesis derechos ')'
         * @param regex String expresion regular
         * @return int contador 
         */
        private int ParentesisDer(String regex)
        {
            int P1 = 0;
            for (int i = 0; i < regex.Length; i++)
            {
                char ch = regex[i];
                if (ch.Equals(')'))
                {
                    P1++;
                }
            }
            return P1;
        }
        /**
         * Método para balancear parentesis en caso de que esté mal ingresada
         * la expresión regular
         * @param regex String expresión regular
         * @return String expresion regular modificada
         */
        private String BalanceParentheses(String regex)
        {
            //corregir parentesis de la expresion en caso que no esten balanceados
            int P1 = ParentesisIzq(regex);
            int P2 = ParentesisDer(regex);


            while (P1 != P2)
            {
                if (P1 > P2)
                    regex += ")";
                if (P2 > P1)
                    regex = "(" + regex;
                P1 = ParentesisIzq(regex);
                P2 = ParentesisDer(regex);
            }
            return regex;
        }

        /**
         * Método para abreviar el operador de cerradura positiva
         * @param regex expresion regular (string)
         * @return expresion regular modificada sin el operador +
         */
        public String KleeneAbreviature(String regex)
        {
            //sirve para buscar el '(' correcto cuando  hay () en medio
            // de la cerradura positiva
            /*int compare = 0;

            for (int i = 0; i < regex.Length; i++)
            {
                char ch = regex[i];

                if (ch.Equals('+'))
                {
                    //si hay un ')' antes de un operador
                    //significa que hay que buscar el '(' correspondiente
                    if (regex[i - 1] == ')')
                    {

                        int fixPosicion = i;

                        while (fixPosicion != -1)
                        {
                            if (regex[fixPosicion] == ')')
                            {
                                compare++;

                            }

                            if (regex[fixPosicion] == '(')
                            {

                                compare--;
                                if (compare == 0)
                                    break;
                            }


                            fixPosicion--;

                        }

                        String regexAb = regex.Substring(fixPosicion, i);
                        regex = InsertCharAt(regex, i, regexAb + "*");


                    }
                    //si no hay parentesis, simplemente se inserta el caracter
                    else
                    {
                        regex = InsertCharAt(regex, i, regex[i - 1] + "*");
                    }


                }

            }*/
            //regex = BalanceParentheses(regex);
            return regex;
        }


        public ArrayList FormatRegEx(ArrayList regex)
        {
            //regex = regex.Trim();
            //regex = QAvreviature(regex);
            //regex = KleeneAbreviature(regex);
            ArrayList regexExplicit = new ArrayList();
            List<String> operadores = new List<String>();
            operadores.Add("|");
            operadores.Add("?");
            operadores.Add("+");
            operadores.Add("*");
            List<String> operadoresBinarios = new List<String>();
            operadoresBinarios.Add("|");

            //recorrer la cadena
            for (int i = 0; i < regex.Count; i++)
            {
                String c1 = (String)regex[i];

                if (i + 1 < regex.Count)
                {

                    String c2 = (String)regex[i + 1];

                    regexExplicit.Add(c1);

                    //mientras la cadena no incluya operadores definidos, será una concatenación implicita
                    if (!c1.Equals("(") && !c2.Equals(")") && !operadores.Contains(c2) && !operadoresBinarios.Contains(c1))
                    {
                        regexExplicit.Add(".");

                    }

                }
            }
            regexExplicit.Add(regex[regex.Count - 1]);


            return regexExplicit;
        }

        public ArrayList abreviacionOr(ArrayList regex)
        {
            return regex;
            /*ArrayList resultado =new ArrayList();
            try
            {
                for (int i = 0; i < regex.Count; i++)
                {
                    String ch = (String)regex[i];
                    if (ch == "[")
                    {
                        if (((String)regex[i + 2]) == "-")
                        {
                            int inicio = regex[i + 1];
                            int fin = regex[i + 3];
                            resultado += "(";
                            for (int j = 0; j <= fin - inicio; j++)
                            {
                                if (j == (fin - inicio))
                                    resultado += char.ToString((char)(inicio + j));
                                else
                                    resultado += char.ToString((char)(inicio + j)) + '|';
                            }
                            resultado += ")";
                            i = i + 4;
                        }
                        else
                        {
                            resultado += ch;
                        }
                    }
                    else
                    {
                        resultado += ch;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la conversión " + regex);
                resultado = " ";
            }

            return resultado;*/
        }

        public String abreviacionAnd(String regex)
        {
            String resultado = "";
            try
            {
                for (int i = 0; i < regex.Length; i++)
                {
                    char ch = regex[i];
                    if (ch == '[')
                    {
                        if (regex[i + 2] == '.')
                        {
                            int inicio = regex[i + 1];
                            int fin = regex[i + 3];
                            resultado += "(";
                            for (int j = 0; j <= fin - inicio; j++)
                            {

                                resultado += char.ToString((char)(inicio + j));
                            }
                            resultado += ")";
                            i = i + 4;
                        }
                    }
                    else
                    {
                        resultado += ch;
                    }
                    //System.out.println(resultado);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la conversion " + regex);
                resultado = regex;
            }
            return resultado;
        }


        /**
	 * Convertir una expresión regular de notación infix a postfix 
	 * con el algoritmo de Shunting-yard. 
	 * 
	 * @param regex notacion infix 
	 * @return notacion postfix 
	 */
        public ArrayList infixToPostfix(ArrayList regex)
        {
            ArrayList postfix = new ArrayList();
            //regex = abreviacionOr(regex);
            //regex = abreviacionAnd(regex);
            Stack<String> stack = new Stack<String>();

            ArrayList formattedRegEx = FormatRegEx(regex);
            //System.out.println(formattedRegEx);
            foreach(String c in formattedRegEx)
            {
                switch (c)
                {
                    case "(":
                        stack.Push(c);
                        break;

                    case ")":
                        while (!stack.Peek().Equals("("))
                        {
                            postfix.Add(stack.Pop());
                        }
                        stack.Pop();
                        break;

                    default:
                        while (stack.Count() > 0)
                        {
                            String peekedChar = stack.Peek();
                            int peekedCharPrecedence = getPrecedencia(peekedChar);
                            int currentCharPrecedence = getPrecedencia(c);


                            if (peekedCharPrecedence >= currentCharPrecedence)
                            {
                                postfix.Add(stack.Pop());

                            }
                            else
                            {
                                break;
                            }
                        }
                        stack.Push(c);
                        break;
                }

            }

            while (stack.Count() > 0)
                postfix.Add(stack.Pop());
            return postfix;
        }

    }
}
