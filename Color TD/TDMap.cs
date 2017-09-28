using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class TDMap
    {
        private Point[] path;
        private Bitmap map;
        private float totalDistance;
        private float[] cumulativeDistances;

        public TDMap (string imagePath, Point[] path)
        {
            this.path = path;
            map = new Bitmap(imagePath);
            SetupDistances();
        }

        private void SetupDistances ()
        {
            totalDistance = 0;
            cumulativeDistances = new float[path.Length];
            cumulativeDistances[0] = 0;
            int x, y;
            float dist;
            for (int i = 0; i < path.Length - 1; i++)
            {
                x = path[i].X - path[i + 1].X;
                y = path[i].Y - path[i + 1].Y;
                dist = (float) Math.Sqrt(x * x + y * y);
                totalDistance += dist;
                cumulativeDistances[i + 1] = totalDistance;
            }
        }

        public PointF GetPosition (float distance) //TODO: Optimize
        {
            int index = 0;
            for (int i = 0; i < cumulativeDistances.Length; i++)
            {
                if (distance > cumulativeDistances[i])
                {
                    index = i;
                }
                else { break; }
            }
            if (index == cumulativeDistances.Length - 1)
            {
                return path[path.Length - 1];
            }
            else
            {
                int x, y;
                x = path[index + 1].X - path[index].X;
                y = path[index + 1].Y - path[index].Y;
                float progress = (distance - cumulativeDistances[index]) / (float) Math.Sqrt(x * x + y * y);
                return new PointF(path[index].X + x * progress, path[index].Y + y * progress);
            }
        }

        public bool HasFinished (float distance) => distance > totalDistance;

        public Bitmap Map => map;
    }
}
