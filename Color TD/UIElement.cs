﻿using Microsoft.Xna.Framework;
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
        private bool isClickable;
        private string text;
        private int spriteIndex, xPos, yPos, width, height, textSize;

        public UIElement (int spriteIndex, int xPos, int yPos, bool isClickable, TowerType tower) : this(xPos, yPos, isClickable)
        {
            this.spriteIndex = spriteIndex;
            //TODO
            //width = image.Width;
            //height = image.Height;
            text = "";
            textSize = 0;
            this.tower = tower;
        }

        public UIElement (string text, int textSize, int xPos, int yPos) : this(xPos, yPos, false)
        {
            spriteIndex = 0;
            width = 0;
            height = 0;
            this.text = text;
            this.textSize = textSize;
            tower = TowerType.None;
        }

        private UIElement (int xPos, int yPos, bool isClickable)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.isClickable = isClickable;
        }

        public int SpriteIndex => spriteIndex;

        public TowerType HeldTowerType => tower;

        public string Text => text;

        public int Height => height;

        public int TextSize => textSize;

        public int Width => width;

        public int XPos => xPos;

        public int YPos => yPos;

        public bool WasClicked (Vector2 mousePosition)
        {
            return isClickable && xPos <= mousePosition.X && mousePosition.X <= xPos + width && yPos <= mousePosition.Y && mousePosition.Y <= yPos + height;
        }
    }
}
