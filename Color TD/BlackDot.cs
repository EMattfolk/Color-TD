using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class BlackDot : Dot
    {
        public BlackDot () : base(100, 0.125f, 10, 0, 0) { }

        public override EnemyType EnemyType => EnemyType.BlackDot;
    }
}
