using System;
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

        public abstract EnemyType EnemyType { get; }
        protected int speed, hp, regeneration;
        private float distance;

        public Dot (int speed, float scale, int hp, int regeneration, float distance)
        {
            this.speed = speed;
            this.hp = hp;
            this.regeneration = regeneration;
            this.distance = distance;
            Size = 64;
            Scale = scale;
            Position = new PointF();
        }

        public float UpdateDistance(float deltaTime)
        {
            distance += deltaTime * speed;
            return distance;
        }

        public void ApplyDamage (int damage)
        {
            hp -= damage;
        }

        public void Kill ()
        {
            hp = 0;
        }

        public bool IsAlive => hp > 0;

        public override Bitmap GetImage() => images[(int)EnemyType];
    }
}
