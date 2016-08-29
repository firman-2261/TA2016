using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Engine;

namespace View
{
    public class Focus
    {
        public Rectangle rectangle { set; get; }
        public Position posisi { set; get; }

        public Focus(Rectangle rectangle, Position posisi)
        {
            this.rectangle = rectangle;
            this.posisi = posisi;
        }

        public Focus()
        {
            this.rectangle = new Rectangle();
            this.posisi = new Position();
        }
    }
}
