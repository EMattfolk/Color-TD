using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class Wave
    {
        private int currentCluster;
        private List<WaveCluster> clusters;

        public Wave (string waveString)
        {
            clusters = new List<WaveCluster>();
            int i = 0;
            while (waveString.Contains(","))
            {
                i = waveString.IndexOf(',');
                clusters.Add(GetCluster(waveString.Substring(0,i)));
                waveString = waveString.Substring(i + 1);
            }
            clusters.Add(GetCluster(waveString));
        }

        public WaveCluster GetNextCluster ()
        {
            return clusters[currentCluster++];
        }

        public bool IsDone => currentCluster >= clusters.Count;

        private WaveCluster GetCluster (string data)
        {
            float waitTime;
            int enemyCount;
            EnemyType enemyType;
            float spawnDelay;

            int i;
            string temp;

            data = data.Trim();
            i = data.IndexOf(' ');
            temp = data.Substring(0, i);
            waitTime = float.Parse(temp, CultureInfo.InvariantCulture);
            data = data.Substring(i + 1);

            i = data.IndexOf(' ');
            temp = data.Substring(0, i);
            enemyCount = int.Parse(temp);
            data = data.Substring(i + 1);

            i = data.IndexOf(' ');
            temp = data.Substring(0, i);
            enemyType = EnemyTypeFromString(temp);
            data = data.Substring(i + 1);

            spawnDelay = float.Parse(data, CultureInfo.InvariantCulture);

            return new WaveCluster(waitTime, enemyCount, enemyType, spawnDelay);
        }

        private EnemyType EnemyTypeFromString (string s)
        {
            switch (s.ToLower())
            {
                case "black":
                    return EnemyType.BlackDot;
                case "blue":
                    return EnemyType.BlueDot;
                case "purple":
                    return EnemyType.PurpleDot;
                case "green":
                    return EnemyType.GreenDot;
                case "red":
                    return EnemyType.RedDot;
                case "yellow":
                    return EnemyType.YellowDot;
                case "cyan":
                    return EnemyType.CyanDot;
                case "white":
                    return EnemyType.WhiteDot;
                default:
                    return EnemyType.BlackDot;
            }
        }
    }
}
