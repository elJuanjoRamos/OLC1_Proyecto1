using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Model
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

                // Determine whether the directory exists.
                if (!Directory.Exists(path + "\\" + "Arboles"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path + "\\" + "Arboles");
                }

                String pngPath = path + "\\" + "Arboles";



                System.IO.File.WriteAllText(path + "\\" +  "Tree.dot", getCodeGraphviz());
                var command = "dot -Tpng \"" + path + "\\Tree.dot\"  -o \"" + pngPath + "\\" + nombre + ".png\"   ";
                var procStarInfo = new ProcessStartInfo("cmd", "/C" + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStarInfo;
                proc.Start();
                proc.WaitForExit();

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
        private String getBody()
        {
            String etiqueta;

            String str = element.Replace('"', ' ');
            if (element.Equals("ε"))
            {
                str = "epsilon";
            
            }
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

        
    }
}
