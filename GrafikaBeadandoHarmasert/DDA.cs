using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaBeadandoHarmasert
{
    class DDA
    {
        public DDA(Graphics g)
        {

            this.g = g;
        }
        private Graphics g;
        public void Draw(Pen pen, int x0, int y0, int x1, int y1) {
            double dx = x1 - x0;
            double dy = y1 - y0;
            double length = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);
            double x_increment = dx / length;
            double y_increment = dy / length;
            double x = x0;
            double y = y0;
            g.DrawRectangle(pen, (int)x, (int)y, 0.5f, 0.5f);
            for (int i = 1; i < length; i++)
            {
                x += x_increment;
                y += y_increment;
                g.DrawRectangle(pen, (int)x, (int)y, 0.5f, 0.5f);
            }
        }
    }
}
