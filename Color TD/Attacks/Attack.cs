using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    enum AttackType
    {
        Laser,
        Bolt
    }

    abstract class Attack : GameObject
    {
        protected Dot target;
        protected Tower shooter;
        protected int damage, hitsLeft;
        protected float aliveTime;

        public Attack (Dot target, Tower shooter, int damage, float aliveTime, int maxHitCount)
        {
            this.target = target;
            this.shooter = shooter;
            this.damage = damage;
            this.aliveTime = aliveTime;
            hitsLeft = maxHitCount;
        }

        abstract public void Update (GameTime gametime);

        abstract public AttackType AttackType { get; }

        public void Kill ()
        {
            aliveTime = 0;
            hitsLeft = 0;
        }

        public void ApplyDamage (Dot enemy)
        {
            if (enemy.IsAlive && enemy.ApplyDamage(this)) hitsLeft--;
        }

        public bool IsAlive => aliveTime > 0 && hitsLeft > 0;

        public bool CanHit => hitsLeft > 0;

        public int Damage => damage;

        public Dot Target
        {
            get { return target; }
            set { target = value; }
        }

        public Tower Shooter => shooter;
    }
}
