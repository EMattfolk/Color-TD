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

        public LaserTower () : this(new Vector2()) { }

        public LaserTower (Vector2 position) : base(position, .5f, 0, 1/2f, 10, 100, 50)
        {
            UpgradeCosts = new List<int>() { 100, 600, 0 };
        } //TODO: change attack to make it more effective

        public static List<Texture2D> Sprites => sprites;

        public override TowerType TowerType => TowerType.Laser;

        public override Texture2D GetSprite() => sprites[level];

        public override Attack Shoot()
        {
            if (timeSinceLastShot >= fireDelay && target != null)
            {
                timeSinceLastShot = 0;
                TurnToTarget();
                return new LaserAttack(target, this, damage, 1, 1);
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
            }
            else if (level == 2)
            {
                fireDelay = 1 / 30f;
                range += 20;
            }
        }
    }
}
