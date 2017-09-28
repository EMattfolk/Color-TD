using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    enum TowerType
    {
        Laser
    }

    abstract class Tower
    {
        private static Bitmap[] images = new Bitmap[] { new Bitmap("..\\..\\Tower_laser.png") };

        protected Dot target;
        protected Point position;
        protected float rotation;
        protected int size, fireRate, damage, range, framesSinceLastShot;

        public Tower (Point position, int size, float rotation, int fireRate, int damage, int range)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;
            this.fireRate = fireRate;
            this.damage = damage;
            this.range = range;
            framesSinceLastShot = 0;
        }

        public bool HasTarget ()
        {
            if (target != null && target.IsAlive && DistanceTo(target.Position) < range)
            {
                return true;
            }
            target = null;
            return false;
        }

        public void UpdateFrameCount ()
        {
            framesSinceLastShot++;
        }

        public double DistanceTo(PointF other)
        {
            float x = position.X - other.X, y = position.Y - other.Y;
            return Math.Sqrt(x * x + y * y);
        }

        protected void TurnToTarget ()
        {
            rotation = (float)Math.Atan2(target.Position.Y - position.Y, target.Position.X - position.X) * 180 / (float)Math.PI;
        }

        abstract public TowerType TowerType { get; }

        abstract public Attack Shoot ();

        public static Bitmap[] Images => images;

        public Bitmap GetImage() => images[(int)TowerType];

        public Point Position => position;

        public int Damage => damage;

        public int Range => range;

        public int Size => size;

        public float Rotation
        {
            get { return rotation; }

            set { rotation = value; }
        }

        public Dot Target
        {
            get { return target; }

            set { target = value; }
        }
    }
}
