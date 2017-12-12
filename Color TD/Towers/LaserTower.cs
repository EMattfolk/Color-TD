﻿using Microsoft.Xna.Framework;
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

        public LaserTower (Vector2 position) : base(position, .5f, 0, 1/2f, 10, 100, 50)
        {
            UpgradeCosts = new List<int>() { 110, 620, 2400, 0 };
            SellValues = new List<int>() { 45, 144, 702, 2862 };
            laserIndex = 0;
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
                return new LaserAttack(target, this, damage, 1, 1, laserIndex);
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
            else if (level == 3)
            {
                damage += 10;
                laserIndex++;
                fireDelay = 1 / 60f;
                range += 20;
            }
        }
    }
}
