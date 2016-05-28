using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class NondeterministicActions:Actions
    {
        public Position position { private set; get; }
        public List<double> probability { private set; get; }

        public List<int> piece { private set; get; }

        public NondeterministicActions(Position position, List<double> probability,List<int> piece,ACTION action):base(action)
        {
            this.position = position;
            this.probability = probability;
            this.piece = piece;
        }
    }
}
