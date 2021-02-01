using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics.Contracts;

namespace TrainCross
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TrainCrossGame : Game
    {
        private readonly int ScreenWidth = 1920;
        private readonly int ScreenHeight = 1080;
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        Texture2D TrainTexture;
        Texture2D TrainTrackTexture;


        public TrainCrossGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            this.IsMouseVisible = true;
            Window.IsBorderless = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
            TrainTexture = Content.Load<Texture2D>("TrainModel");
            TrainTrackTexture = Content.Load<Texture2D>("SimpleTexture");
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

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                trackAngle = (float)Math.Atan2(Mouse.GetState().Y, Mouse.GetState().X);
                isLeftReleased = false;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                maxTrackLength = trackLength;
                isLeftReleased = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            //Draw Sprites
            SpriteBatch.Begin();

            //Draw Tracks

            SpriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// This maps a value from one range to another
        /// </summary>
        /// <param name="val">Value to be remapped</param>
        /// <param name="min">Minimum value that 'val' can take</param>
        /// <param name="max">Maximum value that 'val' can take</param>
        /// <param name="otherMin">Minimum value that 'val' will be mapped to</param>
        /// <param name="otherMax">Maximum value that 'val' will be mapped to</param>
        /// <returns></returns>
        private float Remap(float val, float min, float max, float otherMin, float otherMax)
        {
            Contract.Requires<ArgumentOutOfRangeException>(val >= min && val <= max,
                string.Format("Parameter 'val' must be within range of 'min' and 'max'. " +
                "Recieved val: {0} min: {1} max: {2}",
                val, min, max));
            return (val - min) / (max - min) * (otherMax - otherMin) + min;
        }
    }
}
