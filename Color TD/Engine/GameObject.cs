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
    abstract class GameObject
    {
        private static long objectCount = 0;
        private long Id;
        private Vector2 position = new Vector2();
        private int width = 0, height = 0, size = 0;
        private float rotation = 0, scale = 1;

        public GameObject ()
        {
            Id = objectCount;
            objectCount++;
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public double DistanceTo (GameObject other)
        {
            float x = Position.X - other.Position.X, y = Position.Y - other.Position.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public long ID => Id;

        abstract public int GetSpriteIndex ();
    }
}
