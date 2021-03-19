using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using TrainCross.Sprites;
using TrainCross.Utils;

namespace TrainCross
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TrainCrossGame : Game
    {
        private const int ScreenWidth = 800;
        private const int ScreenHeight = 600;
        readonly GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        private Texture2D _trainTrackTexture;
        private SpriteFont _font;
        private IList<Sprite> _finished_trains;
        private IList<Sprite> _in_progress_trains;
        private SmartFramerate _frameRate;


        public TrainCrossGame()
        {
            // Initialize ContentManager and GraphicsDeviceManager.
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set window dimensions.
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            // In order to make the game borderless and fullscreen uncomment next 
            // two lines.
            //Window.IsBorderless = true;
            //Graphics.IsFullScreen = true;

            Graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Mouse.SetCursor(MouseCursor.Crosshair);
            _frameRate = new SmartFramerate(5);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _trainTrackTexture = Content.Load<Texture2D>(@"Sprites\SimpleTextureSmooth");
            _font = Content.Load<SpriteFont>(@"Fonts\Arial");
            _finished_trains = new List<Sprite>();
            _in_progress_trains = new List<Sprite>() { new TrainTrackInProgress(_trainTrackTexture) };

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _finished_trains.Clear();
            }


            // Draw placed train tracks.
            foreach (Sprite sprite in new List<Sprite>(_finished_trains))
            {
                sprite.Update(gameTime, _finished_trains);
            }

            foreach (Sprite sprite in new List<Sprite>(_in_progress_trains))
            {
                sprite.Update(gameTime, _finished_trains);
            }

            _frameRate.Update(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            // Reset screen.
            GraphicsDevice.Clear(Color.DimGray);

            // Draw sprites.
            SpriteBatch.Begin();
            // Draw placed train tracks.
            foreach (Sprite sprite in _finished_trains)
            {
                sprite.Draw(SpriteBatch);
            }
            foreach(Sprite sprite in _in_progress_trains)
            {
                sprite.Draw(SpriteBatch);
            }
            SpriteBatch.End();

            DrawFrameRate();

            base.Draw(gameTime);
        }

        private void DrawFrameRate()
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(_font, $"{_frameRate.Framerate:0}", new Vector2(50, 50), Color.Black);
            SpriteBatch.End();
        }
    }
}
