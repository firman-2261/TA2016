using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class CannonValidMove
    {
        public UInt64 bitboard { private set; get; }
        public List<Position> position { private set; get; }

        public CannonValidMove(UInt64 bitboard, List<Position> position)
        {
            this.bitboard = bitboard;
            this.position = position;
        }
    }
}
