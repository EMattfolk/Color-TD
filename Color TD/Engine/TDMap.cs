using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_TD
{
    class TDMap
    {
        private Vector2[] path;
        private Line[] lines;
        private int spriteIndex;
        private float totalDistance;
        private float[] cumulativeDistances;

        public TDMap (int mapIndex, Vector2[] path)
        {
            spriteIndex = mapIndex;
            this.path = path;
            SetupDistances();
        }

        private void SetupDistances ()
        {
            totalDistance = 0;
            lines = new Line[path.Length - 1];
            cumulativeDistances = new float[path.Length];
            cumulativeDistances[0] = 0;
            float x, y;
            float dist;
            for (int i = 0; i < path.Length - 1; i++)
            {
                lines[i] = new Line(path[i], path[i + 1]);
                x = path[i].X - path[i + 1].X;
                y = path[i].Y - path[i + 1].Y;
                dist = (float) Math.Sqrt(x * x + y * y);
                totalDistance += dist;
                cumulativeDistances[i + 1] = totalDistance;
            }
            Console.WriteLine(totalDistance);
        }

        public Vector2 GetPosition (float distance) //TODO: Optimize if needed
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
                float x, y;
                x = path[index + 1].X - path[index].X;
                y = path[index + 1].Y - path[index].Y;
                float progress = (distance - cumulativeDistances[index]) / (cumulativeDistances[index + 1] - cumulativeDistances[index]);
                return new Vector2(path[index].X + x * progress, path[index].Y + y * progress);
            }
        }

        public float DistanceToPath (Vector2 fromPosition)
        {
            float dist, min = float.MaxValue;
            foreach (Line l in lines)
            {
                dist = l.DistanceToPoint(fromPosition);
                min = min > dist ? dist : min;
            }
            return min;
        }

        public bool HasFinished (float distance) => distance > totalDistance;

        public int SpriteIndex => spriteIndex;
    }
}
