using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Actions
    {
        public ACTION action { private set; get; }

        public Actions(ACTION action)
        {
            this.action = action;
        }
    }
}
