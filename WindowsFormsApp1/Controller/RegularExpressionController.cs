using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Automata;
using WindowsFormsApp1.Model;
namespace WindowsFormsApp1.Controller
{
    class RegularExpressionController
    {
        private readonly static RegularExpressionController instance = new RegularExpressionController();
        private ArrayList arrayListER = new ArrayList();
        private Stack stk = new Stack();
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
                                    if (t2.Description.Equals("TK_Suma")) //Hace reemplazo de +a -> . a* a 
                                    {
                                        String a = ((Token)l[j + 1]).Lexema;
                                        a = a.Replace('"', ' ');
                                        temp.Add(".");

                                        temp.Add(a.Trim());
                                        temp.Add("*");
                                        temp.Add(a.Trim());
                                        j = j + 1;
                                    }
                                    /*else if (t2.Lexema.Equals("?"))
                                    {
                                        temp.Add("|");
                                        String a = ((Token)l[j + 1]).Lexema;
                                        a = a.Replace('"', ' ');
                                        temp.Add(a.Trim());
                                        temp.Add("\"ε\"");
                                        j = j + 1;
                                    }*/
                                    else
                                    {
                                        string a = t2.Lexema.Replace('"', ' ');
                                        temp.Add(a.Trim());
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
            
        }


        





        public void Insert(String name, ArrayList ar, String path)
        {
            ArrayList vuelta = new ArrayList(); // array que va a almacenar los elementos en orden inverso
            for (int i = ar.Count-1; i >= 0; i--)
            {

                vuelta.Add(ar[i]);
                NodeController.getInstancia().InsertStack(ar[i].ToString());

                /*if (ar[i].Equals("?"))//ε
                {
                    vuelta.Remove(ar[i + 1]);
                    vuelta.Add("|");
                    vuelta.Add(ar[i+1]);
                    vuelta.Add("ε");
                    NodeController.getInstancia().InsertStack("ε");
                    NodeController.getInstancia().InsertStack(ar[i+1].ToString());
                    NodeController.getInstancia().InsertStack("|");
                }
                else
                {
                   
                }*/

            }
            String ast = "";
            foreach (var item in vuelta)
            {
                ast = ast + " " + item;
            }

            Console.WriteLine(ast);

            RegularExpression re = new RegularExpression(name, vuelta);
            arrayListER.Add(re);
            //NodeController.getInstancia().Print(name, path);
            NodeController.getInstancia().Print(name, path);



            //Convierte la expresion regular de prefija a pos
            NodeController.getInstancia().ConvertExpression(NodeController.getInstancia().getRoot());

            ArrayList regularExpresion = NodeController.getInstancia().getRegularExpression();
            ArrayList regex = new ArrayList();
            

            try
            {
                regex = RegexController.Instance.infixToPostfix(regularExpresion);
            }
            catch (Exception a)
            {
                Console.WriteLine("Expresión mal ingresada");
            }

            string st = "";

            foreach (var item in regularExpresion)
            {
                st = st + item;
            }

            Console.WriteLine(name+"->"+st);

            //CONSTRUYE EL AUTOMATA ANF
            AFN aFN = new AFN();
            aFN.construirAutomata(regex);
            Automata.Automata afn_result = aFN.Afn;
            ThompsonControlador.Instance.generarDOT("AFN", name, afn_result);

            //CONSTRUYE EL AUTOMATA AFD
            AFD AFD = new AFD();
            AFD.conversionAFN(afn_result);
            Automata.Automata afd_result = AFD.Afd;

            //CONSTRUYE EL AUTOMATA SIN ESTADO STRAMPA
            Automata.Automata afd_trampa = AFD.RemoveCheatStates(afd_result);
            ThompsonControlador.Instance.generarDOT("AFD", name, afd_trampa);

            //CONSTRUYE LA TABLA
            ThompsonControlador.Instance.TableConstructor(name, path, afd_trampa);

            //ENVIA EL AUTOMATA A SER GUARDADO PARA POSTERIOR EVALUACION
            EvaluatorController.Instance.Insert(name, afd_trampa);


            NodeController.getInstancia().clearList();

        }

        public ArrayList getArrayListER()
        {
            return arrayListER;
        }



    }
}
