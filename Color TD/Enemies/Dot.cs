using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Color_TD
{
    enum EnemyType
    {
        BlackDot
    }

    abstract class Dot : GameObject
    {
        private HashSet<long> hitById;
        protected int hp, regeneration;
        private float speed, distance;
        private int worth;

        public Dot (int worth, int speed, float scale, int hp, int regeneration)
        {
            this.worth = worth;
            this.speed = speed;
            this.hp = hp;
            this.regeneration = regeneration;
            distance = 0;
            Size = 64;
            Width = 64;
            Height = 64;
            Scale = scale;
            hitById = new HashSet<long>();
        }

        public void UpdateDistance(GameTime gameTime)
        {
            distance += (float)(gameTime.ElapsedGameTime.TotalSeconds * speed);
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

        public int Worth => worth;

        public float Distance => distance;

        public abstract EnemyType EnemyType { get; }

        public override int GetSpriteIndex() => (int)EnemyType;
    }
}
