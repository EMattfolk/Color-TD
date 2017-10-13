﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class BoltTower : Tower
    {
        public BoltTower() : this(new Point()) { }

        public BoltTower(Point position) : base(position, .5f, 0, 1/4f, 10, 100, 200) { }

        public override TowerType TowerType => TowerType.Bolt;

        public override Attack Shoot()
        {
            if (timeSinceLastShot >= fireDelay && target != null)
            {
                timeSinceLastShot = 0;
                TurnToTarget();
                return new BoltAttack(this, damage, 2, 400, 0.5f);
            }
            return null;
        }
    }
}
