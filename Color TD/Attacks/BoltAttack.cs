﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_TD
{
    class BoltAttack : Attack
    {
        private static List<Texture2D> sprites = new List<Texture2D>();
        private Vector2 velocity;
        private int level;

        public BoltAttack(Tower shooter, int damage, int maxHitCount, int speed, float scale, int level) : base(null, shooter, damage, 2, maxHitCount)
        {
            Position = shooter.Position;
            Rotation = shooter.Rotation;
            velocity = new Vector2((float)Math.Cos(Rotation) * speed, (float)Math.Sin(Rotation) * speed);
            this.level = level;
            Size = 32;
            Width = 32;
            Height = 16; //TODO: change
            Scale = scale;
        }

        public override void Update(GameTime gameTime)
        {
            aliveTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override AttackType AttackType => AttackType.Bolt;

        public override Texture2D GetSprite() => sprites[level];

        public static List<Texture2D> Sprites => sprites;
    }
}