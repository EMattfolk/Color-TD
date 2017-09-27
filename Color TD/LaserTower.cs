﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class LaserTower : Tower
    {
        public LaserTower (Point position) : base(position, 32, 0, 30, 10, 100) { }

        public override Bitmap GetImage () => Images[0];

        public override Attack Shoot()
        {
            if (framesSinceLastShot >= fireRate && target != null)
            {
                Console.Write("Shot");
                framesSinceLastShot = 0;
                TurnToTarget();
                return new LaserAttack(target, this, damage, 1, 1);
            }
            return null;
        }
    }
}
