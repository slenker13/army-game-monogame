using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArmyGame
{
    class BulletManager
    {
        // List of bullets
        public List<Bullet> BulletList { get; }

        // Texture used for all bullets
        private Texture2D bulletTexture;


        public BulletManager(GraphicsDevice graphicsDevice, int size)
        {
            // Create texture
            bulletTexture = new Texture2D(graphicsDevice, size, size);
            Color[] data = new Color[size * size];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Black;
            bulletTexture.SetData(data);

            // Init list
            BulletList = new List<Bullet>();
        }

        // Create new bullet and add it to the list
        public Bullet AddBullet(Vector2 position, float angle)
        {
            Bullet bullet = new Bullet(position, bulletTexture, angle);
            BulletList.Add(bullet);
            return bullet;
        }

        // Loop through all bullets and update
        public void UpdateBullets(GameTime gameTime, int levelWidth, int levelHeight, List<Entity> entityList)
        {
            // Delete bullets marked for removal
            BulletList.RemoveAll(bullet => bullet.Remove);

            // Update remaining bullets
            foreach (Bullet bullet in BulletList)
            {
                bullet.Update(gameTime, levelWidth, levelHeight, entityList);
            }
        }

        // Loop through all bullets and draw
        public void DrawBullets(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            foreach (Bullet bullet in BulletList)
            {
                bullet.Draw(spriteBatch, cameraPosition);
            }
        }
    }
}
