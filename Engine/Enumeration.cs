using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public enum END_STATE
    {
        DRAW,RED_WIN=-1,BLACK_WIN=1,CONTINUE
    }

    public enum ACTION
    {
        FLIP,MOVE
    }

    public enum PLAYER
    {
        HUMAN,COMPUTER
    }

    public enum TOURNAMENT
    {
        HUMAN_VS_HUMAN,HUMAN_VS_COMPUTER
    }


}
