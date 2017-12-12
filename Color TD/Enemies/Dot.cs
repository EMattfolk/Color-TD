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
        WhiteDot,
        RainbowDot
    }

    abstract class Dot : GameObject
    {
        private static bool upgraded = false;
        private static List<Texture2D> sprites = new List<Texture2D>(); 
        private HashSet<long> hitById, lastHitById;
        private int regeneration, worth;
        private float speed, distance, hp, maxhp;

        public Dot (int worth, int speed, float scale, float hp, int regeneration)
        {
            int modifier = upgraded ? 2 : 1;
            this.worth = worth;
            this.speed = speed;
            this.hp = hp * modifier;
            maxhp = hp * modifier;
            this.regeneration = regeneration * modifier;
            distance = 0;
            Size = 64;
            Width = 64;
            Height = 64;
            Scale = scale * modifier;
            hitById = new HashSet<long>();
            lastHitById = new HashSet<long>();
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
                case EnemyType.RainbowDot:
                    return new RainbowDot();
                default:
                    return null;
            }
        } 

        public void UpdateDistance(GameTime gameTime)
        {
            distance += (float)(gameTime.ElapsedGameTime.TotalSeconds * speed);
            hp = MathHelper.Clamp(hp + (float)(gameTime.ElapsedGameTime.TotalSeconds * regeneration), 0, maxhp);
            lastHitById = hitById;
            hitById = new HashSet<long>();
        }

        public bool ApplyDamage (Attack attack)
        {
            if (HasBeenHitByID(attack.ID))
            {
                hitById.Add(attack.ID);
                return false;
            }
            hitById.Add(attack.ID);
            hp -= attack.Damage;
            return true;
        }

        public void Kill ()
        {
            hp = 0;
        }

        public string GetInfo ()
        {
            return "HP: " + ((int)hp).ToString() + "/" + ((int)maxhp).ToString() + Environment.NewLine + "Regen: " + regeneration.ToString() + Environment.NewLine + "Speed: " + speed.ToString();
        }

        public static List<Texture2D> Sprites => sprites;

        public static void Upgrade()
        {
            upgraded = true;
        }

        public bool HasBeenHitByID (long id) => lastHitById.Contains(id);

        public bool IsAlive => hp > 0;

        public int Worth => worth;

        public float Distance => distance;

        public abstract EnemyType EnemyType { get; }

        public override Texture2D GetSprite() => sprites[(int)EnemyType];
    }
}
