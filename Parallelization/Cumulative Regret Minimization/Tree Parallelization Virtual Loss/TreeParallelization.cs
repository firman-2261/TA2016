using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Parallelization.CR.TreeVl
{
    public class TreeParallelization : parallelNMCTS
    {
        private Board board;
        public DeterministicNode tree;

        public TreeParallelization(int jlhParallel, int wktThinking, BoardState board, bool isShowConsole = true)
            :base(jlhParallel, wktThinking, board.sideToMove,isShowConsole)
        {
            this.board = new Board(board);
        }

        public Node startNMCTS()
        {
            timer.Start();
            tree = new DeterministicNode(this.board.getBoardState(), null, Constant.NONE, Constant.NONE);
            tree.expand();
            tree.updateStatus(tree.rollOut(),tree.s);
            taskQueue jlhParallelThread = new taskQueue(base.jlhParallel);

            //Task<DeterministicNode> [] searchTree = new Task<DeterministicNode>[base.jlhParallel];

            //for (int i = 0; i < base.jlhParallel; i++)
            //{
            //    searchTree[i] = jlhParallelThread.enqueueReturn(move);
            //}


            Task<DeterministicNode> searchTree = null;

            for (int i = 0; i < base.jlhParallel; i++)
            {
                searchTree = jlhParallelThread.enqueueReturn(move);
            }
            //sum winrate nondeterministic node
            for (int j = 0; j < searchTree.Result.children.Length; j++)
            {
                if (searchTree.Result.children[j] is NondeterministicNode)
                {
                    ((NondeterministicNode)searchTree.Result.children[j]).sumWinRate();
                }
            }

            DeterministicNode result = searchTree.Result;

            Node maxWinRate = result.children[0];
            for (int i = 1; i < result.children.Length; i++)
            {
                if (maxWinRate.winRate <= result.children[i].winRate)
                {
                    maxWinRate = result.children[i];
                }
            }

            jlhParallelThread.Dispose();

            return maxWinRate;
        }

        public void setPGLValue()
        {
            if (base.isShowConsole)
            {
                openConsole();
            }
            DeterministicNode tmpNode = new DeterministicNode(this.board.getBoardState(), null, Constant.NONE, Constant.NONE);
            DeterministicNode.side = this.sideToMove;
            double x = 0;
            if (base.isShowConsole)
            {
                Console.WriteLine("\nSET PGL VALUE : 500 Kali");
            }
            for (int i = 0; i < 500; i++)
            {
                tmpNode.rollOut();
                x += tmpNode.s;
            }
            Node.PGL = x / 500;
            if (base.isShowConsole)
            {
                Console.WriteLine("SELESAI SET PGL VALUE");
                Console.WriteLine("=====================");
            }
        }
        private DeterministicNode move()
        {
            if (base.isShowConsole)
            {
                Console.WriteLine("EXECUTE TASK : " + Task.CurrentId + ", ON THREAD : " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            }
            while (timer.Elapsed.TotalSeconds <= base.wktThinking)
            {
                //if (base.isVerboseRunning)
                //{
                //    Console.WriteLine("Left Time : " + (wktThinking - timer.Elapsed.TotalSeconds));
                //}
                tree.selectAction();
            }
            return tree;
        }
        public double getTotalSimulasi()
        {
            return tree.nVisits;
        }
    }
}
