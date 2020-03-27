using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    class NodeController
    {

        private ArrayList listAux = new ArrayList();//Lista que contendra los elementos de la expresion regular
        private ArrayList list = new ArrayList();  //Lista que servira para reordenar los elementos de la expresion regular
        private ArrayList elementsList = new ArrayList();  //Lista que contendra los nombres de los elementos
        private ArrayList followList = new ArrayList();  //Lista que servira para los siguientes
        private ArrayList newFollowList = new ArrayList();  //Lista que servira para la tabla de transiciones



        private Stack stk = new Stack();
        int index = 0;
        int cant = 0;
        Node raiz = null;
        ArrayList regularExpression = new ArrayList();


        public static NodeController instancia;

        public static NodeController getInstancia()
        {
            if (instancia == null)
            {
                instancia = new NodeController();
            }
            return instancia;
        }
        /********************/

        public NodeController()
        {
        }
        //Limpa las listas de elementos
        public void clearList()
        {

            raiz = null;
            cant = 0;
            this.followList.Clear();
            //this.elementsList.clear();
            this.listAux.Clear();
            this.list.Clear();
            this.regularExpression.Clear();
        }

        //Iserta los elementos de la expresion regular a una lista;
        public void Insert(String element)
        {
            listAux.Add(element);
        }

        public void Print(String name, String path)
        {
            if (stk.Count > 0)
            {
                Node left = (Node)stk.Pop();
                raiz = left;


                Node temp = raiz;
                //manda la raiz a numerar sus nodos

                Node right = new Node("#");

                Node dot = new Node(".", right, temp, false);

                leafNode(dot);
                setDesition(dot);
                setRootAntNext(dot);

                //Envia la raiz para obtener los siguientes
                //elementsOfTable(dot);

                RegularExpressionController.Instance.InsertArbol(name + "Tree");
                dot.print(path, name + "Tree");

                index++;

            }
        }
        public void InsertStack(String s)
        {

            Node newNode = new Node();
            Node right = new Node();
            Node left = new Node();
            bool anuable = false;

            if (s.Equals("|") || s.Equals("."))
            {
                left = (Node)stk.Pop();

                if (stk.Count != 0)
                {
                    right = (Node)stk.Pop();
                } else
                {
                    right = null;
                }

                if (s.Equals("|"))
                {
                    if (left.Anulable || right.Anulable)
                    {
                        anuable = true;
                    }
                }
                else if (s.Equals("."))
                {
                    if (left.Anulable && right.Anulable)
                    {
                        anuable = true;
                    }
                }
                newNode = new Node(s, right, left, anuable);
                anuable = false;
                stk.Push(newNode);

            }
            else if (s.Equals("*") || s.Equals("+") || s.Equals("?"))
            {
                left = (Node)stk.Pop();

                if (s.Equals("*") || s.Equals("?"))
                {
                    anuable = true;
                }
                else if (s.Equals("+"))
                {
                    if (left.Anulable)
                    {
                        anuable = true;
                    }
                    else
                    {
                        anuable = false;
                    }
                }
                else
                {
                    anuable = true;
                }
                Node n = new Node(s, null, left, anuable);
                anuable = false;
                stk.Push(n);
            }
            else
            {
                Node node = new Node(s);
                stk.Push(node);
            }
        }




        private void leafNode(Node reco)
        {
            if (reco != null)
            {
                if (reco.LeftChild == null && reco.RightChild == null)
                {
                    cant++;

                    //Le ingresa el anuable o no
                    if (!reco.Element.Equals("\"ε\""))
                    {
                        reco.Anulable = false;
                        reco.First = cant.ToString();
                        reco.Last = cant.ToString();
                    }
                    else
                    {
                        reco.Anulable = true;
                        reco.First = "\"Ø\"";
                        reco.Last = "\"Ø\"";
                    }
                    reco.IsLeaf = true;
                    //le da numeracion


                    
                }
                leafNode(reco.LeftChild);
                leafNode(reco.RightChild);
            }
        }

        string nuevoTexo = "";
        private void setDesition(Node n)
        {
            if (n != null)
            {
                if (n.Element.Equals("|"))
                {
                    setOrAntNext(n);
                }
                else if ((n.Element.Equals("*") || n.Element.Equals("?") || n.Element.Equals("+")) && n.IsLeaf == false)
                {
                    setOperatorAntNext(n);
                }
                else if (n.Element.Equals(".") && n.IsLeaf == false)
                {
                    setDotAntNext(n);
                }
            }
        }

        //metodo que sirve para poner los primera pos y ultima pos al Or
        private void setOrAntNext(Node reco)
        {
            if (reco != null)
            {
                reco.First = reco.LeftChild.First + "," + reco.RightChild.First;
                reco.Last = reco.LeftChild.Last + "," + reco.RightChild.Last;
                setDesition(reco.LeftChild);
                setDesition(reco.RightChild);
            }
        }
        //Metodo para poner primiero y siguiente a los operadores * + ?
        private void setOperatorAntNext(Node reco)
        {
            if (reco != null)
            {
                reco.First = reco.LeftChild.First;
                reco.Last = reco.LeftChild.Last;
                setDesition(reco.LeftChild);
                setDesition(reco.RightChild);
            }
        }


        private void setDotAntNext(Node reco)
        {
            if (reco != null)
            {
                //Parte izquierda y derecha
                if (reco.LeftChild.Element.Equals(".")
                        || reco.RightChild.Element.Equals(".")
                        || reco.RightChild.Element.Contains("0")
                        || reco.LeftChild.Element.Contains("0")
                        || reco.First.Contains("0")
                        || reco.Last.Contains("0"))
                {
                    setDesition(reco.LeftChild);
                    setDesition(reco.RightChild);
                }
                //set first
                if (reco.LeftChild.Anulable)
                {
                    reco.First = reco.LeftChild.First + "," + reco.RightChild.First;
                }
                else
                {
                    reco.First = reco.LeftChild.First;
                }

                //set last
                if (reco.RightChild.Anulable)
                {
                    reco.Last = reco.LeftChild.Last + "," + reco.RightChild.Last;
                }
                else
                {
                    reco.Last = reco.RightChild.Last;
                }

                setDesition(reco.LeftChild);
                setDesition(reco.RightChild);
            }
        }
        //Le asigna los primero y ultimo a la raiz
        public void setRootAntNext(Node reco)
        {
            if (reco.LeftChild.Anulable)
            {
                reco.First = reco.LeftChild.First + "," + reco.RightChild.First;
            }
            else
            {
                reco.First = reco.LeftChild.First;
            }
            reco.Last = reco.RightChild.Last;
        }
        //retorna la raiz
        public Node getRoot()
        {
            return raiz;
        }


        public void InOrder(Node n)
        {
            if (n != null)
            {
                InOrder(n.LeftChild);
                Console.WriteLine(n.Element + ", " );
                InOrder(n.RightChild);
            }
        }

        public void PreOrden(Node n)
        {
            if (n != null)
            {
                Console.WriteLine(n.Element);
                PreOrden(n.LeftChild);
                PreOrden(n.RightChild);
            }
        }

        public void PostOrden(Node n)
        {
            if (n != null)
            {
                PostOrden(n.LeftChild);
                PostOrden(n.RightChild);
                Console.WriteLine(n.Element);
            }
        }



        //METODO QUE CONVIERTE LA EXPRESION REGULAR DE PREFIJA A POSTFIJA
        public ArrayList ConvertExpression(Node nroot)
        {

            if (nroot !=null)
            {
                if (nroot.LeftChild == null && nroot.RightChild == null)
                {
                    string b = nroot.Element;
                    string a = "";
                    if (nroot.Element.Equals("\".\"") || 
                        nroot.Element.Equals("\"*\""))
                    {
                        a = nroot.Element;
                    }
                    else if (nroot.Element == "\n")
                    {
                        a = ('\n').ToString();
                    }
                    else if (nroot.Element.Equals("\t"))
                    {
                        a = ('\t').ToString();

                    }
                    else if (nroot.Element.Equals("\r"))
                    {
                        a = ('\r').ToString();
                    }
                    else
                    {
                        a = nroot.Element.Trim('"');
                    }

                    regularExpression.Add(a);
                } 
                else
                {
                    if (nroot.Element.Equals("+"))
                    {
                        regularExpression.Add("(");
                        ConvertExpression(nroot.LeftChild);
                        regularExpression.Add("(");
                        ConvertExpression(nroot.LeftChild);
                        regularExpression.Add(")");
                        regularExpression.Add("*");
                        regularExpression.Add(")");
                    }
                    if (nroot.Element.Equals("*"))
                    {
                        regularExpression.Add("(");
                        ConvertExpression(nroot.LeftChild);
                        regularExpression.Add(")");
                        regularExpression.Add("*");
                    }
                    else if (nroot.Element.Equals("|"))
                    {
                        regularExpression.Add("(");
                        ConvertExpression(nroot.LeftChild);
                        regularExpression.Add("|");
                        ConvertExpression(nroot.RightChild);
                        regularExpression.Add(")");

                    }
                    else if (nroot.Element.Equals("?"))
                    {

                        regularExpression.Add("(");
                        ConvertExpression(nroot.LeftChild);
                        regularExpression.Add(")");
                        regularExpression.Add("?");

                        /*regularExpression.Add("(");
                        ConvertExpression(nroot.LeftChild);
                        regularExpression.Add("|");
                        regularExpression.Add("ε");
                        regularExpression.Add(")");*/
                    }
                    else if (nroot.Element.Equals("."))
                    {
                        ConvertExpression(nroot.LeftChild);
                        ConvertExpression(nroot.RightChild);
                    }
                }
            }
            return regularExpression;
        }
        
    }
}
