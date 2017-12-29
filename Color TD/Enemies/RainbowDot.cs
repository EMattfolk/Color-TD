using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class RainbowDot : Dot
    {
        public RainbowDot() : base(500, 100, 0.5f, 5000, 0)
        {
            if (IsUpgraded)
            {
                maxhp *= 5;
                hp *= 5;
            }
            Upgrade();
        }

        public override EnemyType EnemyType => EnemyType.RainbowDot;
    }
}
