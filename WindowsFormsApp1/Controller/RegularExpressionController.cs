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
        private ArrayList arrayListAFD = new ArrayList();
        private ArrayList arrayListAFN = new ArrayList();
        private ArrayList arrayListTabla = new ArrayList();
        private ArrayList arrayListArbol = new ArrayList();

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

                if (t.Lexema.ToLower().Equals("conj"))
                {
                    for (int j = i+1; j < l.Count; j++)
                    {
                        if (((Token)l[j]).Lexema.Equals(";"))
                        {
                            i = j;
                            break;
                        }
                    }
                }
                else
                {

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

                        Token t1 = (Token)l[i + 2]; // token de inicio de la expresion

                        if (t1 != null && !t1.Lexema.Equals("~") && !t1.Lexema.Equals(","))
                        {

                            //itera en la expresion y guarda los elementos
                            for (int j = i + 1; j < l.Count; j++)
                            {
                                Token t2 = (Token)l[j];
                                if (!t2.Lexema.Equals(";")) //El limite de la expresion es el punto y coma
                                {
                                    if (!t2.Lexema.Equals("{") && !t2.Lexema.Equals("}"))
                                    {
                                        string a = t2.Lexema;
                                        temp.Add(a);
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
            
        }


        





        public void Insert(String name, ArrayList ar, String path)
        {
            ArrayList vuelta = new ArrayList(); // array que va a almacenar los elementos en orden inverso
            for (int i = ar.Count-1; i >= 0; i--)
            {

                vuelta.Add(ar[i]);
                NodeController.getInstancia().InsertStack(ar[i].ToString());

            }
            String ast = "";
            foreach (var item in vuelta)
            {
                ast = ast + " " + item;
            }

            Console.WriteLine(ast);

            RegularExpression re = new RegularExpression(name, vuelta);
            arrayListER.Add(re);
            NodeController.getInstancia().Print(name, path);



            //Convierte la expresion regular de prefija a pos
            ArrayList regularExpresion =  NodeController.getInstancia().ConvertExpression(NodeController.getInstancia().getRoot());
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
            InsertAutomataAFNName("AFN "+ name);
            //CONSTRUYE EL AUTOMATA AFD
            AFD AFD = new AFD();
            AFD.conversionAFN(afn_result);
            Automata.Automata afd_result = AFD.Afd;
          
            //CONSTRUYE EL AUTOMATA SIN ESTADO STRAMPA
            Automata.Automata afd_trampa = AFD.RemoveCheatStates(afd_result);
            ThompsonControlador.Instance.generarDOT("AFD", name, afd_trampa);
            InsertAutomataAFDName("AFD "+ name);




            //CONSTRUYE LA TABLA
            ThompsonControlador.Instance.TableConstructor(name, path, afd_trampa);
            InsertTablaName(name+"Table");
            //ENVIA EL AUTOMATA A SER GUARDADO PARA POSTERIOR EVALUACION
            EvaluatorController.Instance.Insert(name, afd_trampa);


            NodeController.getInstancia().clearList();

        }

        public ArrayList getArrayListER()
        {
            return arrayListER;
        }

        public void InsertAutomataAFDName(string name)
        {
            arrayListAFD.Add(name);
        }
        public void InsertTablaName(string name)
        {
            arrayListTabla.Add(name);
        }
        public ArrayList GetAFDAutomata()
        {
            return arrayListAFD;
        }
        public void InsertAutomataAFNName(string name)
        {
            arrayListAFN.Add(name);
        }
        public void InsertArbol(string name)
        {
            arrayListArbol.Add(name);
        }
        public ArrayList GetAFNAutomata()
        {
            return arrayListAFN;
        }
        public ArrayList GetTabla()
        {
            return arrayListTabla;
        }
        public ArrayList GetArbol()
        {
            return arrayListArbol;
        }

        public void ClearList()
        {
            arrayListAFD.Clear();
            arrayListAFN.Clear();
            arrayListTabla.Clear();
            arrayListArbol.Clear();
        }

    }
}
