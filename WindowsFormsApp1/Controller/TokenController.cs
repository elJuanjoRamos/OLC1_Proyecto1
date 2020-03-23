using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    class TokenController
    {
        private readonly static TokenController instance = new TokenController();
        private ArrayList arrayListTokens = new ArrayList();
        private ArrayList arrayListErrors = new ArrayList();
        private int idToken = 1;
        private int idTokenError = 1;
        private TokenController()
        {
        }

        public static TokenController Instance
        {
            get
            {
                return instance;
            }
        }

        public void agregarToken(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idToken, lexema, descripcion, columna, fila);
            arrayListTokens.Add(token);
            idToken++;
        }

        public void agregarError(int fila, int columna, string lexema, string descripcion)
        {
            Token token = new Token(idTokenError, lexema, descripcion, columna, fila);
            arrayListErrors.Add(token);
            idTokenError++;
        }

        public ArrayList getArrayListTokens()
        {
            return arrayListTokens;
        }

        public ArrayList getArrayListErrors()
        {
            return arrayListErrors;
        }
        public void clearListaTokens()
        {
            arrayListTokens.Clear();
        }

        public void clearListaTokensError()
        {
            arrayListErrors.Clear();
        }

       
        public void PrintTokens(string name)
        {
            string cadena = "";
            string contenido = "";

            for (int i = 0; i < arrayListTokens.Count; i++)
            {
                Token tok = (Token)arrayListTokens[i];

                contenido = "<tr>\n" +
                    "     <th scope=\"row\">" + (i).ToString() + "</th>\n" +
                    "     <td>" + tok.Lexema + "</td>\n" +
                    "     <td>" + tok.Description + "</td>\n" +
                    "     <td>" + tok.Row + "</td>\n" +
                    "     <td>" + tok.Column + "</td>\n" +
                    "</tr>";
                cadena = cadena + contenido;

            }
            string cadena2 = "<th scope =\"col\">No</th>\n" +
            "          <th scope=\"col\">Lexema</th>\n" +
            "          <th scope=\"col\">Token</th>\n" +
            "          <th scope=\"col\">Fila</th>\n" +
            "          <th scope=\"col\">Columna</th>\n";
            assembleHTML(cadena, cadena2, "Tokens " + name);

        }

        public void ImprimirVoid(string name)
        {
            string cadena = "";

            string cadena2 = "<th scope =\"col\">No</th>\n" +
             "          <th scope=\"col\">Lexema</th>\n" +
             "          <th scope=\"col\">Token</th>\n" +
             "          <th scope=\"col\">Fila</th>\n" +
             "          <th scope=\"col\">Columna</th>\n";
            assembleHTML(cadena, cadena2, "Tokens " + name);

        }

        public void PrintErrores(string name)
        {
            string cadena = "";
            string contenido = "";

            for (int i = 0; i < arrayListErrors.Count; i++)
            {
                Token tok = (Token)arrayListErrors[i];

                contenido = "<tr>\n" +
                    "     <th scope=\"row\">" + (i).ToString() + "</th>\n" +
                    "     <td>" + tok.Lexema + "</td>\n" +
                    "     <td>" + tok.Description + "</td>\n" +
                    "     <td>" + tok.Row + "</td>\n" +
                    "     <td>" + tok.Column + "</td>\n" +
                    "</tr>";
                cadena = cadena + contenido;

            }
            string cadena2 = "<th scope =\"col\">No</th>\n" +
            "          <th scope=\"col\">Lexema</th>\n" +
            "          <th scope=\"col\">Token</th>\n" +
            "          <th scope=\"col\">Fila</th>\n" +
            "          <th scope=\"col\">Columna</th>\n";
            assembleHTML(cadena, cadena2, "Errores " + name);

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
          "        <h1 class=\"display-4\">" + titulo + "</h1>\n" +
          "        <p class=\"lead\">Listado de " + titulo + " detectados por el analizador</p>\n" +
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


            /*creando archivo html*/
            File.WriteAllText("Reporte " + titulo + ".html", html);
            System.Diagnostics.Process.Start("Reporte " + titulo + ".html");

        }


    }
}
