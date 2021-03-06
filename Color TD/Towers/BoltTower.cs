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
        private int pierceCount = 2, projectileSpeed = 600;
        private float projectileRotationSpeed = 0;

        public BoltTower() : this(new Vector2()) { }

        public BoltTower(Vector2 position) : base(position, 1/2f, 0, 1/4f, 10, 80, 200)
        {
            UpgradeCosts = new List<int>() { 220, 510, 2700, 0 };
            SellValues = new List<int>() { 180, 378, 837, 3267 };
        }

        public static List<Texture2D> Sprites => sprites;

        public override TowerType TowerType => TowerType.Bolt;

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
                    return new BoltAttack(this, damage, pierceCount, projectileSpeed, projectileRotationSpeed, 0.5f, level);
                }
            }
            return null;
        }

        public override void Upgrade()
        {
            level++;
            if (level == 1)
            {
                pierceCount++;
                projectileRotationSpeed += 6;
                damage += 5;
                range += 20;
            }
            else if (level == 2)
            {
                pierceCount += 3;
                projectileRotationSpeed += 6;
                damage += 5;
                range += 20;
            }
            else if (level == 3)
            {
                fireDelay = 1 / 8f;
                damage *= 2;
                range += 20;
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + Environment.NewLine + "Pierce: " + (pierceCount - 1);
        }

        protected override void TurnToTarget()
        {
            float t = 0.5f;
            Vector2 v = (target.Position - Position) / t + target.Velocity;
            float speed = (int)(v.Length());
            for (int i = 0; i < 5; i++)
            {
                t *= speed * 1f / projectileSpeed;
                v = (target.Position - Position) / t + target.Velocity;
                speed = v.Length();
            }
            Rotation = (float)Math.Atan2(v.Y,v.X);
        }
    }
}
