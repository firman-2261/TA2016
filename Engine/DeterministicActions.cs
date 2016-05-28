using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class DeterministicActions:Actions
    {
        public Position from{ private set; get; }
        public List<Position> to { private set; get; }

        public DeterministicActions(Position from, List<Position> to, ACTION action):base(action)
        {
            this.from = from;
            this.to = to;
        }
    }
}
