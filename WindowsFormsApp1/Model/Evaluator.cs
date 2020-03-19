using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class Evaluator
    {
        string expressionName;
        Automata.Automata afd;

        public Evaluator(string n, Automata.Automata a )
        {
            this.expressionName = n;
            this.afd = a;
        }

        public string ExpressionName { get => expressionName; set => expressionName = value; }
        internal Automata.Automata Afd { get => afd; set => afd = value; }
    }
}
