using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ArmyGame
{
    class EnemySpawner
    {
        // Texture used when creating enemies
        public Texture2D EnemyTexture { get; set; }

        private double previousSpawnTime;

        private Random rand;


        public EnemySpawner()
        {
            previousSpawnTime = 0;
            rand = new Random();
        }

        public EnemySpawner(Texture2D enemyTexture)
        {
            EnemyTexture = enemyTexture;
            previousSpawnTime = 0;
            rand = new Random();
        }

        // Spawn enemy on timer
        public void Update(GameTime gameTime, List<Entity> entityList, int levelWidth, int levelHeight)
        {
            double spawnInterval;

            double currentTime = gameTime.TotalGameTime.TotalSeconds;
            double timeSinceSpawn = currentTime - previousSpawnTime;

            // Increase spawn rate as time goes on
            if (currentTime < 30)
            {
                spawnInterval = 3;  // 3 seconds
            }
            else if (currentTime < 60)
            {
                spawnInterval = 1;  // 1 second
            }
            else
            {
                spawnInterval = 0.5;    // .5 seconds
            }

            // Spawn
            if (timeSinceSpawn > spawnInterval)
            {
                SpawnEnemy(entityList, levelWidth, levelHeight);
                previousSpawnTime = currentTime;
            }
        }

        public void SpawnEnemy(List<Entity> entityList, int levelWidth, int levelHeight)
        {
            // Randomize location
            int width = levelWidth - 10 - 60;
            int height = levelHeight - 10 - 60;
            int border = width * 2 + height * 2;
            int location = rand.Next(border);

            // Calculate position
            Vector2 enemyPosition;
            if (location < width)
            {
                enemyPosition = new Vector2(location + 5, 5);
            }
            else if (location >= width && location < width + height)
            {
                enemyPosition = new Vector2(levelWidth - 5 - 60, location - width + 5);
            }
            else if (location >= width + height && location < width + height + width)
            {
                enemyPosition = new Vector2(location - height - width + 5, levelHeight - 5 - 60);
            }
            else
            {
                enemyPosition = new Vector2(5, location - width - height - width + 5);
            }

            // Create enemy
            entityList.Add(new Enemy(enemyPosition, EnemyTexture));
        }
    }
}
