﻿using System;
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
        protected int fireRate, damage, range, framesSinceLastShot, cost;

        public Tower (Point position, float scale, float rotation, int fireRate, int damage, int range, int cost)
        {
            this.fireRate = fireRate;
            this.damage = damage;
            this.range = range;
            this.cost = cost;
            Position = position;
            Size = 64;
            Width = 64;
            Height = 64;
            Scale = scale;
            Rotation = rotation;
            framesSinceLastShot = 0;
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

        public void UpdateFrameCount ()
        {
            framesSinceLastShot++;
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
    }
}
