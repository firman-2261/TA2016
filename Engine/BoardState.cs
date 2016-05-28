using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class BoardState
    {
        public Piece[,] array { private set; get; }
        public UInt64[] bitboard { private set; get; }
        public short sideToMove { private set; get; }
        public byte ply { private set; get; } //digunakan untuk mendeteksi 40 ply
        public byte restOfRedPieces { private set; get; }
        public byte restOfBlackPieces { private set; get; }
        public byte flippedRedPieces { private set; get; }
        public byte flippedBlackPieces { private set; get; }
        public UInt64[] repeatList { private set; get; }
        public byte repeatIndex { private set; get; }
        public byte blackTurn { private set; get; }
        public byte redTurn { private set; get; }

        public BoardState(Board board)
        {
            this.array = Board.getArrayByValue(board.array);
            this.bitboard = board.bitboard.Clone() as UInt64[];
            this.sideToMove = board.sideToMove;
            this.ply = board.ply;
            this.restOfRedPieces = board.restOfRedPieces;
            this.restOfBlackPieces = board.restOfBlackPieces;
            this.flippedRedPieces = board.flippedRedPieces;
            this.flippedBlackPieces = board.flippedBlackPieces;
            this.repeatList = board.repeatList.Clone() as UInt64[];
            this.repeatIndex = board.repeatIndex;
            this.blackTurn = board.blackTurn;
            this.redTurn = board.blackTurn;
        }

    }
}
