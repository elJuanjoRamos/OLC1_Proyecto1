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

        public void reportToken()
        {
            XDocument d = new XDocument(new XDeclaration("1.0", "utf-8", null));
            XElement root = new XElement("ListaToken");
            d.Add(root);
            foreach(Token t in arrayListTokens)
            {
                XElement element = new XElement("Token");
                XElement n = new XElement("Nombre");
                XElement v = new XElement("Valor");
                XElement f = new XElement("Fila");
                XElement c = new XElement("Columna");
                element.Add(n);
                n.Add(t.Description);
                element.Add(v);
                v.Add(t.Lexema);
                element.Add(f);
                f.Add(t.Row);
                element.Add(c);
                c.Add(t.Column);
                root.Add(element);
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            d.Save(@"C:\OLC1\Token.xml");
        }

        public void reportError()
        {
            XDocument d = new XDocument(new XDeclaration("1.0", "utf-8", null));
            XElement root = new XElement("ListaErrores");
            d.Add(root);
            foreach (Token t in arrayListErrors)
            {
                XElement element = new XElement("Error");
                XElement n = new XElement("Nombre");
                XElement v = new XElement("Valor");
                XElement f = new XElement("Fila");
                XElement c = new XElement("Columna");
                element.Add(n);
                n.Add(t.Description);
                element.Add(v);
                v.Add(t.Lexema);
                element.Add(f);
                f.Add(t.Row);
                element.Add(c);
                c.Add(t.Column);
                root.Add(element);
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            d.Save(@"C:\OLC1\Error.xml");
        }

    }
}
