﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Experiment.SR.TreeVl
{
    public class Node
    {
        public static readonly double bias = 0.75;
        public static readonly double d = 0.01;
        public static readonly object _lock = new object();
        public const int Available = 0;
        public const int Taken = 1;
        public static int side;
        public static double PGL;

        public static double s;
        public double nVisits { set; get; }
        public double winRate { set; get; }
        public Move action { set; get; }
        public virtual bool isLeaf() { return true; }
        public virtual Node select() { return null; }

        public virtual void updateStatus(double value) { }

        public virtual void expand() { }

        public virtual double rollOut(Node tn) { return 0; }

    }
}
