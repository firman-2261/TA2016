﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace NMCTS_SR_CR
{
    public class NondeterministicNode:Node
    {
        public List<double> dp;
        public Node selected { set; get; }
        public Node[] children { set; get; }

        public NondeterministicNode(List<double> dp, Move action, NODE type)
        {
            this.dp = dp;
            this.action = action;
            this.type = type;
        }

        public override Node select()
        {
            selected = children[Shuffle.rouletteSelect(this.dp)];
            return this;
        }

        public override void updateStatus(double value)
        {
            this.nVisits += 1;
            this.selected = null;
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
