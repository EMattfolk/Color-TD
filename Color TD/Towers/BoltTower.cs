using Microsoft.Xna.Framework;
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
        public BoltTower() : this(new Vector2()) { }

        public BoltTower(Vector2 position) : base(position, .5f, 0, 1/4f, 10, 80, 200) { }

        public override TowerType TowerType => TowerType.Bolt;

        public override Attack Shoot()
        {
            if (timeSinceLastShot >= fireDelay && target != null)
            {
                timeSinceLastShot = 0;
                TurnToTarget();
                return new BoltAttack(this, damage, 2, 600, 0.5f);
            }
            return null;
        }
    }
}
