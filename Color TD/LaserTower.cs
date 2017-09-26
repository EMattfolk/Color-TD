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
        public LaserTower (Point position) : base(position, 32, 0, 30, 10, 100) { }

        public override Bitmap GetImage ()
        {
            return Images[0];
        }

        public override void Shoot()
        {
            if (framesSinceLastShot >= fireRate && target != null)
            {
                framesSinceLastShot = 0;
                TurnToTarget();
            }
        }
    }
}
