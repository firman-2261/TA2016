using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Piece
    {
        public static Piece none = new Piece(Constant.NONE);
        public static Piece blackKing = new Piece(Constant.BLACK_KING);
        public static Piece blackGuard = new Piece(Constant.BLACK_GUARD);
        public static Piece blackMinister = new Piece(Constant.BLACK_MINISTER);
        public static Piece blackKnight = new Piece(Constant.BLACK_KNIGHT);
        public static Piece blackRook = new Piece(Constant.BLACK_ROOK);
        public static Piece blackCannon = new Piece(Constant.BLACK_CANNON);
        public static Piece blackPawn = new Piece(Constant.BLACK_PAWN);
        public static Piece redKing = new Piece(Constant.RED_KING);
        public static Piece redGuard = new Piece(Constant.RED_GUARD);
        public static Piece redMinister = new Piece(Constant.RED_MINISTER);
        public static Piece redKnight = new Piece(Constant.RED_KNIGHT);
        public static Piece redRook = new Piece(Constant.RED_ROOK);
        public static Piece redCannon = new Piece(Constant.RED_CANNON);
        public static Piece redPawn = new Piece(Constant.RED_PAWN);


        public readonly int index;
        public readonly int number;

        public Piece(int number)
        {
            this.number = number;
            this.index = number + 7;
        }

        public Piece clone()
        {
            return new Piece(this.number);
        }

        public static bool operator ==(Piece a, Piece b)
        {
            return a.number == b.number;
        }
        public static bool operator !=(Piece a, Piece b)
        {
            return a.number != b.number;
        }

        public override string ToString()
        {
            return ("index : "+this.index + ", number : " + this.number);
        }

    }
}
