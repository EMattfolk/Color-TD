using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    enum UIElementType
    {
        Image,
        Text
    }

    class UIElement : GameObject
    {
        private static List<Texture2D> sprites = new List<Texture2D>();
        private TowerType tower;
        private UIElementType type;
        private bool isClickable;
        private string text;
        private int spriteIndex, textSize;

        public UIElement (int spriteIndex, Vector2 position, int width, int height, bool isClickable, TowerType tower) : this(position, isClickable)
        {
            this.spriteIndex = spriteIndex;
            Width = width;
            Height = height;
            text = "";
            textSize = 0;
            this.tower = tower;
            type = UIElementType.Image;
        }

        public UIElement (string text, int textSize, Vector2 position) : this(position, false)
        {
            spriteIndex = -1;
            Width = 0;
            Height = 0;
            this.text = text;
            this.textSize = textSize;
            tower = TowerType.None;
            type = UIElementType.Text;
        }

        private UIElement(Vector2 position, bool isClickable)
        {
            Position = position;
            this.isClickable = isClickable;
        }

        public static List<Texture2D> Sprites => sprites;

        public override Texture2D GetSprite() => sprites[spriteIndex];

        public TowerType HeldTowerType => tower;

        public UIElementType Type => type;

        public string Text => text;

        public int TextSize => textSize;

        public int SpriteIndex => spriteIndex;

        public bool WasClicked (Vector2 mousePosition)
        {
            return isClickable && Position.X <= mousePosition.X && mousePosition.X <= Position.X + Width && Position.Y <= mousePosition.Y && mousePosition.Y <= Position.Y + Height;
        }
    }
}
