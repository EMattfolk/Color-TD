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
        public LaserTower () : this(new Point()) { }

        public LaserTower (Point position) : base(position, .5f, 0, 1/2f, 10, 100, 100) { } //TODO: change attack to make it more effective

        public override TowerType TowerType => TowerType.Laser;

        public override Attack Shoot()
        {
            if (timeSinceLastShot >= fireDelay && target != null)
            {
                timeSinceLastShot = 0;
                TurnToTarget();
                return new LaserAttack(target, this, damage, 1, 1);
            }
            return null;
        }
    }
}
