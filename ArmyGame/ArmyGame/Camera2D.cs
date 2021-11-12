using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ArmyGame
{
    class Camera2D
    {
        // Camera position and size
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
                cameraRect.X = (int)Math.Round(position.X);
                cameraRect.Y = (int)Math.Round(position.Y);
            }
        }
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
                cameraRect.Width = width;
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
                cameraRect.Height = height;
            }
        }
        
        // Camera rectangle used for rendering
        private Rectangle cameraRect;
        public Rectangle CameraRect { get { return cameraRect; } }

        // Level dimensions
        private int levelWidth;
        private int levelHeight;


        public Camera2D(Vector2 position, int screenWidth, int screenHeight, int levelWidth, int levelHeight)
        {
            Position = position;
            Width = screenWidth;
            Height = screenHeight;
            cameraRect = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), screenWidth, screenHeight);
            this.levelWidth = levelWidth;
            this.levelHeight = levelHeight;
        }

        public void Update(Vector2 position)
        {
            // Set camera position
            Position = position;

            // Keep camera in bounds
            if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }
            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, 0);
            }
            if (Position.X > levelWidth - Width)
            {
                Position = new Vector2(levelWidth - Width, Position.Y);
            }
            if (Position.Y > levelHeight - Height)
            {
                Position = new Vector2(Position.X, levelHeight - Height);
            }
        }
    }
}
