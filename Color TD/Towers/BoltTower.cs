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
    class BoltTower : Tower
    {
        private static List<Texture2D> sprites = new List<Texture2D>();

        public BoltTower() : this(new Vector2()) { }

        public BoltTower(Vector2 position) : base(position, .5f, 0, 1/4f, 10, 80, 200) { }

        public static List<Texture2D> Sprites => sprites;

        public override TowerType TowerType => TowerType.Bolt;

        public override Texture2D GetSprite() => sprites[level];

        public override Attack Shoot()
        {
            if (timeSinceLastShot >= fireDelay && target != null)
            {
                timeSinceLastShot = 0;
                TurnToTarget();
                return new BoltAttack(this, damage, 2, 600, 0.5f, level);
            }
            return null;
        }
    }
}
