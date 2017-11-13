using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color_TD.Enemies;

namespace Color_TD
{
    enum EnemyType
    {
        BlackDot,
        BlueDot,
        PurpleDot,
        GreenDot,
        RedDot,
        YellowDot,
        CyanDot,
        WhiteDot
    }

    abstract class Dot : GameObject
    {
        private static List<Texture2D> sprites = new List<Texture2D>(); 
        private HashSet<long> hitById;
        private int regeneration;
        private float speed, distance, hp, maxhp;
        private int worth;

        public Dot (int worth, int speed, float scale, float hp, int regeneration)
        {
            this.worth = worth;
            this.speed = speed;
            this.hp = hp;
            maxhp = hp;
            this.regeneration = regeneration;
            distance = 0;
            Size = 64;
            Width = 64;
            Height = 64;
            Scale = scale;
            hitById = new HashSet<long>();
        }

        public static Dot FromType (EnemyType type)
        {
            switch (type)
            {
                case EnemyType.BlackDot:
                    return new BlackDot();
                case EnemyType.BlueDot:
                    return new BlueDot();
                case EnemyType.PurpleDot:
                    return new PurpleDot();
                case EnemyType.GreenDot:
                    return new GreenDot();
                case EnemyType.RedDot:
                    return new RedDot();
                case EnemyType.YellowDot:
                    return new YellowDot();
                case EnemyType.CyanDot:
                    return new CyanDot();
                case EnemyType.WhiteDot:
                    return new WhiteDot();
                default:
                    return null;
            }
        } 

        public void UpdateDistance(GameTime gameTime)
        {
            distance += (float)(gameTime.ElapsedGameTime.TotalSeconds * speed);
            hp = MathHelper.Clamp(hp + (float)(gameTime.ElapsedGameTime.TotalSeconds * regeneration), 0, maxhp);
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

        public static List<Texture2D> Sprites => sprites;

        public bool HasBeenHitByID (long id) => hitById.Contains(id);

        public bool IsAlive => hp > 0;

        public int Worth => worth;

        public float Distance => distance;

        public abstract EnemyType EnemyType { get; }

        public override Texture2D GetSprite() => sprites[(int)EnemyType];
    }
}
