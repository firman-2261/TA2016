using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class CESPFMove
    {
        public Move move { private set; get; }
        public int score { private set; get; }

        public CESPFMove(Move move,int score){
            this.move = move;
            this.score = score;
        }

        public bool isFlippingAction()
        {
            if (this.move.from.row == this.move.to.row && this.move.from.column == this.move.to.column)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return ("from : " + this.move.from.row + "," + this.move.from.column + " to : " + this.move.to.row + "," + this.move.to.column + " score : " + score);
        }
    }
}
