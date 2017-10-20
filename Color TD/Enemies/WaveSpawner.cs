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
 */

namespace Color_TD
{
    class WaveSpawner
    {
        private List<Wave> waves;
        private List<Dot> queuedEnemies;
        private WaveCluster currentCluster;
        private int currentWave;
        private float spawnDelay, time;
        private string[] waveStrings;
        private bool isIdle;

        public WaveSpawner ()
        {
            queuedEnemies = new List<Dot>();
            currentCluster = null;
            currentWave = -1;
            spawnDelay = .01f;
            time = 0;
            isIdle = true;
            waveStrings = new string[] {
                "0 10 Black 0.2",
                "0 20 Black 0.1",
                "0 10 Blue 0.2",
                "0 20 Blue 0.2",
                "0 10 Purple 0.3",
                "0 30 Blue 0.1",
                "0 20 Purple 0.2",
                "0 20 Green 0.2",
                "0 20 Black 0.1, 0 20 Blue 0.1, 0 20 Purple 0.1, 0 20 Green 0.1"
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
    }
}
