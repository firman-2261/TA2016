using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Move
    {
        public Position from { private set; get; }
        public Position to { private set; get; }
        public Move(Position from,Position to){
            this.from = from;
            this.to = to;
        }

        public override string ToString()
        {
            return ("from : " + from.row + "," + from.column + " to : " + to.row + "," + to.column );
        }
    }
}
