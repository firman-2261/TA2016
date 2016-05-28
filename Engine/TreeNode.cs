using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class TreeNode
    {
        static Random r = new Random();
        static int nActions = 5;
        static double epsilon = 1e-6;
        static double bias = 0.75;

        List<TreeNode>[] children;
        double nVisits, totValue,winRate;
        Board board;

        public TreeNode(BoardState state)
        {
            this.board = new Board(state);
        }

        public void selectAction()
        {
            List<TreeNode> visited = new List<TreeNode>();
            TreeNode cur = this; // root
            visited.Add(this);
            while (!cur.isLeaf())
            {
                cur = cur.select();
                // System.out.println("Adding: " + cur);
                visited.Add(cur);
            }
            cur.expand();
            TreeNode newNode = cur.select();
            visited.Add(newNode);
            double value = rollOut(newNode);
            foreach (TreeNode node in visited)
            {
                // would need extra logic for n-player game
                // System.out.println(node);
                node.updateStats(value);
            }
        }

        public void expand()
        {
            children = new List<TreeNode>[this.board.getCountActions()];
        }

        private TreeNode select()
        {
            TreeNode selected = null;
            double bestValue = double.MinValue;
            /*foreach (TreeNode c in children)
            {
                double uctValue =
                        c.totValue / (c.nVisits + epsilon) +
                                Math.Sqrt(Math.Log(nVisits + 1) / (c.nVisits + epsilon)) +
                                r.NextDouble() * epsilon;
                // small random number to break ties randomly in unexpanded nodes
                // System.out.println("UCT value = " + uctValue);
                if (uctValue > bestValue)
                {
                    selected = c;
                    bestValue = uctValue;
                }
            }
            // System.out.println("Returning: " + selected);*/
            double uctValue =
                    winRate + bias + Math.Sqrt(Math.Log(totValue) / nVisits);
            /*if (uctValue > bestValue)
            {
                selected = c;
                bestValue = uctValue;
            }*/
            return selected;
        }

        public bool isLeaf()
        {
            return children == null;
        }

        public double rollOut(TreeNode tn)
        {
            // ultimately a roll out will end in some value
            // assume for now that it ends in a win or a loss
            // and just return this at random
            return r.Next(2);
        }

        public void updateStats(double value)
        {
            nVisits++;
            totValue += value;
        }

        public int arity()
        {
            return children == null ? 0 : children.Length;
        }

    }
}
