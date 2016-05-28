using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Position
    {
        public int row { private set; get; }
        public int column { private set; get; }
        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public Position() { }
        public override string ToString()
        {
            return row + "," + column;
            //return "ROW : " + row + " , COLUMN : " + column;
        }
    }
}
