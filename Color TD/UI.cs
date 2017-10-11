using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class UI
    {
        private Bitmap coinImage, backgroundImage;
        private List<UIElement> uiElements;
        private int xPos;

        public UI (int xPos)
        {
            this.xPos = xPos;
            coinImage = new Bitmap("..\\..\\Coin.png");
            backgroundImage = new Bitmap("..\\..\\UI_Background.png");
            uiElements = new List<UIElement>();
        }

        public Bitmap CoinImage => coinImage;

        public Bitmap BackgroundImage => backgroundImage;

        public List<UIElement> UIElements => uiElements;
    }
}
