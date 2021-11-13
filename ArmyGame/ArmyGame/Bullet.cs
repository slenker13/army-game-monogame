using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyGame
{
    class Bullet : Entity
    {
        // Constant velocity of a bullet
        public const float BULLET_VELOCITY = 1000f;

        // Velocity calculated from the angle
        private Vector2 baseVelocity;


        public Bullet(Vector2 position, Texture2D texture, float angle) : base(position, texture)
        {
            // Calculate velocity
            float velX = (float)(BULLET_VELOCITY * Math.Cos(angle));
            float velY = (float)(BULLET_VELOCITY * Math.Sin(angle));
            baseVelocity = new Vector2(velX, velY);
        }

        public void Update(GameTime gameTime, int levelWidth, int levelHeight, List<Entity> entityList)
        {
            // Move
            Vector2 velocity = baseVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position = new Vector2(Position.X + velocity.X, Position.Y + velocity.Y);

            // Check for out of bounds
            if (Position.X < 0 || Position.X + Width > levelWidth || Position.Y < 0 || Position.Y + Height > levelHeight)
            {
                Remove = true;
            }
            
            // Check for collisions
            foreach (Entity other in entityList)
            {
                if (other != this && other.GetType() != typeof(Player) && CheckCollision(other))
                {
                    Remove = true;
                }
            }
        }
    }
}
