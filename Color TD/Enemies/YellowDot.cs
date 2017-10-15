using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class YellowDot : Dot
    {
        public YellowDot() : base(10, 180, 0.2f, 100, 0) { }

        public override EnemyType EnemyType => EnemyType.YellowDot;
    }
}
