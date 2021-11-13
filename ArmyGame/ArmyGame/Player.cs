using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ArmyGame
{
    // The player object
    class Player : Actor
    {
        // Rotation angle
        public float Angle { get; set; }

        // Maximum player velocity
        public float PlayerVelocity { get; } = 300.0f;


        // Constructors
        public Player()
        {
            Angle = 0.0f;
        }
        public Player(Vector2 position) : base(position)
        {
            Angle = 0.0f;
        }
        public Player(Vector2 position, Texture2D texture) : base(position, texture)
        {
            Angle = 0.0f;
        }

        // Updates the player
        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, int levelWidth, int levelHeight, Vector2 cameraPosition, List<Entity> entityList)
        {
            CalculateVelocity(gameTime, keyboardState);
            CalculateAngle(mouseState, cameraPosition);

            Move(levelWidth, levelHeight, entityList);
        }

        private void CalculateVelocity(GameTime gameTime, KeyboardState keyboardState)
        {
            Vector2 newVelocity = Vector2.Zero;

            // Key pressed
            if (keyboardState.IsKeyDown(Keys.W)) newVelocity.Y = -1;
            if (keyboardState.IsKeyDown(Keys.A)) newVelocity.X = -1;
            if (keyboardState.IsKeyDown(Keys.S)) newVelocity.Y = 1;
            if (keyboardState.IsKeyDown(Keys.D)) newVelocity.X = 1;

            if (newVelocity != Vector2.Zero)
            {
                // Normalize vector so diagonal movement is not faster
                newVelocity.Normalize();
                newVelocity *= (float)(PlayerVelocity * gameTime.ElapsedGameTime.TotalSeconds);
            }

            Velocity = newVelocity;
        }

        private void CalculateAngle(MouseState mouseState, Vector2 cameraPosition)
        {
            float centerX = Position.X - cameraPosition.X + (Width / 2);
            float centerY = Position.Y - cameraPosition.Y + (Height / 2);
            Angle = (float)(Math.Atan2(mouseState.Position.Y - centerY, mouseState.Position.X - centerX) + Math.PI / 2.0);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            base.Draw(spriteBatch, cameraPosition, Angle);
        }
    }
}
