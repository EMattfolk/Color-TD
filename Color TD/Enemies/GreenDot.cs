using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class GreenDot : Dot
    {
        public GreenDot () : base(4, 150, 0.175f, 30, 0) { }

        public override EnemyType EnemyType => EnemyType.GreenDot;
    }
}
