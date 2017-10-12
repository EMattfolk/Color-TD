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
        Laser,
        Bolt,
        None
    }

    abstract class Tower : GameObject
    {
        private static Bitmap[] images = new Bitmap[] { new Bitmap("..\\..\\Tower_laser.png"), new Bitmap("..\\..\\Tower_bolt.png") };

        protected Dot target;
        protected float fireDelay, timeSinceLastShot;
        protected int damage, range, cost;
        private bool hasValidPosition;

        public Tower (Point position, float scale, float rotation, float fireDelay, int damage, int range, int cost)
        {
            this.fireDelay = fireDelay;
            this.damage = damage;
            this.range = range;
            this.cost = cost;
            Position = position;
            Size = 64;
            Width = 64;
            Height = 64;
            Scale = scale;
            Rotation = rotation;
            timeSinceLastShot = 0;
            hasValidPosition = false;
        }

        public static Tower FromTowerType (TowerType type)
        {
            switch (type)
            {
                case TowerType.Laser:
                    return new LaserTower();
                case TowerType.Bolt:
                    return new BoltTower();
                default:
                    return null;
            }
        }

        public bool HasTarget ()
        {
            if (target != null && target.IsAlive && DistanceTo(target) < range)
            {
                return true;
            }
            target = null;
            return false;
        }

        public void Update (float deltatime)
        {
            timeSinceLastShot += deltatime;
        }

        protected void TurnToTarget ()
        {
            Rotation = (float)Math.Atan2(target.Position.Y - Position.Y, target.Position.X - Position.X) * 180 / (float)Math.PI;
        }

        abstract public TowerType TowerType { get; }

        abstract public Attack Shoot ();

        public override Bitmap GetImage() => images[(int)TowerType];

        public int Damage => damage;

        public int Range => range;

        public int Cost => cost;

        public Dot Target
        {
            get { return target; }

            set { target = value; }
        }

        public bool HasValidPosition
        {
            get { return hasValidPosition; }

            set { hasValidPosition = value; }
        }
    }
}
