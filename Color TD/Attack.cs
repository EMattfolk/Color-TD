using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
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

        public abstract void Update (); // Add firstUpdate to increase effectiveness?

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
