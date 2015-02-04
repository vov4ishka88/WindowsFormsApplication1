using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Weapon
    {
        Point position;

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }
        int powerOfDistraction;

        public int PowerOfDistraction
        {
            get { return powerOfDistraction; }
            set { powerOfDistraction = value; }
        }
        int radius;

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        bool taken;

        public bool Taken
        {
            get { return taken; }
            set { taken = value; }
        }
        Size size;

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        

        public Weapon(Point position, int power, int radius) 
        {
            this.position = position;
            this.powerOfDistraction = power;
            this.radius = radius;
            taken = false;
            size = new Size(15, 15);
        }

        public void Paint(Graphics g) 
        {
            if (!taken) g.FillRectangle(Brushes.Black, new Rectangle(position, new Size(15, 15)));
        }
    }
}
