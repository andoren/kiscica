using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaBeadandoHarmasert
{
    class Line
    {
		public Line()
		{

		}
		public Line(int X0,int Y0,int X1,int Y1)
		{
			this.X0 = X0;
			this.Y0 = Y0;
			this.X1 = X1;
			this.Y1 = Y1;
		}
		private int x0;

		public int X0
		{
			get { return x0; }
			set { x0 = value; }
		}
		private int y0;

		public int Y0
		{
			get { return y0; }
			set { y0 = value; }
		}
		private int x1;

		public int X1
		{
			get { return x1; }
			set { x1 = value; }
		}
		private int y1;

		public int Y1
		{
			get { return y1; }
			set { y1 = value; }
		}




	}
}
