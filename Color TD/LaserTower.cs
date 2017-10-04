using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class LaserTower : Tower
    {
        public LaserTower (Point position) : base(position, .5f, 0, 30, 10, 100) { }

        public override TowerType TowerType => TowerType.Laser;

        public override Attack Shoot()
        {
            if (framesSinceLastShot >= fireRate && target != null)
            {
                framesSinceLastShot = 0;
                TurnToTarget();
                return new LaserAttack(target, this, damage, 1, 1);
            }
            return null;
        }
    }
}
