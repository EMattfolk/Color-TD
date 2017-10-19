using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD.Enemies
{
    class WaveCluster
    {
        private float waitTime;
        private int enemyCount;
        private EnemyType enemyType;
        private float spawnDelay;

        public WaveCluster(float waitTime, int enemyCount, EnemyType enemyType, float spawnDelay)
        {
            WaitTime = waitTime;
            EnemyCount = enemyCount;
            EnemyType = enemyType;
            SpawnDelay = spawnDelay;
        }

        public float WaitTime
        {
            get { return waitTime; }

            set  { waitTime = value; }
        }

        public int EnemyCount
        {
            get { return enemyCount; }

            set { enemyCount = value; }
        }

        public EnemyType EnemyType
        {
            get { return enemyType; }

            set { enemyType = value; }
        }

        public float SpawnDelay
        {
            get { return spawnDelay; }

            set { spawnDelay = value; }
        }
    }
}
