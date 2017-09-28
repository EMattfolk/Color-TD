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

    abstract class Dot
    {
        private static Bitmap[] images = { new Bitmap("..\\..\\Black_dot.png") }; 

        public abstract EnemyType EnemyType { get; }
        protected int speed, size, hp, regeneration;
        private float distance;
        private PointF position;

        public Dot (int speed, int size, int hp, int regeneration, float distance)
        {
            this.speed = speed;
            this.size = size;
            this.hp = hp;
            this.regeneration = regeneration;
            this.distance = distance;
            position = new PointF();
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

        public int Size => size;

        public Bitmap GetImage() => images[(int)EnemyType];

        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
