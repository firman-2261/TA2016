using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Engine;

namespace Parallelization.CR.Root
{
    public class RootParallelization : parallelNMCTS
    {
        private Board board;
        public DeterministicNode result;
 
        public RootParallelization(int jlhParallel, int wktThinking,BoardState board,bool isShowConsole=true)
            :base(jlhParallel, wktThinking,board.sideToMove,isShowConsole)
        {
            this.board = new Board(board);
        }

        public Node startNMCTS()
        {
            taskQueue jlhParallelThread = new taskQueue(base.jlhParallel);
            timer.Start();

            Task<DeterministicNode>[] searchTree = new Task<DeterministicNode>[base.jlhParallel];

            for (int i = 0; i < base.jlhParallel; i++)
            {
                searchTree[i] = jlhParallelThread.enqueueReturn(move);
            }

            //sum winrate nondeterministic node
            for (int i = 0; i < base.jlhParallel; i++)
            {
                for (int j = 0; j < searchTree[i].Result.children.Length; j++)
                {
                    if (searchTree[i].Result.children[j] is NondeterministicNode)
                    {
                        ((NondeterministicNode)searchTree[i].Result.children[j]).sumWinRate();
                    }
                }
            }

            //sum total
            result = searchTree[0].Result;
            for (int i = 1; i < base.jlhParallel; i++)
            {
                result.nVisits += searchTree[i].Result.nVisits;
                result.winRate += searchTree[i].Result.winRate;
                for (int j = 0; j < searchTree[i].Result.children.Length; j++)
                {
                    result.children[j].nVisits += searchTree[i].Result.children[j].nVisits;
                    result.children[j].winRate += searchTree[i].Result.children[j].winRate;
                }
            }

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
            DeterministicNode tmpNode = new DeterministicNode(this.board.getBoardState(), null, Constant.NONE, Constant.NONE);
            DeterministicNode.side = this.sideToMove;
            double x = 0;
            if (base.isShowConsole)
            {
                openConsole();
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
            DeterministicNode searchTree = new DeterministicNode(this.board.getBoardState(), null, Constant.NONE, Constant.NONE);
            searchTree.expand();
            searchTree.updateStatus(searchTree.rollOut(), searchTree.s);
            while (timer.Elapsed.TotalSeconds <= base.wktThinking)
            {
                //if (base.isVerboseRunning)
                //{
                //    Console.WriteLine("Left Time : " + (wktThinking - timer.Elapsed.TotalSeconds));
                //}
                searchTree.selectAction();
            }
            return searchTree;
        }

        public double getTotalSimulasi()
        {
            return result.nVisits;
        }

    }
}
