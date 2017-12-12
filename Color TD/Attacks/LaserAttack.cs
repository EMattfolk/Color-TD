using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private static List<Texture2D> sprites = new List<Texture2D>();
        private int laserIndex;

        public LaserAttack (Dot target, Tower shooter, int damage, int aliveTime, int maxHitCount, int laserIndex) : base(target, shooter, damage, aliveTime, maxHitCount)
        {
            this.laserIndex = laserIndex;
        }

        public static List<Texture2D> Sprites => sprites;

        public override AttackType AttackType => AttackType.Laser;

        public override Texture2D GetSprite() => sprites[laserIndex * 2];

        public Texture2D GetSplashSprite() => sprites[laserIndex * 2 + 1];

        public override void Update (GameTime gameTime)
        {
            if (IsAlive)
            {
                target.ApplyDamage(this);
                aliveTime--;
                hitsLeft--;
            }
        }
    }
}
