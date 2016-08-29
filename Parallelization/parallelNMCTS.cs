using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 
using System.Runtime.InteropServices;
using Engine;

namespace Parallelization
{
    public class parallelNMCTS
    {
        [DllImport("kernel32")]
        protected static extern void AllocConsole();
        [DllImport("kernel32")]
        protected static extern IntPtr GetConsoleWindow();
        [DllImport("user32")]
        protected static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        protected Stopwatch timer;
        protected int jlhParallel;
        protected int wktThinking;
        protected int sideToMove;
        protected bool isVerboseRunning;
        protected bool isShowConsole;
        public parallelNMCTS(int jlhParallel, int wktThinking,int sideToMove,bool isShowConsole, bool isVerboseRunning=true)
        {
            timer = new Stopwatch();
            this.jlhParallel = jlhParallel;
            this.wktThinking = wktThinking;
            this.sideToMove = sideToMove;
            this.isVerboseRunning = isVerboseRunning;
            this.isShowConsole = isShowConsole;
        }


        public static void openConsole()
        {
            IntPtr hw = GetConsoleWindow();
            if (hw != IntPtr.Zero)
            {
                ShowWindow(hw, 5);
            }
            else
            {
                AllocConsole();
            }
        }

        public static void closeConsole()
        {
            IntPtr hw = GetConsoleWindow(); 
            if (hw != IntPtr.Zero)
            {
                ShowWindow(hw, 0);
            }
        }

    }
}
