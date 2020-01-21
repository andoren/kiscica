using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaBeadandoHarmasert
{
    class SutherHodgmanAlgo
    {
        public SutherHodgmanAlgo(List<Point> PoligonPoints)
        {
            this.PoligonPoints = new List<Point>(PoligonPoints);
            //this.PoligonPoints.Reverse();
        }
        private  List<Point> PoligonPoints;
        private void Clip(Point[] EdgesPoints ) {

            List<Point> ValidPoints = new List<Point>(PoligonPoints);
            PoligonPoints.Clear();
            for (int i = 0; i < ValidPoints.Count; i++)
            {
                int k = (i + ValidPoints.Count - 1) % ValidPoints.Count;
                Point p1 = ValidPoints[i];
                Point p2 = ValidPoints[k]; ;
                int B = LeftOrRight(EdgesPoints[0], EdgesPoints[1], p1 ) ;
                int A = LeftOrRight(EdgesPoints[0], EdgesPoints[1], p2 ) ;
                if (A >= 0 && B >= 0)
                {

                    PoligonPoints.Add(p1);
                }
                else if (A >= 0 && B <= 0)
                {
                    PoligonPoints.Add(IntersectionPoint(p1, p2, EdgesPoints[0], EdgesPoints[1]));
                }
                else if(B>= 0 && A <= 0)
                {
                    PoligonPoints.Add(IntersectionPoint(p1, p2, EdgesPoints[0], EdgesPoints[1]));
                    PoligonPoints.Add(p1);
                }
            }
         
        }

        /// <summary>
        /// Kiszámolja, hogy a pont a szaksz jobb vagy baloldalán van. Ha balodalon van akkor pozitív értékkel tér vissza, ha negativ akkor jobb oldalon.
        /// </summary>
        /// <param name="Edge1">A szakasz "A" pontja</param>
        /// <param name="Edge2">A szakasz "B" pontja</param>
        /// <param name="p">A vizsgálni kívánt pont.</param>
        /// <returns></returns>
        private int LeftOrRight(Point Edge1, Point Edge2, Point p)
        {
            int xc = ((Edge2.X - Edge1.X) * (p.Y - Edge2.Y) - (Edge2.Y - Edge1.Y) * (p.X - Edge2.X));
            return xc * -1;
        }

        private Point IntersectionPoint(Point prevPoint, Point currentPoint, Point Edge1, Point Edge2) {
            Point point = new Point();
            try
            {
                int x = (((prevPoint.X * currentPoint.Y - prevPoint.Y * currentPoint.X) * (Edge1.X - Edge2.X) - (prevPoint.X - currentPoint.X) * (Edge1.X * Edge2.Y - Edge1.Y * Edge2.X)))

    / ((prevPoint.X - currentPoint.X) * (Edge1.Y - Edge2.Y) - ((prevPoint.Y - currentPoint.Y) * (Edge1.X - Edge2.X)));

                int y = (((prevPoint.X * currentPoint.Y - prevPoint.Y * currentPoint.X) * (Edge1.Y - Edge2.Y) - (prevPoint.Y - currentPoint.Y) * (Edge1.X * Edge2.Y - Edge1.Y * Edge2.X)))

                    / ((prevPoint.X - currentPoint.X) * (Edge1.Y - Edge2.Y) - ((prevPoint.Y - currentPoint.Y) * (Edge1.X - Edge2.X)));
                point = new Point(x, y);
            }
            catch (DivideByZeroException ) {
                return prevPoint;
            }

            return point;
        }

        public void SutherHodgman(Graphics g, Pen p, Point[] Edges) {


            for (int i = 0; i < Edges.Length-1 ; i++)
            {

                Clip(new Point[2] { Edges[i], Edges[i+1] });

            }
            Clip(new Point[2] { Edges[Edges.Length-1], Edges[0] });
            DDA DDAAlgo = new DDA(g);
            for (int j = 0; j < PoligonPoints.Count - 1; j++)
            {

                DDAAlgo.Draw(p, PoligonPoints[j].X, PoligonPoints[j].Y, PoligonPoints[j + 1].X, PoligonPoints[j + 1].Y);


            }

            if (PoligonPoints.Count > 1) DDAAlgo.Draw(p, PoligonPoints[PoligonPoints.Count - 1].X, PoligonPoints[PoligonPoints.Count - 1].Y, PoligonPoints[0].X, PoligonPoints[0].Y);

        }

    }
}
