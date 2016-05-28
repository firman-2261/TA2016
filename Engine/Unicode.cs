using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Unicode
    {
        public static string getUnicodePiece(int piece)
        {
            switch (piece)
            {
                case Constant.BLACK_PAWN:
                    return "卒¹";
                case Constant.RED_PAWN:
                    return "兵¹";
                case Constant.BLACK_CANNON:
                    return "砲²";
                case Constant.RED_CANNON:
                    return "炮²";
                case Constant.BLACK_KNIGHT:
                    return "馬³";
                case Constant.RED_KNIGHT:
                    return "傌³";
                case Constant.BLACK_ROOK:
                    return "車⁴";
                case Constant.RED_ROOK:
                    return "俥⁴";
                case Constant.BLACK_MINISTER:
                    return  "象⁵";
                case Constant.RED_MINISTER:
                    return "相⁵";
                case Constant.BLACK_GUARD:
                    return "士⁶";
                case Constant.RED_GUARD:
                         return "仕⁶";
                case Constant.BLACK_KING:
                    return "將⁷";
                case Constant.RED_KING:
                    return "帥⁷";
                default:
                    return "?";
            }
        }
    }
}
