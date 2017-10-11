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
        }

        public UIElement (string text, int textSize, int xPos, int yPos) : this(xPos, yPos, false)
        {
            this.text = text;
            this.textSize = textSize;
        }

        private UIElement (int xPos, int yPos, bool isClickable)
        {
            this.isClickable = isClickable;
        }

        public bool WasClicked (Point mousePosition)
        {
            return isClickable && xPos <= mousePosition.X && mousePosition.X <= xPos + width && yPos <= mousePosition.Y && mousePosition.Y <= yPos + height;
        }
    }
}
