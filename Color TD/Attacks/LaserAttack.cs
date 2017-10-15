using Microsoft.Xna.Framework;
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

        public override int GetSpriteIndex() => 0;

        public override void Update (GameTime gameTime)
        {
            target.ApplyDamage(this);
            aliveTime--;
            hitsLeft--;
        }
    }
}
