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
            balls.Add(new Ball(Color.Red));
            balls.Add(new Ball(Color.Blue));
            balls.Add(new Ball(Color.Orange));
            balls.Add(new Ball(Color.Black));
            balls.Add(new Ball(Color.Green));
            current = balls[0];
            weapons.Add(new Weapon(new Point(100, 100), 3, 20));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                for (int i = 0; i < balls.Count; i++)
                {
                    for (int j = i + 1; j < balls.Count; j++)
                    {
                        checkCollision(balls[i], balls[j]);
                    }
                    for (int k = 0; k < weapons.Count; k++) 
                    {
                        checkCollisionWithWeapon(balls[i], weapons[k]);
                    }
                }
                Refresh();
                Thread.Sleep(10);
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

        private void checkCollision(Ball b1, Ball b2) 
        {
            if (b1.Cordinations.X > b2.Cordinations.X - b1.RadiusOfAttack
                && b1.Cordinations.X < b2.Cordinations.X + b1.RadiusOfAttack
                && b1.Cordinations.Y > b2.Cordinations.Y - b1.RadiusOfAttack
                && b1.Cordinations.Y < b2.Cordinations.Y + b1.RadiusOfAttack)
                b1.attack(b2);
        }

        private void checkCollisionWithWeapon(Ball b, Weapon w)
        {
            if (b.Cordinations.X < w.Position.X && b.Cordinations.X + b.Size.Width > w.Position.X + w.Size.Width
                && b.Cordinations.Y < w.Position.Y && b.Cordinations.Y + b.Size.Height > w.Position.Y + w.Size.Height) 
            {
                b.takeWeapon(w);
            } 

        }
    }
}
