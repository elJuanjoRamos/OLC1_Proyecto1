using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Controller
{
    class LexicoController
    {
        private readonly static LexicoController instance = new LexicoController();
        String auxiliar = "";

        private LexicoController()
        {
        }

        public static LexicoController Instance
        {
            get
            {
                return instance;
            }
        }

        public void Analizer(String textInput)
        {
            int state = 0;
            int column = 0;
            int row = 1; ;

            for (int i = 0; i < textInput.Length; i++)
            {
                char letra = textInput[i];
                column++;

                //System.err.println(letra);
                switch (state)
                {
                    case 0:
                        //SI VIENE LETRA
                        //System.out.println("ESTADO 0");
                        if (char.IsLetter(letra))
                        {
                            state = 1;
                            auxiliar += letra;
                            //SI VIENE SALTO DE LINEA
                        }
                        else if (letra == '\n')
                        {
                            state = 0;
                            column = 0;//COLUMNA 0
                            row++; //FILA INCREMENTA

                        }
                        //VERIFICA ESPACIOS EN BLANCO
                        else if (char.IsWhiteSpace(letra))
                        {
                            //column++;
                            state = 0;
                            //VERIFICA SI VIENE NUMERO
                        }
                        else if (char.IsDigit(letra))
                        {
                            state = 2;
                            auxiliar += letra;
                        }

                        //VERIFICA SI ES PUNTUACION
                        else if(char.IsPunctuation(letra))
                        {
                            switch (letra)
                            {
                                case '.':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Punto");
                                    break;
                                case ',':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Coma");
                                    break;
                                case ':':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_DosPuntos");
                                    break;
                                case ';':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_PuntoComa");
                                    break;
                                case '{':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_LlaveIzquierda");
                                    break;
                                case '}':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_LlaveDerecha");
                                    break;
                                case '?':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '%':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Porcentaje");
                                    break;
                                case '*':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Multiplicacion");
                                    break;
                                case '/':
                                    state = 3;
                                    auxiliar += letra;
                                    break;
                                case '"':
                                    state = 8;
                                    auxiliar += letra;
                                    break;
                                case '-':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Resta");
                                    break;
                                default:
                                    TokenController.Instance.agregarError(row, column, letra.ToString(), "TD_Desconocido");
                                    break;
                            }
                        }

                        //VERIFICA SI ES SIMBOLO
                        else if (char.IsSymbol(letra)) // ANTES ESTABA isDefined
                        {
                            switch (letra)
                            {
                                case '>':
                                    //System.out.println("ENTRA A >");
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Mayor");
                                    break;
                                case '~':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Virgulilla");
                                    break;
                                case '+':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Suma");
                                    break;
                                
                                case '|':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Pleca");
                                    break;
                                
                                
                                case '<':
                                    state = 5;
                                    auxiliar += letra;
                                    Console.WriteLine("ESTADO 5");
                                    break;

                                /*SIMBOLOS ASCII*/
                                case '!':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '#':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '$':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '&':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '(':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case ')':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '=':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                
                                case '@':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '[':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case ']':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '^':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                case '_':
                                    TokenController.Instance.agregarToken(row, column - 1, letra.ToString(), "TK_Simbolo");
                                    break;
                                default:
                                    TokenController.Instance.agregarError(row, column, letra.ToString(), "TD_Desconocido");
                                    break;
                            }
                        }
                        else
                        {
                            TokenController.Instance.agregarError(row, column, letra.ToString(), "TD_Desconocido");
                        }
                        break;
                    case 1:
                        if (char.IsLetterOrDigit(letra) || letra == '_')
                        {
                            auxiliar += letra;
                            state = 1;
                        }
                        else
                        {
                            if (auxiliar.Equals("CONJ"))
                            {
                                TokenController.Instance.agregarToken(row, (column - auxiliar.Length - 1), auxiliar, "PR_" + auxiliar);
                            }
                            else
                            {
                                TokenController.Instance.agregarToken(row, (column - auxiliar.Length - 1), auxiliar, "Identificador");
                            }
                            auxiliar = "";
                            i--;
                            column--;
                            state = 0;
                        }
                        break;
                    case 2:
                        if (char.IsDigit(letra))
                        {
                            auxiliar += letra;
                            state = 2;
                        }
                        else
                        {
                            TokenController.Instance.agregarToken(row, column, auxiliar, "Digito");
                            auxiliar = "";
                            i--;
                            column--;
                            state = 0;
                        }
                        break;
                    case 3:
                        if (letra == '/')
                        {
                            state = 4;
                            auxiliar += letra;
                        }
                        break;
                    case 4:
                        if (letra != '\n')
                        {
                            auxiliar += letra;
                            state = 4;
                        }
                        else
                        {
                            TokenController.Instance.agregarToken(row, 0, auxiliar, "ComentarioLinea");
                            row++; column = 0;
                            state = 0;
                            auxiliar = "";
                        }
                        break;
                    case 5:

                        if (letra == '!')
                        {
                            state = 6;
                            auxiliar += letra;
                        }
                        break;
                    case 6:
                        if (letra != '!')
                        {
                            if (letra == '\n') { row++; column = 0; }
                            auxiliar += letra;
                            state = 6;
                            /*System.err.println("ESTADO 5");
                            System.err.println(auxiliar);*/
                        }
                        else
                        {
                            auxiliar += letra;
                            state = 7;
                            /*System.err.println("ESTADO 7");
                            System.err.println(auxiliar);*/
                        }
                        break;
                    case 7:
                        if (letra == '>')
                        {
                            auxiliar += letra;
                            //                        System.err.println("ESTADO 7");
                            //                        System.err.println(auxiliar);
                            TokenController.Instance.agregarToken(row, column, auxiliar, "ComentarioMultilinea");
                            //row++; column = 0;
                            state = 0;
                            auxiliar = "";
                        }
                        break;
                    case 8:
                        if (letra != '"')
                        {
                            if (letra == '\n') { row++; column = 0; }
                            auxiliar += letra;
                            state = 8;
                        }
                        else
                        {
                            state = 9;
                            auxiliar += letra;
                            i--; column--;
                        }
                        break;
                    case 9:
                        if (letra == '"')
                        {
                            TokenController.Instance.agregarToken(row, (column - auxiliar.Length), auxiliar, "Cadena");
                            state = 0;
                            auxiliar = "";
                        }
                        break;
                    default:
                        TokenController.Instance.agregarError(row, column, letra.ToString(), "TD_Desconocido");
                        break;
                }
            }
        }

    }
}
