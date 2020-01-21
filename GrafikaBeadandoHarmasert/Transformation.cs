using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaBeadandoHarmasert
{
    class Transformation
    {
        static public Point WindowToViewPort(Rectangle WindowRect, Rectangle ViewPortRect, Point p) {


            float sx = (ViewPortRect.Right - ViewPortRect.Left) /
                (float)((WindowRect.Right - WindowRect.Left));
            float sy = (ViewPortRect.Bottom - ViewPortRect.Top) /
                (float)((WindowRect.Bottom - WindowRect.Top));


            Point p1 = new Point();
            p1.X = (int)(ViewPortRect.Left + (p.X - WindowRect.Left) * sx);
            p1.Y = (int)(ViewPortRect.Bottom + (p.Y -WindowRect.Bottom) * sy); 

            return p1;
        }
    }
}
