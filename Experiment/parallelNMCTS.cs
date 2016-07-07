using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 
using System.Runtime.InteropServices;
using Engine;

namespace Experiment
{
    public class parallelNMCTS
    {
        [DllImport("Kernel32")]
        protected static extern void AllocConsole();

        [DllImport("Kernel32")]
        protected static extern void FreeConsole();

        [DllImport("Kernel32")]
        protected static extern bool AttachConsole(uint dwProcessId);

        const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;

        protected Stopwatch timer;
        protected int jlhParallel;
        protected int wktThinking;
        protected int jlhPertandingan;
        protected int sideToMove;
        protected bool isVerbosePGL;
        protected bool isVerboseRunning;
        protected void OpenConsole()
        {
            if (!AttachConsole(ATTACH_PARENT_PROCESS))
            {
                AllocConsole();
            };
        }

        public parallelNMCTS(int jlhParallel, int wktThinking, int jlhPertandingan,int sideToMove,bool isVerbosePGL = true, bool isVerboseRunning=true)
        {
            timer = new Stopwatch();
            this.jlhParallel = jlhParallel;
            this.jlhPertandingan = jlhPertandingan;
            this.wktThinking = wktThinking;
            this.sideToMove = sideToMove;
            this.isVerbosePGL = isVerbosePGL;
            this.isVerboseRunning = isVerboseRunning;
        }

    }
}
