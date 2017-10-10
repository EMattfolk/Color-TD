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
        private Bitmap coinImage;

        public UI ()
        {
            coinImage = new Bitmap("..\\..\\Coin.png");
        }

        public Bitmap CoinImage => coinImage;
    }
}
