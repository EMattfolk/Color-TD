using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    enum AttackType
    {
        Laser
    }

    abstract class Attack
    {
        protected Dot target;
        protected Tower shooter;
        protected int damage, aliveTime, hitsLeft;

        public Attack (Dot target, Tower shooter, int damage, int aliveTime, int maxHitCount)
        {
            this.target = target;
            this.shooter = shooter;
            this.damage = damage;
            this.aliveTime = aliveTime;
            hitsLeft = maxHitCount;
        }

        abstract public void Update (); // Add firstUpdate to increase effectiveness?

        abstract public AttackType AttackType { get; }

        public void Kill ()
        {
            aliveTime = 0;
            hitsLeft = 0;
        }

        public bool IsAlive => aliveTime > 0 && hitsLeft > 0;

        public Dot Target => target;

        public Tower Shooter => shooter;
    }
}
