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
        private float xVelocity, yVelocity;

        public BoltAttack(Tower shooter, int damage, int maxHitCount, int speed, float scale) : base(null, shooter, damage, 100, maxHitCount)
        {
            Position = shooter.Position;
            Rotation = shooter.Rotation;
            xVelocity = (float)Math.Cos(Rotation * Math.PI / 180) * speed;
            yVelocity = (float)Math.Sin(Rotation * Math.PI / 180) * speed;

            Size = 32;
            Width = 32;
            Height = 16; //TODO: change
            Scale = scale;
        }

        public override void Update(GameTime gameTime)
        {
            aliveTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += new Vector2((float)(xVelocity * gameTime.ElapsedGameTime.TotalSeconds), (float)(yVelocity * gameTime.ElapsedGameTime.TotalSeconds));
        }

        public override AttackType AttackType => AttackType.Bolt;

        public override int GetSpriteIndex() => 0;
    }
}
