using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class BlueDot : Dot
    {
        public BlueDot () : base(2, 110, 0.15f, 20, 0) { }

        public override EnemyType EnemyType => EnemyType.BlueDot;
    }
}
