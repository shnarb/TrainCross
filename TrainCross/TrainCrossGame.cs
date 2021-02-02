using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        readonly GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;
        private Texture2D TrainTexture;
        private Texture2D TrainTrackTexture;
        private ICollection<ISprite> Sprites;
        private TrainTrack InProgressTrainTrack;
        private bool IsPreviousStatePressed;
        private Point InProgressTrainTrackOrigin;


        public TrainCrossGame()
        {
            // Initialize ContentManager and GraphicsDeviceManager.
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set window dimensions.
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Window.IsBorderless = true;
            Graphics.IsFullScreen = true;

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
            Sprites = new List<ISprite>();
            IsPreviousStatePressed = false;
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
            TrainTexture = Content.Load<Texture2D>(@"Sprites\TrainModel");
            TrainTrackTexture = Content.Load<Texture2D>(@"Sprites\SimpleTextureSmooth");
            Mouse.SetCursor(MouseCursor.FromTexture2D(TrainTexture, 0, 0));
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
                Sprites.Clear();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (IsPreviousStatePressed)
                {
                    var NewMousePosition = Mouse.GetState().Position;
                    var DeltaX = InProgressTrainTrackOrigin.X - NewMousePosition.X;
                    var DeltaY = InProgressTrainTrackOrigin.Y - NewMousePosition.Y;
                    var Rotation = Math.Atan2(DeltaY, DeltaX);
                    InProgressTrainTrack.Length = (int)Math.Sqrt(Math.Pow(DeltaX, 2) + Math.Pow(DeltaY, 2));
                    InProgressTrainTrack.Rotation = (float)Rotation;
                }
                else
                {
                    InProgressTrainTrackOrigin = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                    InProgressTrainTrack = new TrainTrack(TrainTrackTexture, InProgressTrainTrackOrigin, 0);
                }
                IsPreviousStatePressed = true;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                if (IsPreviousStatePressed)
                {
                    Sprites.Add(InProgressTrainTrack);
                }
                InProgressTrainTrack = null;
                IsPreviousStatePressed = false;
            }

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

            // Draw temporary train track
            if (InProgressTrainTrack != null)
            {
                InProgressTrainTrack.Draw(SpriteBatch);
            }

            if (Sprites.Count > 0)
            {
                // Draw placed train tracks
                foreach (ISprite sprite in Sprites)
                {
                    sprite.Draw(SpriteBatch);
                }
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// This maps a value from one range to another.
        /// </summary>
        /// <param name="val">Value to be remapped.</param>
        /// <param name="min">Minimum value that 'val' can take.</param>
        /// <param name="max">Maximum value that 'val' can take.</param>
        /// <param name="otherMin">Minimum value that 'val' will be mapped to.</param>
        /// <param name="otherMax">Maximum value that 'val' will be mapped to.</param>
        /// <returns></returns>
        private float Remap(float val, float min, float max, float otherMin, float otherMax)
        {
            Contract.Requires<ArgumentOutOfRangeException>(val >= min && val <= max,
                string.Format("Parameter 'val' must be within range of 'min' and 'max'. " +
                "Recieved val: {0} min: {1} max: {2}", val, min, max));
            return (val - min) / (max - min) * (otherMax - otherMin) + min;
        }
    }
}
