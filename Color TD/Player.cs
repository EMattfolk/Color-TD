using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class Player
    {
        private int coins, lives;

        public Player ()
        {
            coins = 1000;
            lives = 100;
        }

        public int Coins
        {
            get { return coins; }

            set { coins = value; }
        }

        public int Lives => lives;
    }
}
