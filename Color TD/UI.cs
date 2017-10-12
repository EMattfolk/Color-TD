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
        private Bitmap boltButton, coinImage, heartImage, backgroundImage;
        private List<UIElement> uiElements;
        private int xPos;

        public UI (int xPos)
        {
            this.xPos = xPos;
            boltButton = new Bitmap("..\\..\\Button_Bolt.png");
            coinImage = new Bitmap("..\\..\\Coin.png");
            heartImage = new Bitmap("..\\..\\Heart.png");
            backgroundImage = new Bitmap("..\\..\\UI_Background.png");
            uiElements = new List<UIElement>() {
                new UIElement(coinImage, xPos + 1, 3, false),
                new UIElement(heartImage, xPos + 1, 20, false),
                new UIElement(boltButton, xPos + 43, 200, true),
                new UIElement("PLAYERCOINS", 16, xPos + 16, 0),
                new UIElement("PLAYERLIFE", 16, xPos + 16, 17) };
        }

        public Bitmap BackgroundImage => backgroundImage;

        public List<UIElement> UIElements => uiElements;

        public int XPos => xPos;
    }
}
