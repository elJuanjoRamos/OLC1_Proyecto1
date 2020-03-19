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

   
        public ArrayList infixToPostfix(ArrayList regex)
        {
            ArrayList postfix = new ArrayList();
            Stack<String> stack = new Stack<String>();

            ArrayList formattedRegEx = FormatRegEx(regex);
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
