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
        public static double d = 0.01;
        public static double PGL;
        public static double s;

        public double nVisits { set; get; }
        public double winRate { set; get; }

        public byte name { set; get; }

        public Move action { set; get; }
        public virtual bool isLeaf() { return true; }
        public virtual Node select() { return null; }

        public virtual void updateStatus(double value) { }

        public virtual void expand() { }

        public virtual double rollOut(Node tn,long length) { return 0; }
    }
}
