﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyGame
{
    // Basic entity object. Meant to be extended by other classes.
    abstract class Entity
    {
        // Entity position
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                collider.X = (int)Math.Round(value.X);
                collider.Y = (int)Math.Round(value.Y);
            }
        }

        // Entity dimensions
        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                collider.Width = value;
            }
        }
        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                collider.Height = value;
            }
        }

        // Collision box
        // Collider is automatically updated whenever the entity's position or dimensions are updated
        private Rectangle collider;
        public Rectangle Collider
        {
            get { return collider; }
        }

        // Entity texture
        private Texture2D texture;
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
                Width = value.Width;
                Height = value.Height;
            }
        }

        
        // Constructors
        public Entity()
        {
            collider = new Rectangle();
            Position = new Vector2(0, 0);
            Width = 0;
            Height = 0;
        }
        public Entity(Vector2 position)
        {
            collider = new Rectangle();
            Position = position;
            Width = 0;
            Height = 0;
        }
        public Entity(Vector2 position, int width, int height)
        {
            collider = new Rectangle();
            Position = position;
            Width = width;
            Height = height;
        }
        public Entity(Vector2 position, Texture2D texture)
        {
            collider = new Rectangle();
            Position = position;
            Texture = texture;
        }

        // Checks if the collider intersects the other entity's collider
        public bool CheckCollision(Entity other)
        {
            // TODO: implement
            return false;
        }

        // Render the entity to the screen
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition, float angle = 0f)
        {
            Vector2 adjPosition = new Vector2(Position.X - cameraPosition.X + Width / 2, Position.Y - cameraPosition.Y + Height / 2);
            spriteBatch.Draw(Texture, adjPosition, null, Color.White, angle, new Vector2(Width / 2, Height / 2), 1.0f, SpriteEffects.None, 0f);
        }
    }
}
