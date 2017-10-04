﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    enum EnemyType
    {
        BlackDot
    }

    abstract class Dot : GameObject
    {
        private static Bitmap[] images = { new Bitmap("..\\..\\Black_dot.png") };

        private HashSet<long> hitById;
        protected int speed, hp, regeneration;
        private float distance;

        public Dot (int speed, float scale, int hp, int regeneration, float distance)
        {
            this.speed = speed;
            this.hp = hp;
            this.regeneration = regeneration;
            this.distance = distance;
            Size = 64;
            Width = 64;
            Height = 64;
            Scale = scale;
            Position = new PointF();
            hitById = new HashSet<long>();
        }

        public float UpdateDistance(float deltaTime)
        {
            distance += deltaTime * speed;
            return distance;
        }

        public void ApplyDamage (Attack attack)
        {
            hp -= attack.Damage;
            hitById.Add(attack.ID);
        }

        public void Kill ()
        {
            hp = 0;
        }

        public bool HasBeenHitByID (long id) => hitById.Contains(id);

        public bool IsAlive => hp > 0;

        public abstract EnemyType EnemyType { get; }

        public override Bitmap GetImage() => images[(int)EnemyType];
    }
}
