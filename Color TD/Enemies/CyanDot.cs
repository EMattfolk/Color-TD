using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class CyanDot : Dot
    {
        public CyanDot() : base(20, 200, 0.225f, 200, 20) { }

        public override EnemyType EnemyType => EnemyType.CyanDot;
    }
}
