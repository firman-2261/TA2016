using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MyParallel;

namespace Experiment.SRCR.Root
{
    public class RootParallelization : parallelNMCTS
    {
        private Board board;
 
        public RootParallelization(int jlhParallel, int wktThinking, int jlhPertandingan,BoardState board)
            :base(jlhParallel, wktThinking, jlhPertandingan,board.sideToMove)
        {
            this.board = new Board(board);
        }

        public Node startNMCTS()
        {
            //OpenConsole();
            //timer.Start();
            //PCQueue jlhParallelThread = new PCQueue(base.jlhParallel);
            //Task<DeterministicNode>[] searchTree = new Task<DeterministicNode>[base.jlhParallel];

            //for (int i = 0; i < base.jlhParallel; i++)
            //{
            //    searchTree[i] = jlhParallelThread.enqueueReturn(move);
            //}

            ////sum winrate nondeterministic node
            //for (int i = 0; i < base.jlhParallel; i++)
            //{
            //    for (int j = 0; j < searchTree[i].Result.children.Length; j++)
            //    {
            //        if (searchTree[i].Result.children[j] is NondeterministicNode)
            //        {
            //            ((NondeterministicNode)searchTree[i].Result.children[j]).sumWinRate();
            //        }
            //    }

            //}

            ////sum total
            //DeterministicNode result = searchTree[0].Result;
            //for (int i = 1; i < base.jlhParallel; i++)
            //{
            //    for (int j = 0; j < searchTree[i].Result.children.Length; j++)
            //    {
            //        result.children[j].nVisits += searchTree[i].Result.children[j].nVisits;
            //        result.children[j].winRate += searchTree[i].Result.children[j].winRate;
            //    }
            //}

            //Node maxWinRate = result.children[0];
            //for (int i = 1; i < result.children.Length; i++)
            //{
            //    if (maxWinRate.winRate <= result.children[i].winRate)
            //    {
            //        maxWinRate = result.children[i];
            //    }
            //}

            //jlhParallelThread.Dispose();

            //return maxWinRate;
            return null;
        }

        public void setPGLValue()
        {
            OpenConsole();
            DeterministicNode tmpNode = new DeterministicNode(this.board.getBoardState(),null,NODE.NONE);
            double x = 0;
            Console.WriteLine("MULAI SET PGL VALUE : 500 Kali");
            Console.WriteLine("==============================");
            for (int i = 0; i < 500; i++)
            {
                if (base.isVerbosePGL)
                {
                    Console.WriteLine(i);
                }
                tmpNode.selectAction();
                x += Node.s;
            }
            Node.PGL = x / 500;
            Console.WriteLine("SELESAI SET PGL VALUE");
            Console.WriteLine("=====================");
        }
        private DeterministicNode move()
        {
            DeterministicNode searchTree = new DeterministicNode(this.board.getBoardState(), null, NODE.NONE);
            while (timer.Elapsed.TotalSeconds <= base.wktThinking)
            {
                if (base.isVerboseRunning)
                {
                    Console.WriteLine("Left Time : " + (wktThinking - timer.Elapsed.TotalSeconds));
                }
                searchTree.selectAction();
            }
            return searchTree;
        }
    }
}
