using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Bow : Weapon
    {
      
        public Bow(Point position, int power, int radius) : base(position, power, radius) { }
        public override void Paint(Graphics g)
        {
            if (!Taken) g.FillRectangle(Brushes.Red, new Rectangle(Position, size));
        }
    }
}