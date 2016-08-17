using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Parallelization.CR.Tree
{
    public class NondeterministicNode:Node
    {
        public List<double> dp;
        public Node selected { set; get; }
        public Node[] children { set; get; }

        public NondeterministicNode(List<double> dp,Move action)
        {
            this.dp = dp;
            this.action = action;
            this.name = Constant.NONDETERMINISTIC_NODE;
        }

        public override Node select()
        {
            selected = children[Shuffle.rouletteSelect(this.dp)];
            return this;
        }

        public override void updateStatus(double value,int s)
        {
            this.nVisits += 1;
        }

        public void sumWinRate()
        {
            double total = 0;
            for (int i = 0; i < this.children.Length; i++)
            {
                total += this.children[i].winRate;
            }
            this.winRate = total;
        }

    }
}
