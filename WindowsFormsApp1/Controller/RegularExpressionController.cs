using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Nodes;
using WindowsFormsApp1.Lists;
namespace WindowsFormsApp1.Controller
{
    class RegularExpressionController
    {
        private readonly static RegularExpressionController instance = new RegularExpressionController();
        private ArrayList arrayListER = new ArrayList();
        private Stack stk = new Stack();
        private ConcatList concatlist = new ConcatList();
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

        public void GetElements(String path)
        {
            String texto = "";
            ArrayList l = TokenController.Instance.getArrayListTokens();
            for (int i = 0; i < l.Count; i++)
            {
                ArrayList temp = new ArrayList(); //Array que va a almacenar los elementos de la expresion
                Token t = (Token)l[i];
                if (t.Lexema.Equals(">"))
                {
                    
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

                //Token t1 = (Token)l[i + 1]; // token de inicio de la expresion
                    
                    //itera en la expresion y guarda los elementos
                    for (int j = i + 1; j < l.Count; j++)
                    {
                        Token t2 = (Token)l[j];
                        if (!t2.Lexema.Equals(";")) //El limite de la expresion es el punto y coma
                        {
                            if (!t2.Lexema.Equals("{") && !t2.Lexema.Equals("}"))
                            {
                                if (t2.Description.Equals("TK_Suma")) //Hace reemplazo de +a -> . a* a 
                                {
                                    temp.Add(".");
                                    temp.Add(((Token)l[j+1]).Lexema);
                                    temp.Add("*");
                                    temp.Add(((Token)l[j + 1]).Lexema);
                                    j = j + 1;
                                } else
                                {
                                    temp.Add(t2.Lexema);
                                }
                                    
                            } 
                        }
                        else
                        {
                            Insert(texto, temp, path);
                            i = j;
                            break;
                        }
                    }
                }
            }
            
        }

        public void Insert(String name, ArrayList ar, String path)
        {
            ArrayList vuelta = new ArrayList(); // array que va a almacenar los elementos en orden inverso
            for (int i = ar.Count-1; i >= 0; i--)
            {
                if (ar[i].Equals("?"))//ε
                {
                    vuelta.Add("\"ε\"");
                    vuelta.Add("|");
                    NodeController.getInstancia().InsertStack("|");
                   // Console.WriteLine("|");
                   
                    //NodeController.getInstancia().InsertStack("ε");
                    //Console.WriteLine("\"ε\"");
                } else
                {
                   vuelta.Add(ar[i]);
                   NodeController.getInstancia().InsertStack(ar[i].ToString());
                   
                }
            }
            /*for (int i = 0; i < ar.Count; i++)
            {
                vuelta.Add(ar[i]);
                //NodeController.getInstancia().InsertStack(ar[i].ToString());
            }*/
            RegularExpression re = new RegularExpression(name, vuelta);
            arrayListER.Add(re);
            //NodeController.getInstancia().Print(name, path);
            NodeController.getInstancia().Print(name, path);
        }

        public ArrayList getArrayListER()
        {
            return arrayListER;
        }


        public void imprimir()
        {
           
        }








    }
}
