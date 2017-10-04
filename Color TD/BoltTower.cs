using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class BoltTower : Tower
    {
        public BoltTower(Point position) : base(position, .5f, 0, 10, 10, 100) { }

        public override TowerType TowerType => TowerType.Bolt;

        public override Attack Shoot()
        {
            if (framesSinceLastShot >= fireRate && target != null)
            {
                framesSinceLastShot = 0;
                TurnToTarget();
                return new BoltAttack(this, damage, 1, 200, 0.4f);
            }
            return null;
        }
    }
}
