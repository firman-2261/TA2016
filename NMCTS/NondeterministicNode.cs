﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace NMCTS
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

        public override void updateStatus(double value)
        {
            this.nVisits += 1;
            double totalWinRate = 0;
            foreach (Node x in this.children)
            {
                totalWinRate += x.winRate;
            }
            this.winRate = totalWinRate;
            this.selected = null;
        }

    }
}
