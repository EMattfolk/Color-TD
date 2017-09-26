using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    abstract class Tower
    {
        private static Bitmap[] images = new Bitmap[] { new Bitmap("..\\..\\Tower_laser.png") };
        private Dot target;
        private Point position;
        private float rotation, fireRate;
        private int size, damage, range;

        public Tower (Point position, int size, float rotation, float fireRate, int damage, int range)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;
            this.fireRate = fireRate;
            this.damage = damage;
            this.range = range;
        }

        public bool HasTarget ()
        {
            if (target != null && Distance(target.Position, position) < range)
            {
                return true;
            }
            target = null;
            return false;
        }

        abstract public Bitmap GetImage();

        public static Bitmap[] Images
        {
            get { return images; }
        }

        public float Rotation
        {
            get { return rotation; }

            set { rotation = value; }
        }

        public Point Position
        {
            get { return position; }
        }

        public int Damage
        {
            get{ return damage; }
        }

        public int Range
        {
            get { return range; }
        }

        public int Size
        {
            get { return size; }
        }

        private double Distance (PointF p1, PointF p2)
        {
            float x = p1.X - p2.X, y = p1.Y - p2.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}
