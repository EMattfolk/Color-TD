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
        private Bitmap image;
        private bool isClickable;
        private string text;
        private int xPos, yPos, width, height, textSize;

        public UIElement (Bitmap image, int xPos, int yPos, bool isClickable) : this(xPos, yPos, isClickable)
        {
            this.image = image;
            width = image.Width;
            height = image.Height;
            text = "";
            textSize = 0;
        }

        public UIElement (string text, int textSize, int xPos, int yPos) : this(xPos, yPos, false)
        {
            image = null;
            width = 0;
            height = 0;
            this.text = text;
            this.textSize = textSize;
        }

        private UIElement (int xPos, int yPos, bool isClickable)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.isClickable = isClickable;
        }

        public Bitmap Image => image;

        public string Text => text;

        public int Height => height;

        public int TextSize => textSize;

        public int Width => width;

        public int XPos => xPos;

        public int YPos => yPos;

        public bool WasClicked (Point mousePosition)
        {
            return isClickable && xPos <= mousePosition.X && mousePosition.X <= xPos + width && yPos <= mousePosition.Y && mousePosition.Y <= yPos + height;
        }
    }
}
