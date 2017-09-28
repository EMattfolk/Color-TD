﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    enum AttackType
    {
        Laser,
        Bolt
    }

    abstract class Attack
    {
        protected Dot target;
        protected Tower shooter;
        protected int damage, aliveTime, hitsLeft;

        public Attack (Dot target, Tower shooter, int damage, int aliveTime, int maxHitCount)
        {
            this.target = target;
            this.shooter = shooter;
            this.damage = damage;
            this.aliveTime = aliveTime;
            hitsLeft = maxHitCount;
        }

        abstract public void Update (float deltaTime);

        abstract public AttackType AttackType { get; }

        abstract public PointF Position { get; }

        abstract public Bitmap GetImage ();

        abstract public int Size { get; }

        abstract public float Rotation { get; }

        public void Kill ()
        {
            aliveTime = 0;
            hitsLeft = 0;
        }

        public double DistanceTo(PointF other)
        {
            float x = Position.X - other.X, y = Position.Y - other.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public bool IsAlive => aliveTime > 0 && hitsLeft > 0;

        public int Damage => damage;

        public Dot Target => target;

        public Tower Shooter => shooter;
    }
}
