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

        public LaserAttack (Dot target, Tower shooter, int damage, int aliveTime, int maxHitCount) : base(target, shooter, damage, aliveTime, maxHitCount) { }

        public static List<Texture2D> Sprites => sprites;

        public override AttackType AttackType => AttackType.Laser;

        public override Texture2D GetSprite() => sprites[0];

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
