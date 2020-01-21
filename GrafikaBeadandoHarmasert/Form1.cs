using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaBeadandoHarmasert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitData();

        }
        //Windows
        Rectangle ScreenRect;
        Rectangle LeftRect;
        Rectangle RightRect;
        int Leftx, Rightx, Lefty, Righty;
        int Leftwidth, Leftheight, Rightwidth, Rightheight;
        //Graphics
        Graphics g;
        Pen WindowPens = new Pen(Brushes.Black, 2f);
        Pen PolygonPen = new Pen(Brushes.Black,2f);
        Pen InPolygonPen = new Pen(Brushes.Green, 3.5f);
        List<Point> Points = new List<Point>();
        List<Point> TransformedPoints = new List<Point>();
        List<Point> LeftEdges = new List<Point>();
        List<Point> RightEdges = new List<Point>();
        private void InitData()
        {
            
            
            ScreenRect = new Rectangle(Canvas.Left, Canvas.Top, Canvas.Width - 20, Canvas.Height - 20);

            Leftwidth = 400; Leftheight = 300; Leftx = ScreenRect.Width/4 - Leftwidth/2; Lefty = 65;
            LeftRect = new Rectangle(Leftx, Lefty, Leftwidth, Leftheight);

            Rightx = Canvas.Left + Canvas.Width / 2 + 160; Righty = 50; Rightwidth = 150; Rightheight = 450;
            RightRect = new Rectangle(Rightx, Righty, Rightwidth, Rightheight);

            LeftEdges.Add(new Point(LeftRect.Left, LeftRect.Top));
            LeftEdges.Add(new Point(LeftRect.Left, LeftRect.Bottom));
            LeftEdges.Add(new Point(LeftRect.Right, LeftRect.Bottom));
            LeftEdges.Add(new Point(LeftRect.Right, LeftRect.Top));
            RightEdges.Add(new Point(RightRect.Left, RightRect.Top));
            RightEdges.Add(new Point(RightRect.Left, RightRect.Bottom));
            RightEdges.Add(new Point(RightRect.Right, RightRect.Bottom));
            RightEdges.Add(new Point(RightRect.Right, RightRect.Top));

        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            DrawWindows();
            DrawPolygon(PolygonPen);
        }
        private void DrawWindows()
        {

            g.DrawLine(WindowPens, ScreenRect.Width / 2, ScreenRect.Top, ScreenRect.Width / 2, ScreenRect.Height+12);
            g.DrawRectangle(WindowPens, ScreenRect);
            g.DrawRectangle(WindowPens, LeftRect);
            g.DrawRectangle(WindowPens, RightRect);
        }
        private void DrawPolygon(Pen p) {
            SutherHodgmanAlgo LeftClip = new SutherHodgmanAlgo(Points);
            SutherHodgmanAlgo RightClip = new SutherHodgmanAlgo(TransformedPoints);
            DDA DDAAlgo = new DDA(g);
            for (int i = 0; i < Points.Count ; i++)
            {
                int k = (i + Points.Count - 1) % Points.Count;
                DDAAlgo.Draw(p, Points[i].X, Points[i].Y, Points[k].X, Points[k].Y);
                DDAAlgo.Draw(p, TransformedPoints[i].X, TransformedPoints[i].Y, TransformedPoints[k].X, TransformedPoints[k].Y);
                if (Points.Count > 1) LeftClip.SutherHodgman(g, InPolygonPen, LeftEdges.ToArray());
                if (Points.Count > 1) RightClip.SutherHodgman(g, InPolygonPen, RightEdges.ToArray());
            }
    



        }

        /// <summary>
        /// Megnézi, hogy a pontot jó részre raktuk-e le a képernyőn. Azaz a képernyő bal oldalán van e.
        /// </summary>s
        /// <param name="p">A vizsgálni kívánt pont.</param>
        /// <returns></returns>
        private bool IsValidLeftPoint(Point p) {
            bool valid = false;
            if (p.X < Canvas.Width/2 +10  && p.X > Canvas.Left && p.Y > Canvas.Top  && p.Y < Canvas.Bottom -20 ) valid = true;
            return valid;
                
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsValidLeftPoint(e.Location))
            {

                    Points.Add(new Point(e.Location.X, e.Location.Y));
                    TransformedPoints.Add(Transformation.WindowToViewPort(LeftRect,RightRect,new Point(e.Location.X, e.Location.Y)));
                    Canvas.Invalidate();
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Points.Clear();
            TransformedPoints.Clear();
            Canvas.Refresh();
        }

    }
}
