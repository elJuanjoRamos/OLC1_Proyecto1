using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Automata;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string charInicial = "";
        string appPath = Application.StartupPath;
        string fileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tp = new TabPage("Pestaña Nueva ");
            tabControl1.Controls.Add(tp);
            var rtb = new RichTextBox();
            rtb.Width = 500;
            rtb.Height = 465;
            System.Drawing.Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle = (FontStyle)(currentFont.Style | FontStyle.Regular);
            rtb.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, 11, newFontStyle);
            tp.Controls.Add(rtb);
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "er files (*.er)|*.er";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                fileName = System.IO.Path.GetFileName(ofd.FileName);
                tabControl1.SelectedTab.Text = filePath;
            }

            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string line;
                foreach (Control c in tabControl1.SelectedTab.Controls)
                {
                    RichTextBox rtb = c as RichTextBox;
                    c.Text = "";
                    try
                    {
                        line = sr.ReadLine();
                        while (line != null)
                        {
                            rtb.AppendText(line + "\n");
                            line = sr.ReadLine();
                        }
                    }
                    catch (Exception)
                    {
                        alertMessage("Ha ocurrido un error D:");
                    }
                    sr.Close();
                }
            }
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(tabControl1.SelectedTab.Text))
            {
                String dir = tabControl1.SelectedTab.Text;
                StreamWriter sw = new StreamWriter(@dir);
                try
                {
                    foreach (Control c in tabControl1.SelectedTab.Controls)
                    {
                        RichTextBox rtb = c as RichTextBox;
                        try
                        {
                            sw.WriteLine(rtb.Text);
                            sw.WriteLine("\n");
                        }
                        catch (Exception)
                        {
                            alertMessage("Ha ocurrido un error D:");
                        }
                    }
                }
                catch (Exception) { }
                sw.Close();
            }
            else
            {
                SaveFileDialog svd = new SaveFileDialog();
                svd.Title = "Save Er Files";
                svd.DefaultExt = "er";
                svd.Filter = "Er files (*.er)|*.er";
                svd.FilterIndex = 2;
                svd.RestoreDirectory = true;
                svd.FileName = tabControl1.SelectedTab.Text;

                if (svd.ShowDialog() == DialogResult.OK)
                {
                    String dir = svd.FileName;
                    StreamWriter sw = new StreamWriter(@dir);
                    tabControl1.SelectedTab.Text = dir;
                    try
                    {
                        foreach (Control c in tabControl1.SelectedTab.Controls)
                        {
                            RichTextBox rtb = c as RichTextBox;
                            try
                            {
                                sw.WriteLine(rtb.Text);
                                sw.WriteLine("\n");
                            }
                            catch (Exception)
                            {
                                alertMessage("Ha ocurrido un error D:");

                            }
                        }
                    }
                    catch
                    {
                        alertMessage("Ha ocurrido un error D:");
                    }
                    sw.Close();
                }
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.Title = "Save Er Files";
            svd.DefaultExt = "er";
            svd.Filter = "Er files (*.er)|*.er";
            svd.FilterIndex = 2;
            svd.RestoreDirectory = true;
            svd.FileName = tabControl1.SelectedTab.Text;

            if (svd.ShowDialog() == DialogResult.OK)
            {
                String dir = svd.FileName;
                StreamWriter sw = new StreamWriter(@dir);
                try
                {
                    foreach (Control c in tabControl1.SelectedTab.Controls)
                    {
                        RichTextBox rtb = c as RichTextBox;
                        try
                        {
                            sw.WriteLine(rtb.Text);
                            sw.WriteLine("\n");
                        }
                        catch (Exception)
                        {
                            alertMessage("Ha ocurrido un error D:");

                        }
                    }
                }
                catch
                {
                    alertMessage("Ha ocurrido un error D:");
                }
                sw.Close();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void alertMessage(String mensaje)
        {
            MessageBox.Show(mensaje, "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (Control c in tabControl1.SelectedTab.Controls)
            {
                RichTextBox rtb = c as RichTextBox;
                if (rtb.Text != "")
                {
                    consola.Clear();
                    SetController.Instance.clearList();
                    NodeController.getInstancia().clearList();
                    EvaluatorController.Instance.clearList();
                    TokenController.Instance.clearListaTokens();
                    TokenController.Instance.clearListaTokensError();
                    LexicoController.Instance.Analizer(rtb.Text);

                    /*foreach(Token t in TokenController.Instance.getArrayListTokens())
                    {
                        Console.WriteLine("ID: " + t.Description + " - " + t.Lexema);
                    }*/

                }
                else
                {
                    alertMessage("No existe texto para analizar D:");
                }
            }

            

            //////// PARTE DE LOS CONJUNTOS
            SetController.Instance.assemble_Sets();
            //SetController.Instance.ShowSets();
            
            /////// Parte de la expresion regular
            RegularExpressionController.Instance.GetElements(Application.StartupPath);
            
            //LA CONSTRUCCION DE LOS AUTOMATAS SE PASO A 
            //RegularExpressionController-> Insertar, al final del metodo;
            
            
            //Evaluar la expresion
            GetString();


        }

        //METODO QUE BUSCA LA CADENA A EVALUAR;
        public void GetString()
        {
            int contador = 0;
            String expressionName = "";
            String strcadena = "";
            string cadena = "";
            string contenido = "";

            ArrayList l = TokenController.Instance.getArrayListTokens();
            for (int i = 0; i < l.Count; i++)
            {
                Token t = (Token)l[i];
                if (t.Lexema.Equals(":"))
                {

                    //busca el nombre de la expresion
                    for (int j = i; j > 0; j--)
                    {
                        Token a = (Token)l[j];
                        if (a.Description.Equals("Identificador"))
                        {
                            expressionName = a.Lexema;
                            break;
                        }
                    }
                    //itera en la expresion y guarda los elementos
                    for (int j = i + 1; j < l.Count; j++)
                    {
                        Token t2 = (Token)l[j];
                        if (!t2.Lexema.Equals(";")) //El limite de la expresion es el punto y coma
                        {
                            if (t2.Description.Equals("Cadena"))
                            {
                                strcadena = t2.Lexema;
                            }
                        }
                        else
                        {
                            if (expressionName != "" && strcadena != "")
                            {
                                
                                if (EvaluatorController.Instance.SimulateExpression(expressionName, strcadena) )
                                {

                                    consola.AppendText("* La Cadena "+ strcadena + " de la Expresion " + expressionName + " fue Evaluada correctamente\n");
                                    contenido = "<tr>\n" +
                                       "     <td>" + "* La Cadena " + strcadena + " de la Expresion " + expressionName + " fue Evaluada correctamente\n" + "</td>\n" +
                                       "</tr>";
                                    cadena = cadena + contenido;
                                    EvaluatorController.Instance.reportToken(appPath, expressionName + "-" + contador);
                                }
                                /*else if (EvaluatorController.Instance.SimulateExpressionWhitString(expressionName, strcadena))
                                {
                                    consola.AppendText("* La Cadena " + strcadena + " de la Expresion " + expressionName + " fue Evaluada correctamente\n");
                                }*/
                                else
                                {
                                    String error = EvaluatorController.Instance.GetError();
                                    consola.AppendText(error);
                                    contenido = "<tr>\n" +
                                       "     <td>" + error + "</td>\n" +
                                       "</tr>";
                                    cadena = cadena + contenido;
                                    EvaluatorController.Instance.reportError(appPath, expressionName + "-" + contador);
                                }

                            }
                            i = j;
                            break;
                        }
                    }
                }
                contador++;

            }

            string cadena2 = "<th scope =\"col\">Evaluación</th>\n";
            assembleHTML(cadena, cadena2, "Consola " + fileName);
        }

        private void reporteDeTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TokenController.Instance.PrintTokens(fileName);
        }

        private void reporteDeErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TokenController.Instance.PrintErrores(fileName);    
        }

        public void assembleHTML(string cadena, string cadena2, string titulo)
        {

            string head = "<!DOCTYPE html>\n" +
            "<html>\n" +
            "<head>\n" +
            "    <meta charset='utf-8'>\n" +
            "    <meta http-equiv='X-UA-Compatible' content='IE=edge'>\n" +
            "    <title> Repote " + titulo + "</title>\n" +
            "    <meta name='viewport' content='width=device-width, initial-scale=1'>\n" +
            "    <link rel='stylesheet' type='text/css' media='screen' href='main.css'>\n" +
            "    <script src='main.js'></script>\n" +
            "    <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css\">\n" +
            "    <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\">\n" +
            "</head>" +
            "<body>\n" +
            "  <nav class=\"navbar navbar-light bg-light\">\n" +
            "    <span class=\"navbar-brand mb-0 h1\">Lenguajes formales</span>\n" +
            "  </nav>";

            string body1 = "<div class=\"container\">\n" +
          "    <div class=\"jumbotron jumbotron-fluid\">\n" +
          "      <div class=\"container\">\n" +
          "        <h1 class=\"display-4\">Consola</h1>\n" +
          "        <p class=\"lead\">Resultado arrojado de consola.</p>\n" +
          "      </div>\n" +
          "    </div>\n" +
          "    <div class=\"row\">\n" +
          "    <table id=\"data\"  cellspacing=\"0\" style=\"width: 100 %\" class=\"table table-striped table-bordered table-sm\">\n" +
          "      <thead class=\"thead-dark\">\n" +
          "        <tr>\n" +
                    cadena2 +
          "        </tr>\n" +
          "      </thead>" +
          "<tbody>";


            string body2 = "</tbody>\n" +
           "    </table>\n" +
           "</div>\n" +
           "  </div>";

            string script =
                "  <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\" ></script>\n" +
                "  <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>\n" +
                "  <script src=\"https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js\"></script>\n" +
                "  <script src=\"https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js\" ></script>\n" +
                "<script>" +
                "$(document).ready(function () { " +
                 "$('#data').DataTable(" +

                 "{ \"aLengthMenu\" " + ":" + " [[5, 10, 25, -1], [5, 10, 25, \"All\"]], \"iDisplayLength\" : 5" +
                 "}" +
                 ");" +
                 "}" +
                 "); " +
               "</script>";

            string html;

            html = head + body1 + cadena + body2 +
            script +
            "</body>" +
            "</html>";

            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.RenderHtmlAsPdf(html).SaveAs("Reporte " + titulo + ".pdf");

            /*creando archivo html*/
            File.WriteAllText("Reporte " + titulo + ".html", html);
            System.Diagnostics.Process.Start("Reporte " + titulo + ".html");
            System.Diagnostics.Process.Start("Reporte " + titulo + ".pdf");
        }
    }
}
