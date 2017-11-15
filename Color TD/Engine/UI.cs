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
        public static readonly int Coin = 0, Heart = 1, LaserButton = 2, BoltButton = 3, StartButton = 4;
        private List<UIElement> uiElements, standardLayout, towerInfoLayout;
        private int xPos;

        public UI (int xPos)
        {
            this.xPos = xPos;
            standardLayout = new List<UIElement>() {
                new UIElement(Coin, new Vector2(xPos + 1, 3), 16, 16, false, TowerType.None),
                new UIElement(Heart,  new Vector2(xPos + 1, 20), 16, 16, false, TowerType.None),
                new UIElement("PLAYERCOINS",  16,  new Vector2(xPos + 20, 0)),
                new UIElement("PLAYERLIFE",  16,  new Vector2(xPos + 20, 17)),
                new UIElement(LaserButton,  new Vector2(xPos + 43, 100), 64, 96, true, TowerType.Laser),
                new UIElement(BoltButton,  new Vector2(xPos + 43, 300), 64, 96, true, TowerType.Bolt),
                new UIElement(StartButton,  new Vector2(xPos, 416), 150, 64, true, TowerType.None)
            };
            towerInfoLayout = new List<UIElement>() {
                new UIElement(Coin, new Vector2(xPos + 1, 3), 16, 16, false, TowerType.None),
                new UIElement(Heart,  new Vector2(xPos + 1, 20), 16, 16, false, TowerType.None),
                new UIElement("PLAYERCOINS",  16,  new Vector2(xPos + 20, 0)),
                new UIElement("PLAYERLIFE",  16,  new Vector2(xPos + 20, 17)),
                new UIElement("TOWERINFO",  16,  new Vector2(xPos, 50)),
                new UIElement(StartButton,  new Vector2(xPos, 416), 150, 64, true, TowerType.None)
            };
            uiElements = standardLayout;
        }

        public void SetLayout (string layout)
        {
            layout.ToLower();
            switch (layout)
            {
                case "standard":
                    uiElements = standardLayout;
                    break;
                case "towerinfo":
                    uiElements = towerInfoLayout;
                    break;
                default:
                    break;
            }
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
