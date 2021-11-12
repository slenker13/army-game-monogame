using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyGame
{
    class Wall : Entity
    {
        public Wall(GraphicsDevice graphicsDevice, Vector2 position, int width, int height, Color color) : base(position, width, height)
        {
            Texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++) data[i] = color;
            Texture.SetData(data);
        }
    }
}
