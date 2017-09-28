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

        private PointF position;
        private int size;
        private float rotation, xVelocity, yVelocity;

        public BoltAttack(Tower shooter, int damage, int maxHitCount, int speed, int size) : base(null, shooter, damage, 100, maxHitCount)
        {
            position = shooter.Position;
            this.size = size;
            rotation = shooter.Rotation;
            xVelocity = (float)Math.Cos(rotation * Math.PI / 180) * speed;
            yVelocity = (float)Math.Sin(rotation * Math.PI / 180) * speed;
        }

        public override void Update(float deltaTime)
        {
            aliveTime--;
            position.X += xVelocity * deltaTime;
            position.Y += yVelocity * deltaTime;
        }

        public override AttackType AttackType => AttackType.Bolt;

        public override int Size => size;

        public override float Rotation => rotation;

        public override PointF Position => position;

        public override Bitmap GetImage () => images[0];
    }
}
