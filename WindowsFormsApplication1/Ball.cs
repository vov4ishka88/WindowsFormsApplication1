using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    abstract class Ball
    {
        int radiusOfAttack;

        public int RadiusOfAttack
        {
            get { return radiusOfAttack; }
            set { radiusOfAttack = value; }
        }
        protected int level = 1;
        protected int expiriance = 0;
        protected int healthPoints = 0;
        int att;
        int def;
        protected Bitmap picture;
        static Random rnd = new Random();
        protected Weapon weapon;

        protected Point cordinations;

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
        protected Size size;

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public Ball() {
            calcProperties();            
            //size = new Size(picture.Width, picture.Height);
           
        }

        public abstract void paint(Graphics g);

        private void calcProperties() 
        {            
            if (healthPoints <= 0) 
            {
                healthPoints = maxHP();
                if (weapon != null) dropWeapon();
                cordinations = new Point(rnd.Next(800), rnd.Next(600));
                target = cordinations;
            }
            if (expiriance >= maxEXP())
            {
                expiriance -= maxEXP();
                level++;
            }
            if (weapon != null)
            {
                att = maxHP() + weapon.PowerOfDistraction;                
                radiusOfAttack = size.Width + weapon.Radius;                
            }
            else { 
                att = maxHP();                            
                radiusOfAttack = size.Width;
            }         
            def = maxHP();            
            
        }

        public void attack(Ball enemy) 
        {
            if (checkCollision(enemy))
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
            else enemy.attack(this);
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
            if (this.weapon != null) dropWeapon();
            this.weapon = weapon;
            weapon.Taken = true;
            calcProperties();
        }
        public void dropWeapon() 
        {
            weapon.Taken = false;
            weapon.Position = new Point(cordinations.X + size.Width + 10, cordinations.Y + size.Height + 10);
            weapon = null;
            calcProperties();
        }
        protected int maxHP() 
        {
            return 80 + level * 20;
        }
        protected int maxEXP() 
        {
            return level * level * 100;
        }
        public bool checkCollision(Ball enemy)
        {
            return (Cordinations.X > enemy.Cordinations.X - RadiusOfAttack
                && Cordinations.X < enemy.Cordinations.X + RadiusOfAttack
                && Cordinations.Y > enemy.Cordinations.Y - RadiusOfAttack
                && Cordinations.Y < enemy.Cordinations.Y + RadiusOfAttack);                
        }
        
        public bool checkCollisionWithWeapon(Weapon w)
        {
            return (Cordinations.X < w.Position.X && Cordinations.X + Size.Width > w.Position.X + w.Size.Width
                && Cordinations.Y < w.Position.Y && Cordinations.Y + Size.Height > w.Position.Y + w.Size.Height);           

        }
    }
}
