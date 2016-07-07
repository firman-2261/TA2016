using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MyParallel;

namespace Experiment.SR.TreeVl
{
    public class TreeParallelization : parallelNMCTS
    {
        private Board board;
        private DeterministicNode tree;
 
        public TreeParallelization(int jlhParallel, int wktThinking, int jlhPertandingan,BoardState board)
            :base(jlhParallel, wktThinking, jlhPertandingan,board.sideToMove)
        {
            this.board = new Board(board);
        }

        public Node startNMCTS()
        {
            OpenConsole();
            timer.Start();
            tree = new DeterministicNode(this.board.getBoardState(), null);
            tree.expand();
            tree.updateStatus(tree.rollOut(tree));
            PCQueue jlhParallelThread = new PCQueue(base.jlhParallel);

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
            OpenConsole();
            DeterministicNode tmpNode = new DeterministicNode(this.board.getBoardState(),null);
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
            while (timer.Elapsed.TotalSeconds <= base.wktThinking)
            {
                if (base.isVerboseRunning)
                {
                    Console.WriteLine("Left Time : " + (wktThinking - timer.Elapsed.TotalSeconds));
                }
                tree.selectAction();
            }
            return tree;
        }
    }
}
