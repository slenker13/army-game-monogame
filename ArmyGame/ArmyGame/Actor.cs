using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyGame
{
    // Actor object, encapsulates moving entities like the player and enemies
    abstract class Actor : Entity
    {
        // Actor velocity
        public Vector2 Velocity { get; set; }


        // Constructors
        public Actor() : base()
        {
            Velocity = new Vector2(0.0f, 0.0f);
        }
        public Actor(Vector2 position) : base(position)
        {
            Velocity = new Vector2(0.0f, 0.0f);
        }
        public Actor(Vector2 position, Texture2D texture) : base(position, texture)
        {
            Velocity = new Vector2(0.0f, 0.0f);
        }

        // Move the actor
        public virtual void Move(int levelWidth, int levelHeight)
        {
            // Move left or right
            Position = new Vector2(Position.X + Velocity.X, Position.Y);

            // Keep in bounds
            if (Position.X < 0 || Position.X + Width > levelWidth)
            {
                Position = new Vector2(Position.X - Velocity.X, Position.Y);
            }
            // Check collision

            // Move up or down
            Position = new Vector2(Position.X, Position.Y + Velocity.Y);

            // Keep in bounds
            if (Position.Y < 0 || Position.Y + Height > levelHeight)
            {
                Position = new Vector2(Position.X, Position.Y - Velocity.Y);
            }

            // Check collision
        }
    }
}
