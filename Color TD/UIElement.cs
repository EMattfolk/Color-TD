using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class UIElement
    {
        private TowerType tower;
        private Vector2 position;
        private bool isClickable;
        private string text;
        private int spriteIndex, width, height, textSize;

        public UIElement (int spriteIndex, Vector2 position, int width, int height, bool isClickable, TowerType tower) : this(position, isClickable)
        {
            this.spriteIndex = spriteIndex;
            this.width = width;
            this.height = height;
            text = "";
            textSize = 0;
            this.tower = tower;
        }

        public UIElement (string text, int textSize, Vector2 position) : this(position, false)
        {
            spriteIndex = -1;
            width = 0;
            height = 0;
            this.text = text;
            this.textSize = textSize;
            tower = TowerType.None;
        }

        private UIElement(Vector2 position, bool isClickable)
        {
            this.position = position;
            this.isClickable = isClickable;
        }

        public int SpriteIndex => spriteIndex;

        public TowerType HeldTowerType => tower;

        public string Text => text;

        public int Height => height;

        public int TextSize => textSize;

        public int Width => width;

        public Vector2 Position => position;

        public bool WasClicked (Vector2 mousePosition)
        {
            return isClickable && position.X <= mousePosition.X && mousePosition.X <= position.X + width && position.Y <= mousePosition.Y && mousePosition.Y <= position.Y + height;
        }
    }
}
