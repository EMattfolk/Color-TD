using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class LaserAttack : Attack
    {
        public LaserAttack (Dot target, Tower shooter, int damage, int aliveTime, int maxHitCount) : base(target, shooter, damage, aliveTime, maxHitCount) { }

        public override AttackType AttackType => AttackType.Laser;

        public override PointF Position => new PointF();

        public override Bitmap GetImage() => null;

        public override int Size => 0;

        public override float Rotation => 0;

        public override void Update (float deltaTime)
        {
            target.ApplyDamage(damage);
            aliveTime--;
            hitsLeft--;
        }
    }
}
