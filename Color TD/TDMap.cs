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
        private double totalDistance;
        private double[] cumulativeDistances;

        public TDMap (string imagePath, Point[] path)
        {
            this.path = path;
            map = new Bitmap(imagePath);
            SetupDistances();
        }

        private void SetupDistances ()
        {
            totalDistance = 0;
            cumulativeDistances = new double[path.Length];
            cumulativeDistances[0] = 0;
            int x, y;
            double dist;
            for (int i = 0; i < path.Length - 1; i++)
            {
                x = path[i].X - path[i + 1].X;
                y = path[i].Y - path[i + 1].Y;
                dist = Math.Sqrt(x * x + y * y);
                totalDistance += dist;
                cumulativeDistances[i + 1] = totalDistance;
            }
        }

        public PointD GetPosition (double distance) //TODO: Optimize
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
                return new PointD(path[path.Length - 1]);
            }
            else
            {
                int x, y;
                x = path[index + 1].X - path[index].X;
                y = path[index + 1].Y - path[index].Y;
                double progress = (distance - cumulativeDistances[index]) / Math.Sqrt(x * x + y * y);
                return new PointD(path[index].X + x * progress, path[index].Y + y * progress);
            }
        }

        public bool HasFinished (double distance)
        {
            return distance > totalDistance;
        }

        public Bitmap Map
        {
            get { return map; }
        } 
    }
}
