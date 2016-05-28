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
        public static double cr = 0.3;
        public static double cs = 0.9;
        public static int side;
        public static double d = 0.01;
        public static double PGL;
        public static double s;

        public double nVisits, winRate;
        public Move action;
        public NODE type;
        public virtual bool isLeaf() { return true; }
        public virtual Node select() { return null; }

        public virtual void updateStatus(double value) { }

        public virtual void expand() { }

        public virtual double rollOut(Node tn,long length) { return 0; }
    }
}
