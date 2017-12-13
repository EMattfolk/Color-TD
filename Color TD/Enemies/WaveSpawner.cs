using Color_TD.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Spawner syntax
 * waittime enemycount enemytype spawndelay , waittime enemycount enemytype spawndelay ...
 * 
 * Wavecomp
 * hp/spawntime > dps to lose
 * 
 * Tower DPS
 * 20 dps for around 50 coins
 * 0.4 dps/coin
 */

namespace Color_TD
{
    class WaveSpawner
    {
        private List<Wave> waves;
        private List<Dot> queuedEnemies;
        private WaveCluster currentCluster;
        private int currentWave;
        private float time;
        private string[] waveStrings;
        private bool isIdle;

        public WaveSpawner ()
        {
            queuedEnemies = new List<Dot>();
            currentCluster = null;
            currentWave = -1;
            time = 0;
            isIdle = true;
            waveStrings = new string[] {
                "0 10 Black 0.2",
                "0 20 Black 0.2",
                "0 10 Blue 0.2",
                "0 20 Blue 0.2",
                "0 10 Purple 0.3",
                "0 30 Blue 0.15",
                "0 20 Purple 0.2",
                "0 20 Black 0.15, 2 20 Blue 0.15, 2 20 Purple 0.2",
                "0 20 Green 0.2",
                "0 60 Blue 0.15",
                "0 30 Purple 0.2",
                "0 20 Black 0.1, 0.5 20 Blue 0.1, 0.5 20 Purple 0.1, 0.5 20 Green 0.1",
                "0 1 Red 0",
                "0 10 Red 0.3",
                "0 30 Green 0.1",
                "0 25 Black 0.05, 0.5 25 Blue 0.1, 0.5 25 Purple 0.1, 0.5 25 Green 0.1, 0.5 10 Red 0.3",
                "0 1 Yellow 0",
                "0 10 Yellow 0.25",
                "0 100 Purple 0.1",
                "0 20 Yellow 0.05",
                "0 5 Cyan 0.4, 0.5 1 Red 0, 0.5 1 Green 0, 0.5 1 Purple 0, 0.5 1 Blue 0, 0.5 1 Black 0",
                "0 10 Yellow 0.04, 3 10 Yellow 0.04",
                "0 10 Cyan 0.2",
                "0 20 Red 0.1",
                "0 3 Cyan 0.03, 2 3 Cyan 0.03, 2 3 Cyan 0.03, 2 3 Cyan 0.03",
                "0 3 Cyan 0.2, 0 3 Yellow 0.2, 0 3 Cyan 0.2, 0 3 Yellow 0.2, 0 3 Cyan 0.2, 0 3 Yellow 0.2, 0 3 Cyan 0.2, 0 3 Yellow 0.2",
                "0 1 White 0",
                "0 10 Cyan 0.1",
                "0 5 White 0.3", // Total coins: 3630 DPS: 1452
                "0 10 White 0.3", // Total coins: 3930 DPS: 1572
                "0 1 Rainbow 0", // Total coins: 4430 DPS: 1772
                "0 200 Black 0.02, 1 100 Blue 0.02",
                "0 200 Blue 0.02, 1 100 Purple 0.02",
                "0 200 Purple 0.03, 1 100 Green 0.03",
                "0 10 Yellow 0.2",
                "0 200 Green 0.02, 1 50 Red 0.03",
                "0 100 Red 0.03",
                "0 3 White 0.05",
                "0 100 Yellow 0.1",
                "0 50 Cyan 0.2"
            };
            waves = new List<Wave>();
            foreach (string str in waveStrings)
            {
                waves.Add(new Wave(str));
            }
        }

        public void Update (GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            isIdle = true;
            if (currentCluster != null || !waves[currentWave].IsDone)
            {
                isIdle = false;
                if (currentCluster == null)
                {
                    currentCluster = waves[currentWave].GetNextCluster();
                }
                while (time > currentCluster.WaitTime && currentCluster.EnemyCount > 0)
                {
                    queuedEnemies.Add(Dot.FromType(currentCluster.EnemyType));
                    currentCluster.EnemyCount--;
                    time -= currentCluster.WaitTime;
                    currentCluster.WaitTime = currentCluster.EnemyCount > 0 ? currentCluster.SpawnDelay : 0;
                }
                if (currentCluster.EnemyCount == 0)
                {
                    currentCluster = null;
                }
            }
        }

        public void SpawnNextWave ()
        {
            if (isIdle && currentWave + 1 < waves.Count)
            {
                currentWave++;
                isIdle = false;
                time = 0;
            }
        }

        public List<Dot> QueuedEnemies => queuedEnemies;

        public bool IsIdle => isIdle;

        public bool IsEmpty => waves[waves.Count - 1].IsDone;
    }
}
