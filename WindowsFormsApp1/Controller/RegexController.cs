using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Controller
{
    class RegexController
    {
        /** Mapa de precedencia de los operadores. */
        private Dictionary<Char, int> precedenciaOperadores;


        private readonly static RegexController instance = new RegexController();
        public RegexController()
        {
            Dictionary<Char, int> map = new Dictionary<char, int>();
            
            map.Add('(', 1); // parentesis
            map.Add('|', 2); // Union o or
            map.Add('.', 3); // explicit concatenation operator
            map.Add('?', 4); // | €
            map.Add('*', 4); // kleene
            map.Add('+', 4); // positivo

            precedenciaOperadores = map;
        }
        public static RegexController Instance
        {
            get
            {
                return instance;
            }
        }



        /**
	     * Obtener la precedencia del caracter
	     * 
	     * @param c character
	     * @return corresponding precedence
	     */
        private int getPrecedencia(Char c)
        {
           
            int precedencia = precedenciaOperadores[c];
            //si obtiene un valor nulo retrona 6 por default
            return (precedencia == null) ? 6 : precedencia;
        }

        /**
        * Insertar caracter en una posicion deseada
        * @param s string deseado
        * @param pos indice del caracter
        * @param ch caracter  o String deseado
        * @return nuevo string con el caracter deseado
        */
        private String insertCharAt(String s, int pos, Object ch)
        {
            return s.substring(0, pos) + ch + s.substring(pos + 1);
        }

    }
}
