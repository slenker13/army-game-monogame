using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ArmyGame
{
    public class ArmyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Textures
        Texture2D background;

        // Fonts
        SpriteFont font;

        // Camera
        Camera2D camera;

        // Entities
        List<Entity> entityList;
        Player player;
        Wall wall1;

        // Input states
        KeyboardState keyboardState;
        MouseState mouseState;

        // Level dimensions
        int levelWidth = 1920;
        int levelHeight = 1440;

        // Game variables
        int score = 0;


        public ArmyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set screen size
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            //_graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            //_graphics.IsFullScreen = true;
            // _graphics.ApplyChanges();

            // Create camera
            camera = new Camera2D(Vector2.Zero, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, levelWidth, levelHeight);

            // Init entity list
            entityList = new List<Entity>();

            // Create player
            player = new Player(new Vector2(levelWidth / 2, levelHeight / 2));
            entityList.Add(player);

            // Create walls
            wall1 = new Wall(GraphicsDevice, new Vector2(600f, 300f), 40, 400, Color.Black);
            entityList.Add(wall1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Textures/Backgrounds/background");
            player.Texture = Content.Load<Texture2D>("Textures/Entities/player");
            font = Content.Load<SpriteFont>("Fonts/Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Get input states
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Update objects
            player.Update(gameTime, keyboardState, mouseState, levelWidth, levelHeight, camera.Position, entityList);
            camera.Update(new Vector2((player.Position.X + player.Width / 2) - _graphics.PreferredBackBufferWidth / 2, (player.Position.Y + player.Height / 2) - _graphics.PreferredBackBufferHeight / 2));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Calculate framerate
            int frameRate = (int)Math.Round(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw background
            _spriteBatch.Draw(background, new Vector2(0, 0), camera.CameraRect, Color.White);

            // Draw entities
            player.Draw(_spriteBatch, camera.Position);
            wall1.Draw(_spriteBatch, camera.Position);

            // Draw text
            // _spriteBatch.DrawString(font, "FPS: " + frameRate, new Vector2(_graphics.PreferredBackBufferWidth - 100, 10), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
