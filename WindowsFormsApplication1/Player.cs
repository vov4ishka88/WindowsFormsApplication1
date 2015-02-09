using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Player : Ball
    {

        public Player() : base() {
            picture = WindowsFormsApplication1.Properties.Resources.Empty_handed_Hero;
            size = new Size(picture.Width, picture.Height);
        }

        public override void paint(Graphics g)
        {
            if (Target.X > Cordinations.X) cordinations.X++;
            if (Target.X < Cordinations.X) cordinations.X--;
            if (Target.Y > Cordinations.Y) cordinations.Y++;
            if (Target.Y < cordinations.Y) cordinations.Y--;
            // g.FillEllipse(color, new Rectangle(cordinations, size));
            if (weapon != null) picture = WindowsFormsApplication1.Properties.Resources.Hero_with_sword;
            else picture = WindowsFormsApplication1.Properties.Resources.Empty_handed_Hero;
            g.DrawImage(picture, cordinations);
            int healt = 30 * healthPoints / maxHP();
            int expir = 30 * expiriance / maxEXP();
            // g.DrawString(healthPoints + "/" + level + "/" + expiriance, SystemFonts.SmallCaptionFont, Brushes.Black, new Point(cordinations.X - 12, cordinations.Y + 23));
            g.FillRectangle(Brushes.DarkMagenta, new Rectangle(cordinations.X + 4, cordinations.Y - 10, 32, 4));
            g.FillRectangle(Brushes.Red, new Rectangle(cordinations.X + 4, cordinations.Y - 9, healt, 2));

            g.FillRectangle(Brushes.DarkMagenta, new Rectangle(cordinations.X + 4, cordinations.Y - 15, 32, 4));
            g.FillRectangle(Brushes.Gold, new Rectangle(cordinations.X + 4, cordinations.Y - 14, expir, 2));

            g.DrawString(level.ToString(), SystemFonts.SmallCaptionFont, Brushes.White, new Point(cordinations.X + 5, cordinations.Y + 2));



        }
    }
}
