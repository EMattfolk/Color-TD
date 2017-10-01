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

        public BoltAttack(Tower shooter, int damage, int maxHitCount, int speed, int size) : base(null, shooter, damage, 100, maxHitCount)
        {
            Position = shooter.Position;
            Size = size;
            Rotation = shooter.Rotation;
            xVelocity = (float)Math.Cos(Rotation * Math.PI / 180) * speed;
            yVelocity = (float)Math.Sin(Rotation * Math.PI / 180) * speed;
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
