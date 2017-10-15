using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class RedDot : Dot
    {
        public RedDot() : base(5, 160, 0.185f, 50, 10) { }

        public override EnemyType EnemyType => EnemyType.RedDot;
    }
}
