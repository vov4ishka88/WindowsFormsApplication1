using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Ball current;
        List<Ball> balls = new List<Ball>();
        List<Weapon> weapons = new List<Weapon>();
        public Form1()
        {
            balls.Add(new Player());
            balls.Add(new Player());
            balls.Add(new Player());
            balls.Add(new Player());
            balls.Add(new Player());
            current = balls[0];
            weapons.Add(new Sword(new Point(100, 100), 3, 20));
            weapons.Add(new Bow(new Point(50, 380), 3, 200));
            weapons.Add(new Sword(new Point(300, 400), 3, 20));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ball b in balls)
            {
                b.paint(e.Graphics);

            }
            foreach (Weapon w in weapons) 
            {
                w.Paint(e.Graphics);
            }
        }
       
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Ball b in balls)
            {
                if (e.X > b.Cordinations.X && e.X < b.Cordinations.X + b.Size.Width
                    && e.Y > b.Cordinations.Y && e.Y < b.Cordinations.Y + b.Size.Height)
                {
                    current = b;
                    return;
                }                 
            }
                current.Target = new Point(e.X - current.Size.Width/2, e.Y - current.Size.Height/2);
        }    
 
       
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (i != j && balls[i].checkCollision(balls[j])) balls[i].attack(balls[j]);

                }
                for (int k = 0; k < weapons.Count; k++)
                {
                    if (balls[i].checkCollisionWithWeapon(weapons[k])) balls[i].takeWeapon(weapons[k]);
                }
            }
            Refresh();
        }
    }
}
