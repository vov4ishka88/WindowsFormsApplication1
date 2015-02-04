using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Ball
    {
        int radiusOfAttack;

        public int RadiusOfAttack
        {
            get { return radiusOfAttack; }
            set { radiusOfAttack = value; }
        }
        int level = 1;
        int expiriance = 0;
        int healthPoints = 0;
        int att;
        int def;
        Bitmap picture;
        static Random rnd = new Random();
        Weapon weapon;

        Point cordinations;

        public Point Cordinations
        {
            get { return cordinations; }
            set { cordinations = value; }
        }
        Point target;

        public Point Target
        {
            get { return target; }
            set { target = value; }
        }
        Brush color;
        Size size;

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public Ball(Color color) {
            calcProperties();
            this.color = new SolidBrush(color);
            size = new Size(picture.Width, picture.Height);
           
        }

        public void paint(Graphics g) {
            if (target.X > cordinations.X) cordinations.X++;
            if (target.X < cordinations.X) cordinations.X--;
            if (target.Y > cordinations.Y) cordinations.Y++;
            if (target.Y < cordinations.Y) cordinations.Y--;
            // g.FillEllipse(color, new Rectangle(cordinations, size));
            g.DrawImage(picture, cordinations);
            int healt = 30 * healthPoints / (80 + level * 20);
            int expir = 30 * expiriance / (level * level * 100);
            // g.DrawString(healthPoints + "/" + level + "/" + expiriance, SystemFonts.SmallCaptionFont, Brushes.Black, new Point(cordinations.X - 12, cordinations.Y + 23));
            g.FillRectangle(Brushes.DarkMagenta, new Rectangle(cordinations.X-5, cordinations.Y+23, 32, 4));
            g.FillRectangle(Brushes.Red, new Rectangle(cordinations.X - 4, cordinations.Y + 24, healt, 2));
            
            g.FillRectangle(Brushes.DarkMagenta, new Rectangle(cordinations.X - 5, cordinations.Y + 28, 32, 4));
            g.FillRectangle(Brushes.Gold, new Rectangle(cordinations.X - 4, cordinations.Y + 29, expir, 2));

            g.DrawString(level.ToString(), SystemFonts.SmallCaptionFont, Brushes.White, new Point(cordinations.X + 5, cordinations.Y + 2));



        }

        private void calcProperties() 
        {
            if (expiriance >= level * level * 100) 
            {
                expiriance -= level * level * 100;
                level++;
            }
            if (healthPoints <= 0) 
            {
                healthPoints = 80 + level * 20;
                cordinations = new Point(rnd.Next(800), rnd.Next(600));
                target = cordinations;
            }
            if (weapon != null)
            {
                att = (20 + level * 20) * weapon.PowerOfDistraction;
                picture = WindowsFormsApplication1.Properties.Resources.Hero_with_sword;
                radiusOfAttack = picture.Width + weapon.Radius;                
            }
            else { 
                att = (20 + level * 20);                
                picture = WindowsFormsApplication1.Properties.Resources.Empty_handed_Hero;
                radiusOfAttack = picture.Width;
            }         
            def = 80 + level * 20;            
            
        }

        public void attack(Ball enemy) 
        {
            int a = rnd.Next(att);
            bool victory = enemy.defence(a);
            if (victory)
            {
                expiriance += 50 * enemy.level / this.level;
                calcProperties();
            }
            else 
            {
                enemy.attack(this);
            }
        }
        public bool defence(int a) 
        {
            int d = rnd.Next(def);
            if (a > d) healthPoints -= (a - d);
            if (healthPoints <= 0) 
            {
                calcProperties();
                return true;
            }
            else return false;
        }
        public void takeWeapon(Weapon weapon) 
        {
            this.weapon = weapon;
            weapon.Taken = true;
            calcProperties();
        }
        public void dropWeapon() 
        {
            weapon.Taken = false;
            weapon.Position = new Point(cordinations.X + 30, cordinations.Y + 30);
            weapon = null;
            calcProperties();
        }
    }
}
