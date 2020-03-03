using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    class Token
    {
        private int id;
        private String lexema;
        private String description;
        private int column;
        private int row;

        public int Id { get => id; set => id = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Description { get => description; set => description = value; }
        public int Column { get => column; set => column = value; }
        public int Row { get => row; set => row = value; }

        public Token(int id, string lexema, string description, int column, int row)
        {
            this.Id = id;
            this.Lexema = lexema;
            this.Description = description;
            this.Column = column;
            this.Row = row;
        }

        public Token()
        {
        }

    }
}
