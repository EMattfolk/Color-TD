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
        public static readonly int Coin = 0, Heart = 1, LaserButton = 2, BoltButton = 3, StartButton = 4, UpgradeButton = 5, SellButton = 6, GameOver = 7;
        private List<UIElement> uiElements, standardLayout, towerInfoLayout, enemyInfoLayout;
        private int xPos;

        public UI (int xPos)
        {
            this.xPos = xPos;
            standardLayout = new List<UIElement>() {
                new UIElement(Coin, new Vector2(xPos + 1, 3), 16, 16, false, TowerType.None),
                new UIElement(Heart,  new Vector2(xPos + 1, 20), 16, 16, false, TowerType.None),
                new UIElement("PLAYERCOINS",  0,  new Vector2(xPos + 20, 0)),
                new UIElement("PLAYERLIFE",  0,  new Vector2(xPos + 20, 17)),
                new UIElement(LaserButton,  new Vector2(xPos + 43, 100), 64, 96, true, TowerType.Laser),
                new UIElement(BoltButton,  new Vector2(xPos + 43, 250), 64, 96, true, TowerType.Bolt),
                new UIElement(StartButton,  new Vector2(xPos, 416), 150, 64, true, TowerType.None)
            };
            towerInfoLayout = new List<UIElement>() {
                new UIElement(Coin, new Vector2(xPos + 1, 3), 16, 16, false, TowerType.None),
                new UIElement(Heart,  new Vector2(xPos + 1, 20), 16, 16, false, TowerType.None),
                new UIElement("PLAYERCOINS",  0,  new Vector2(xPos + 20, 0)),
                new UIElement("PLAYERLIFE",  0,  new Vector2(xPos + 20, 17)),
                new UIElement("TOWERINFO",  1,  new Vector2(xPos + 5, 50)),
                new UIElement(SellButton,  new Vector2(xPos, 288), 150, 64, true, TowerType.None),
                new UIElement("TOWERSELLVALUE",  2,  new Vector2(xPos + 55, 321)),
                new UIElement(UpgradeButton,  new Vector2(xPos, 352), 150, 64, true, TowerType.None),
                new UIElement("TOWERUPGRADECOST",  2,  new Vector2(xPos + 55, 385)),
                new UIElement(StartButton,  new Vector2(xPos, 416), 150, 64, true, TowerType.None)
            };
            enemyInfoLayout = new List<UIElement>() {
                new UIElement(Coin, new Vector2(xPos + 1, 3), 16, 16, false, TowerType.None),
                new UIElement(Heart,  new Vector2(xPos + 1, 20), 16, 16, false, TowerType.None),
                new UIElement("PLAYERCOINS",  0,  new Vector2(xPos + 20, 0)),
                new UIElement("PLAYERLIFE",  0,  new Vector2(xPos + 20, 17)),
                new UIElement("ENEMYINFO",  1,  new Vector2(xPos + 5, 50)),
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
                case "enemyinfo":
                    uiElements = enemyInfoLayout;
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
