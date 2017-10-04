using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class BoltAttack : Attack
    {
        private static Bitmap[] images = new Bitmap[] { new Bitmap("..\\..\\Bolt_blue.png") };

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

        public override void Update(float deltaTime)
        {
            aliveTime--;
            Position = new PointF(Position.X + xVelocity * deltaTime, Position.Y + yVelocity * deltaTime);
        }

        public override AttackType AttackType => AttackType.Bolt;

        public override Bitmap GetImage () => images[0];
    }
}
