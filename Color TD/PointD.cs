using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class PointD
    {
        double x, y;

        public PointD() : this(0, 0) { }

        public PointD (Point point)
        {
            x = point.X;
            y = point.Y;
        }

        public PointD (double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }

        public override string ToString ()
        {
            return "PointD(" + x.ToString() + ", " + y.ToString() + ")";
        }
    }
}
