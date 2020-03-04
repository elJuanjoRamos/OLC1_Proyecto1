using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
namespace WindowsFormsApp1.Controller
{
    class RegularExpressionController
    {
        private readonly static RegularExpressionController instance = new RegularExpressionController();
        private ArrayList arrayListER = new ArrayList();

        public RegularExpressionController()
        {

        }
        public static RegularExpressionController Instance
        {
            get
            {
                return instance;
            }
        }

        public void GetElements()
        {

            ArrayList l = TokenController.Instance.getArrayListTokens();
            for (int i = 0; i < l.Count; i++)
            {
                ArrayList temp = new ArrayList(); //Array que va a almacenar los elementos de la expresion
                Token t = (Token)l[i];
                if (t.Lexema.Equals(">"))
                {
                    String texto = "";

                    //busca el nombre de la expresion
                    for (int j = i; j > 0; j--)
                    {
                        Token a = (Token)l[j];
                        if (a.Description.Equals("Identificador"))
                        {
                            texto = a.Lexema;
                            break;
                        }
                    }

                    Token t1 = (Token)l[i + 1]; // token de inicio de la expresion
                    if (t1 != null && t1.Lexema.Equals("."))
                    {
                        //itera en la expresion y guarda los elementos
                        for (int j = i + 1; j < l.Count; j++)
                        {
                            Token t2 = (Token)l[j];
                            if (!t2.Lexema.Equals(";")) //El limite de la expresion es el punto y coma
                            {
                                if (!t2.Lexema.Equals("{") && !t2.Lexema.Equals("}"))
                                {
                                    temp.Add(t2.Lexema);
                                }
                            }
                            else
                            {
                                Insert(texto, temp);
                                i = j;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Insert(String name, ArrayList ar)
        {
            ArrayList vuelta = new ArrayList(); // array que va a almacenar los elementos en orden inverso
            for (int i = ar.Count-1; i >= 0; i--)
            {
                vuelta.Add(ar[i]);
            }

            RegularExpression re = new RegularExpression(name, vuelta);
            arrayListER.Add(re);
        }


        public void Show()
        {
            foreach (RegularExpression regular in arrayListER)
            {
                regular.toString();
            }
        }
    }
}
