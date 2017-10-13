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
    class BoltAttack : Attack
    {
        private Vector2 velocity;

        public BoltAttack(Tower shooter, int damage, int maxHitCount, int speed, float scale) : base(null, shooter, damage, 100, maxHitCount)
        {
            Position = shooter.Position;
            Rotation = shooter.Rotation;
            velocity = new Vector2((float)Math.Cos(Rotation) * speed, (float)Math.Sin(Rotation) * speed);

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

        public override int GetSpriteIndex() => 0;
    }
}
