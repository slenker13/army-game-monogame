using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ArmyGame
{
    public class ArmyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Textures
        Texture2D background;

        // Game objects
        Camera2D camera;
        Player player;

        // Input states
        KeyboardState keyboardState;
        MouseState mouseState;

        // Level dimensions
        int levelWidth = 1920;
        int levelHeight = 1440;


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

            // Create player
            player = new Player(new Vector2(levelWidth / 2, levelHeight / 2));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Textures/Backgrounds/background");
            player.Texture = Content.Load<Texture2D>("Textures/Entities/player");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Get input states
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Update objects
            player.Update(gameTime, keyboardState, mouseState, levelWidth, levelHeight, camera.Position);
            camera.Update(new Vector2((player.Position.X + player.Width / 2) - _graphics.PreferredBackBufferWidth / 2, (player.Position.Y + player.Height / 2) - _graphics.PreferredBackBufferHeight / 2));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw background
            _spriteBatch.Draw(background, new Vector2(0, 0), camera.CameraRect, Color.White);

            // Draw Entities
            player.Draw(_spriteBatch, camera.Position);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
