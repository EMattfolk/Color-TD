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
    enum TowerType
    {
        Laser,
        Bolt,
        None
    }

    abstract class Tower : GameObject
    {
        protected Dot target;
        protected float fireDelay, timeSinceLastShot;
        protected int damage, range, cost, level;
        protected List<int> UpgradeCosts, SellValues;
        private bool hasValidPosition;

        public Tower (Vector2 position, float scale, float rotation, float fireDelay, int damage, int range, int cost)
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
            level = 0;
            Rotation = rotation;
            timeSinceLastShot = 1000;
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

        public void Update (GameTime gameTime)
        {
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected virtual void TurnToTarget ()
        {
            Rotation = (float)Math.Atan2(target.Position.Y - Position.Y, target.Position.X - Position.X);
        }

        abstract public TowerType TowerType { get; }

        abstract public Attack Shoot ();

        abstract public void Upgrade();

        virtual public string GetInfo()
        {
            return "Level: " + (level + 1).ToString() + Environment.NewLine + "Damage: " + damage.ToString() + Environment.NewLine + "Firerate: " + Math.Round(1 / fireDelay, 2).ToString();
        }

        public int Damage => damage;

        public int Range => range;

        public int Cost => cost;

        public int Level => level;

        public int UpgradeCost => UpgradeCosts[level];

        public int SellValue => SellValues[level];

        public bool CanUpgrade => UpgradeCosts[level] != 0;

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
