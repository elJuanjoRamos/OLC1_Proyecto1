using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Nodes
{
    class Node
    {
        String element;
        String first;
        String last;
        bool anulable;
        bool isLeaf;
        int index;
        Node rightChild;
        Node leftChild;


        private static int correlative = 1;

        public string Element { get => element; set => element = value; }
        public string First { get => first; set => first = value; }
        public string Last { get => last; set => last = value; }
        public bool Anulable { get => anulable; set => anulable = value; }
        public bool IsLeaf { get => isLeaf; set => isLeaf = value; }
        internal Node RightChild { get => rightChild; set => rightChild = value; }
        internal Node LeftChild { get => leftChild; set => leftChild = value; }

        public Node()
        {
        }
        public Node(String element, Node rightChild, Node leftChild, bool anuable)
        {
            this.Element = element;
            this.index = correlative++;
            this.RightChild = rightChild;
            this.LeftChild = leftChild;
            this.Last = "0";
            this.First = "0";
            this.Anulable = anuable;
            this.IsLeaf = false;

        }
        public Node(String element)
        {
            this.Element = element;
            this.index = correlative++;
            this.RightChild = null;
            this.LeftChild = null;
            this.Anulable = false;
            this.First = "0";
            this.Last = "0";
            this.IsLeaf = false;
        }



        public void print(String path, String nombre)
        {
            try
            {
                System.IO.File.WriteAllText(path + "\\" + nombre + ".dot", getCodeGraphviz());
                var command = "dot -Tpng \"" + path + "\\" + nombre + ".dot\"  -o \"" + path + "\\" + nombre + ".png\"   ";
                var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();


                ////////
                ///
                /// 
                System.IO.File.WriteAllText(path + "\\" + nombre +"1" + ".dot", getCodeGraphviz2());
                var command1 = "dot -Tpng \"" + path + "\\" + nombre + "1" + ".dot\"  -o \"" + path + "\\" + nombre + "1" + ".png\"   ";
                var procStarInfo1 = new ProcessStartInfo("cmd", "/C" + command1);
                var proc1 = new System.Diagnostics.Process();
                proc1.StartInfo = procStarInfo1;
                proc1.Start();
                proc1.WaitForExit();

            }
            catch (Exception)
            {
                MessageBox.Show("Error al escribir el archivo aux_grafico.dot", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private String getCodeGraphviz()
        {
            return "digraph grafica{\n" +
                   "rankdir=TB;\n" +
                   "node [shape = record, style=filled, fillcolor=white];\n" +
                    getBody() +
                    "}\n";
        }
        private String getCodeGraphviz2()
        {
            return "digraph grafica{\n" +
                   "rankdir=LR;\n" +
                   "node [shape = circle, style=filled, fillcolor=white];\n" +
                    getBody2() +
                    "}\n";
        }
        private String getBody()
        {
            String etiqueta;
            String str = element.Replace('"', ' ');
            String anuable = "F";

            if (str.Equals("|"))
            {
                str = "or";
            }
            if (Anulable)
            {
                anuable = "V";
            }

            /*nuevo metodo*/
            if (leftChild == null && rightChild == null)
            {
                etiqueta = "nodo" + index + " [ label =\"" + First + "|" + str + "\\l" + anuable + "|" + Last + "\"];\n";
            }
            else
            {
                etiqueta = "nodo" + index + " [ label =\"" + First + "|" + str + "\\l" + anuable + "|" + Last + "\"];\n";
            }
            if (leftChild != null)
            {
                etiqueta = etiqueta + leftChild.getBody() +
                   "nodo" + index + "->nodo" + leftChild.index + "\n";
            }
            if (rightChild != null)
            {
                etiqueta = etiqueta + rightChild.getBody() +
                   "nodo" + index + "->nodo" + rightChild.index + "\n";
            }
            return etiqueta;
        }

        int indice = 100;


        private void updateIndex()
        {
            this.index = indice;
        }
        private String getBody2()
        {
            String etiqueta = "";
            String str = element.Replace('"', ' ');



            if (leftChild == null && rightChild == null)
            {
                etiqueta = " [ label =\"" + str+ "\"]\n" ;
            }
            
            if (LeftChild != null)
            {
                if (Element.Equals("|"))
                {
                    etiqueta = etiqueta + index + "->" + rightChild.index + "[label=\"" + "ε" + "\"]" + "\n" +
                                  rightChild.index + "->" + (indice +1) + rightChild.getBody2() + "\n" +
                                  (indice +1)+ "->" + (indice) + "[label=\"" + "ε" + "\"]" + "\n";
                    this.index = indice;
                }
                else if (Element.Equals("."))
                {
                    leftChild.updateIndex();
                    etiqueta = etiqueta + leftChild.indice + "->" + index + leftChild.getBody2() + "\n";
                }
                #region otro2

                else if (Element.Equals("*"))
                {
                    int indiceTemp = (indice + 1);
                    int indiceTemp2 = (indiceTemp + 1);
                    etiqueta = etiqueta + index + "->" + leftChild.index + "[label=\"" + "ε" + "\"]" + "\n" +
                               leftChild.index + "->" + indiceTemp + leftChild.getBody2() + "\n" +
                               (indiceTemp) + "->" + (indiceTemp2) + "[label=\"" + "ε" + "\"]" + "\n" +
                               (indiceTemp) + "->" + leftChild.index + "[label=\"" + "ε" + "\"]" + "\n" +
                               index + "->" + (indiceTemp2) + "[label=\"" + "ε" + "\"]";
                    //indice = indiceTemp2;
                }
                else
                {
                    etiqueta = " [ label =\"" + str + "\"]\n";
                }
#endregion
            }
            if (RightChild != null)
            {

                if (Element.Equals("|"))
                {

                    etiqueta = etiqueta + index + "->" + leftChild.index + "[label=\"" + "ε" + "\"]" + "\n" +
                          leftChild.index + "->" + (indice + 2) + leftChild.getBody2() + "\n" +
                          (indice + 2) + "->" + indice + "[label=\"" + "ε" + "\"]" + "\n";
                    this.index = indice;


                }
                else if (Element.Equals("."))
                {
                    etiqueta = etiqueta + index + "->" + rightChild.index + rightChild.getBody2() + "\n";
                }


                #region otro
                /*else if (Element.Equals("*"))
                {
                    int indiceTemp = indice + 1;
                    int indiceTemp2 = (indiceTemp + 1);
                    etiqueta = etiqueta + index + "->" + leftChild.index + "[label=\"" + "ε" + "\"]" + "\n" +
                               leftChild.index + "->" + indiceTemp + leftChild.getBody2() + "\n" +
                               (indiceTemp) + "->" + (indiceTemp2) + "[label=\"" + "ε" + "\"]" + "\n" +
                               (indiceTemp) + "->" + leftChild.index + "[label=\"" + "ε" + "\"]" + "\n" +
                               index + "->" + (indiceTemp2) + "[label=\"" + "ε" + "\"]";
                    //indice = indiceTemp2;
                }*/

                else
                {
                    etiqueta = " [ label =\"" + str + "\"]\n";
                }
                #endregion

            }

            return etiqueta;
        }




    }
}
