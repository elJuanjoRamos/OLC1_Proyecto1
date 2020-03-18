using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Nodes;

namespace WindowsFormsApp1.Controller
{
    class NodeController
    {

        private ArrayList listAux = new ArrayList();//Lista que contendra los elementos de la expresion regular
        private ArrayList list = new ArrayList();  //Lista que servira para reordenar los elementos de la expresion regular
        private ArrayList elementsList = new ArrayList();  //Lista que contendra los nombres de los elementos


        private Stack stk = new Stack();
        int index = 0;
        int cant = 0;
        Node raiz = null;
        String regularExpression = "";


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

        //Iserta los elementos de la expresion regular a una lista;
        public void Insert(String element)
        {
            listAux.Add(element);
        }


        public void clearList()
        {
            cant = 0;
            raiz = null;
            //this.elementsList.clear();
            this.listAux.Clear();
            this.list.Clear();
        }

        public void GetStringTree()
        {
            Node newRoot = (Node)stk.Pop();





            stk.Push(newRoot);
        }


        public void Print(String name, String path)
        {
            Node left = (Node)stk.Pop();
            raiz = left;
            raiz.print(path, name + "Tree.jpg");
            index++;
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
                    if (!reco.Element.Equals("epsilon"))
                    {
                        reco.Anulable = false;
                    }
                    else
                    {
                        reco.Anulable = true;
                    }
                    reco.IsLeaf = true;
                    //le da numeracion
                    reco.First = cant.ToString();
                    reco.Last = cant.ToString();
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
        public void ConvertExpression(Node nroot)
        {
            if (nroot !=null)
            {
                if (nroot.LeftChild == null && nroot.RightChild == null)
                {
                    regularExpression = regularExpression + nroot.Element;
                } else
                {
                    if (nroot.Element.Equals("*"))
                    {
                        regularExpression = regularExpression + "(";
                        ConvertExpression(nroot.LeftChild);
                        regularExpression = regularExpression + ")*";
                    }
                    else if (nroot.Element.Equals("|"))
                    {
                        ConvertExpression(nroot.LeftChild);
                        regularExpression = regularExpression + "|";
                        ConvertExpression(nroot.RightChild);
                    }
                    else if (nroot.Element.Equals("."))
                    {
                        ConvertExpression(nroot.LeftChild);
                        ConvertExpression(nroot.RightChild);
                    }
                }
            }
        }
        
        public String getRegularExpression()
        {
            return regularExpression;
        }
    }
}
