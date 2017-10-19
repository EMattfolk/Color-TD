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

        public WaveSpawner ()
        {
            queuedEnemies = new List<Dot>();
            currentCluster = null;
            currentWave = 0;
            spawnDelay = .01f;
            time = 0;
            waveStrings = new string[] {
                "5 10 Black 0.2 , 1 10 Blue 0.5"
            };
            waves = new List<Wave>() {
                new Wave(waveStrings[0])
            };
        }

        public void Update (GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentCluster != null || !waves[currentWave].IsDone)
            {
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
            else if (waves[currentWave].IsDone && currentWave < waves.Count - 1) currentWave++;
        }

        public List<Dot> QueuedEnemies => queuedEnemies;
    }
}
