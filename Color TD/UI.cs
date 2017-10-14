using Microsoft.Xna.Framework;
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
        private int coin, heart, boltButton, laserButton;
        private List<UIElement> uiElements;
        private int xPos;

        public UI (int xPos)
        {
            this.xPos = xPos;
            coin = 0;
            heart = 1;
            laserButton = 2;
            boltButton = 3;
            uiElements = new List<UIElement>() {
                new UIElement(coin, new Vector2(xPos + 1, 3), 16, 16, false, TowerType.None),
                new UIElement(heart,  new Vector2(xPos + 1, 20), 16, 16, false, TowerType.None),
                new UIElement(laserButton,  new Vector2(xPos + 43, 100), 64, 96, true, TowerType.Laser),
                new UIElement(boltButton,  new Vector2(xPos + 43, 300), 64, 96, true, TowerType.Bolt),
                new UIElement("PLAYERCOINS",  16,  new Vector2(xPos + 20, 0)),
                new UIElement("PLAYERLIFE",  16,  new Vector2(xPos + 20, 17)) };
        }

        public UIElement GetElementAt (Vector2 p)
        {
            foreach (UIElement element in uiElements)
            {
                if (element.WasClicked(p)) return element;
            }
            return null;
        }

        public List<UIElement> UIElements => uiElements;

        public int XPos => xPos;
    }
}
