using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Controller
{
    class SetController
    {
        private readonly static SetController instance = new SetController();
        private ArrayList arrayListSets = new ArrayList();

        public SetController()
        {

        }
        public static SetController Instance
        {
            get
            {
                return instance;
            }
        }

        public void Insert(String name, ArrayList ar, bool isInterval)
        {
            ArrayList newElements = new ArrayList();
            Set s = null;

            if (isInterval)
            {
                for (int i = 0; i < ar.Count; i++)
                {
                    if (ar[i].Equals("~"))
                    {
                        
                        newElements = getNewElements(((String)ar[i - 1])[0], ((String)ar[i + 1])[0]);
                        break;
                    }
                }
                s = new Set(name, newElements);
            }
            else
            {
                s = new Set(name, ar);
            }
            arrayListSets.Add(s);
        }


        public void clearList()
        {
            arrayListSets.Clear();
        }
        public ArrayList GetArray()
        {
            return arrayListSets;
        }

        public void assemble_Sets()
        {
            bool isInterval = false;
            ArrayList arrayListTokens = TokenController.Instance.getArrayListTokens();
            for (int i = 0; i < arrayListTokens.Count; i++)
            {
                Token tok = (Token)arrayListTokens[i];
                if (tok.Lexema.ToLower().Equals("conj"))
                {
                    String name = ((Token)arrayListTokens[i + 2]).Lexema;
                   
                    int pos = i + 5;
                    ArrayList elements = new ArrayList();
                    for (int j = pos; j < arrayListTokens.Count; j++)
                    {
                        Token t = (Token)arrayListTokens[j];
                        if (!t.Lexema.Equals(";"))
                        {
                            if (!t.Lexema.Equals(","))
                            {
                                if (t.Lexema.Equals("~"))
                                {
                                    isInterval = true;
                                }
                                elements.Add(t.Lexema);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    Insert(name, elements, isInterval);
                }
            }
        }
        public ArrayList getNewElements(char ant, char sig)
        {
            ArrayList ar = new ArrayList();

            //DIGITS
            if (char.IsDigit(ant))
            {


                for (int i = int.Parse(ant.ToString()); i < int.Parse(sig.ToString()) + 1; i++)
                {
                    ar.Add(i.ToString());
                }
                return ar;
                //LETTERS
            }
            else if (char.IsLetter(ant))
            {
                int initValue = (int)ant;
                int endValue = (int)sig;

                for (int i = initValue; i <= endValue; i++)
                {
                    ar.Add(((char)i).ToString());
                }
                return ar;
                //ASCII CODES 32 TO 125
            }
            else if ((int)ant >= 32 && (int)sig <= 125)
            {
                for (int i = (int)ant; i <= (int)sig; i++)
                {

                    if (!char.IsDigit(ant) && !char.IsDigit(sig) && !char.IsLetter(ant) && !char.IsLetter(sig))
                    {
                        ar.Add(((char)i).ToString());
                    }
                }
                return ar;
            }
            return null;
        }
        //Retorna los elementos que tiene un conjunto
        public ArrayList GetElemntsOfSet(String setName)
        {
            foreach (Set set in arrayListSets)
            {

                if (set.Name.Equals(setName))
                {
                    return set.Elements;
                }
            }
            return null;
        }


        public void ShowSets()
        {
            foreach (Set item in arrayListSets)
            {
                Console.WriteLine(item.Name);
                foreach (String e in item.Elements)
                {
                    Console.WriteLine("-"+e);
                }
            }
        }
    }
}
