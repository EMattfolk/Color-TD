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
    class LaserTower : Tower
    {
        private static List<Texture2D> sprites = new List<Texture2D>();
        private int laserIndex;

        public LaserTower () : this(new Vector2()) { }

        public LaserTower (Vector2 position) : base(position, 1/2f, 0, 1/2f, 10, 100, 50)
        {
            UpgradeCosts = new List<int>() { 90, 550, 2000, 0 };
            SellValues = new List<int>() { 45, 126, 621, 2421 };
            laserIndex = 1;
            Color = Color.DarkOrange;
        } //TODO: change attack to make it more effective

        public static List<Texture2D> Sprites => sprites;

        public override TowerType TowerType => TowerType.Laser;

        public override Texture2D GetSprite() => sprites[level];

        public override Attack Shoot()
        {
            if (target == null)
            {
                timeSinceLastShot = Math.Min(timeSinceLastShot, fireDelay);
            }
            else
            {
                while (timeSinceLastShot >= fireDelay)
                {
                    timeSinceLastShot -= fireDelay;
                    TurnToTarget();
                    return new LaserAttack(target, this, damage, 1, 1, laserIndex);
                }
            }
            return null;
        }

        public override void Upgrade()
        {
            level++;
            if (level == 1)
            {
                fireDelay = 1 / 6f;
                range += 20;
                Color = Color.Cyan;
            }
            else if (level == 2)
            {
                fireDelay = 1 / 30f;
                range += 20;
                Color = Color.DarkRed;
            }
            else if (level == 3)
            {
                damage += 10;
                laserIndex++;
                Color = Color.Blue;
                fireDelay = 1 / 60f;
                range += 20;
            }
        }
    }
}
