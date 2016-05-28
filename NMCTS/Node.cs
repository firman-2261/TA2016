using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace NMCTS
{
    public class Node
    {
        public static Random r = new Random();
        public static double bias = 0.75;
        public static int side;
        public static double epsilon = 1e-6;

        public double nVisits, winRate;
        public Move action;
        public virtual bool isLeaf() { return true; }
        public virtual Node select() { return null; }

        public virtual void updateStatus(double value) { }

        public virtual void expand() { }

        public virtual double rollOut(Node tn) { return 0; }
    }
}
