using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class BoltTower : Tower
    {
        private static List<Texture2D> sprites = new List<Texture2D>();
        private int pierceCount = 2, projectileSpeed = 600;

        public BoltTower() : this(new Vector2()) { }

        public BoltTower(Vector2 position) : base(position, .5f, 0, 1/4f, 10, 80, 200)
        {
            UpgradeCosts = new List<int>() { 1, 1, 0 };
        }

        public static List<Texture2D> Sprites => sprites;

        public override TowerType TowerType => TowerType.Bolt;

        public override Texture2D GetSprite() => sprites[level];

        public override Attack Shoot()
        {
            if (timeSinceLastShot >= fireDelay && target != null)
            {
                timeSinceLastShot = 0;
                TurnToTarget();
                return new BoltAttack(this, damage, pierceCount, projectileSpeed, 0.5f, level);
            }
            return null;
        }

        public override void Upgrade()
        {
            level++;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + Environment.NewLine + "Pierce: " + (pierceCount - 1);
        }
    }
}
