using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyGame
{
    class Enemy : Actor
    {
        public float EnemyVelocity { get; set; } = 200.0f;


        public Enemy() : base() { }

        public Enemy(Vector2 position) : base(position) { }

        public Enemy(Vector2 position, Texture2D texture) : base(position, texture) { }

        public void Update(GameTime gameTime, int levelWidth, int levelHeight, List<Entity> entityList, BulletManager bulletManager)
        {
            // Get player center coordinates
            Vector2 playerCenter = entityList[0].GetCenter();

            // Get enemy center coordinates
            Vector2 enemyCenter = GetCenter();

            // Get angle to player and calculate velocity
            float angle = MathF.Atan2(playerCenter.Y - enemyCenter.Y, playerCenter.X - enemyCenter.X);
            Velocity = new Vector2(
                EnemyVelocity * MathF.Cos(angle) * (float)gameTime.ElapsedGameTime.TotalSeconds, 
                EnemyVelocity * MathF.Sin(angle) * (float)gameTime.ElapsedGameTime.TotalSeconds
                );

            // Move left or right
            Position = new Vector2(Position.X + Velocity.X, Position.Y);
            
            // Keep in bounds
            if (Position.X < 0 || Position.X + Width > levelWidth)
            {
                Position = new Vector2(Position.X - Velocity.X, Position.Y);
            }

            // Check collision
            foreach (Entity other in entityList)
            {
                if (other != this && CheckCollision(other))
                {
                    switch (other)
                    {
                        case Player p:
                            p.Remove = true;
                            break;
                        case Wall w:
                            Position = new Vector2(Position.X - Velocity.X, Position.Y);
                            break;
                        case Enemy e:
                            Position = new Vector2(Position.X - Velocity.X, Position.Y);
                            break;
                    }
                }
            }

            // Move up or down
            Position = new Vector2(Position.X, Position.Y + Velocity.Y);

            // Keep in bounds
            if (Position.Y < 0 || Position.Y + Width > levelHeight)
            {
                Position = new Vector2(Position.X, Position.Y - Velocity.Y);
            }

            // Check collision
            foreach (Entity other in entityList)
            {
                if (other != this && CheckCollision(other))
                {
                    switch (other)
                    {
                        case Player p:
                            p.Remove = true;
                            break;
                        case Wall w:
                            Position = new Vector2(Position.X, Position.Y - Velocity.Y);
                            break;
                        case Enemy e:
                            Position = new Vector2(Position.X, Position.Y - Velocity.Y);
                            break;
                    }
                }
            }

            // Check for bullet hits
            foreach (Bullet bullet in bulletManager.BulletList)
            {
                if (CheckCollision(bullet))
                {
                    Remove = true;
                }
            }
        }
    }
}
