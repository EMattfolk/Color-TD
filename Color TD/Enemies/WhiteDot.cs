using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class WhiteDot : Dot
    {
        public WhiteDot() : base(30, 220, 0.25f, 500, 0) { }

        public override EnemyType EnemyType => EnemyType.WhiteDot;
    }
}
