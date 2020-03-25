using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class RowTable
    {
        string symbol;
        int start;
        int end;

        public string Symbol { get => symbol; set => symbol = value; }
        public int Start { get => start; set => start = value; }
        public int End { get => end; set => end = value; }
    }
}
