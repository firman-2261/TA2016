using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Constant
    {
        public const int NONE = 0;
        public const int BLACK_KING = 1;
        public const int BLACK_GUARD = 2;
        public const int BLACK_MINISTER = 3;
        public const int BLACK_ROOK = 4;
        public const int BLACK_KNIGHT = 5;
        public const int BLACK_CANNON = 6;
        public const int BLACK_PAWN = 7;
        public const int RED_KING = -1;
        public const int RED_GUARD = -2;
        public const int RED_MINISTER = -3;
        public const int RED_ROOK = -4;
        public const int RED_KNIGHT = -5;
        public const int RED_CANNON = -6;
        public const int RED_PAWN = -7;

        public const int ROW = 4;
        public const int COLUMN = 8;

        public const short RED_SIDE = -1;
        public const short BLACK_SIDE = 1;

        public const int KING_SCORE = 5500;
        public const int GUARD_SCORE = 5000;
        public const int MINISTER_SCORE = 2500;
        public const int KNIGHT_SCORE = 1000;
        public const int ROOK_SCORE = 800;
        public const int CANNON_SCORE = 3000;
        public const int PAWN_SCORE = 800;

        public static Dictionary<byte, Position> indexMapping = new Dictionary<byte, Position>()
        {
           {0, new Position(0,0)}, {1, new Position(0,1)}, {2, new Position(0,2)}, {3, new Position(0,3)}, {4, new Position(0,4)}, {5, new Position(0,5)}, {6, new Position(0,6)}, {7, new Position(0,7)},
           {8, new Position(1,0)}, {9, new Position(1,1)}, {10, new Position(1,2)}, {11, new Position(1,3)}, {12, new Position(1,4)}, {13, new Position(1,5)}, {14, new Position(1,6)}, {15, new Position(1,7)},
           {16, new Position(2,0)}, {17, new Position(2,1)}, {18, new Position(2,2)}, {19, new Position(2,3)}, {20, new Position(2,4)}, {21, new Position(2,5)}, {22, new Position(2,6)}, {23, new Position(2,7)},
           {24, new Position(3,0)}, {25, new Position(3,1)}, {26, new Position(3,2)}, {27, new Position(3,3)}, {28, new Position(3,4)}, {29, new Position(3,5)}, {30, new Position(3,6)}, {31, new Position(3,7)},
        };
    }
}
