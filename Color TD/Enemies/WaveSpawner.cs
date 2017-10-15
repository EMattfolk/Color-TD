using Color_TD.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Color_TD
{
    class WaveSpawner
    {
        private List<Dot> queuedEnemies;
        private float spawnDelay, time;

        public WaveSpawner ()
        {
            queuedEnemies = new List<Dot>();
            spawnDelay = .1f;
            time = 0;
        }

        public void Update (GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > spawnDelay)
            {
                queuedEnemies.Add(new WhiteDot());
                time -= spawnDelay;
            }
        }

        public List<Dot> QueuedEnemies => queuedEnemies;
    }
}
