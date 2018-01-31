using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class Line
    {
        private Vector2 p1, p2;
        private float k, m;

        public Line (Vector2 p1, Vector2 p2)
        {
            if (p1.X == p2.X)
            {
                k = 999999;
            }
            else if (p1.Y == p2.Y)
            {
                k = 0;
            }
            else
            {
                k = (p1.Y - p2.Y) / (p1.X - p2.X);
                m = p1.Y - k * p1.X;
            }
            this.p1 = p1;
            this.p2 = p2;
        }

        public float DistanceToPoint (Vector2 p)
        {
            if (k == 999999)
            {
                if ((p.Y > p1.Y && p.Y > p2.Y) || (p.Y < p1.Y && p.Y < p2.Y)) return MathHelper.Min(Vector2.Distance(p, p1), Vector2.Distance(p, p2));
                else return Math.Abs(p.X - p1.X);
            }
            else if (k == 0)
            {
                if ((p.X > p1.X && p.X > p2.X) || (p.X < p1.X && p.X < p2.X)) return MathHelper.Min(Vector2.Distance(p, p1), Vector2.Distance(p, p2));
                else return Math.Abs(p.Y - p1.Y);
            }
            else
            {
                float kNew = 1 / k, mNew = p.Y - kNew * p.X, xIntersect = (mNew - m) / (k - kNew);
                if ((xIntersect > p1.X && xIntersect > p2.X) || (xIntersect < p1.X && xIntersect < p2.X)) return MathHelper.Min(Vector2.Distance(p, p1), Vector2.Distance(p, p2));
                return Math.Abs((p2.Y - p1.Y) * p.X - (p2.X - p1.X) * p.Y + p2.X * p1.Y - p2.Y * p1.X) / Vector2.Distance(p1, p2);
            }
        }
    }
}
