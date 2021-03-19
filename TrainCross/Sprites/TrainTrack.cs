using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainCross.Sprites
{
    class TrainTrack : Sprite
    {
        protected const int TrainTrackWidth = 15;
        protected const int TrainBarWidth = 3;
        protected const int TrainBarLength = 20;
        protected int TrainBarSeparationDistance = 20;

        protected readonly Color TrainTrackColor = Color.Black;
        protected readonly Color TrainBarColor = Color.White;
        protected readonly Vector2 RotationOrigin;
        protected Vector2 Origin;
        protected Vector2 Destination;

        protected Rectangle DestinationRectangle;
        protected float Rotation;
        protected bool IsVisible;


        public TrainTrack(Texture2D texture) : base(texture)
        {
            RotationOrigin = new Vector2(0, texture.Width / 2);
            IsVisible = false;
        }

        public TrainTrack(Texture2D texture, TrainTrackInProgress trainTrackInProgress) : this(texture)
        {
            IsVisible = true;
            DestinationRectangle = trainTrackInProgress.DestinationRectangle;
            Rotation = trainTrackInProgress.Rotation;
            Origin = trainTrackInProgress.Origin;
            Destination = trainTrackInProgress.Destination;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                DrawRails(spriteBatch);
                DrawRailBars(spriteBatch);
            }
        }

        private void DrawRailBars(SpriteBatch spriteBatch)
        {
            var barCount = DestinationRectangle.Width / TrainBarSeparationDistance;
            var delta = Destination - Origin;
            for (int i = 0; i <= barCount; i++)
            {

                spriteBatch.Draw(
                            texture: Texture,
                            destinationRectangle: new Rectangle(
                                (Origin + delta * i / barCount).ToPoint(),
                                new Point(TrainBarWidth, TrainBarLength)
                                ),
                            sourceRectangle: default,
                            color: TrainBarColor,
                            rotation: Rotation,
                            origin: RotationOrigin,
                            effects: default,
                            layerDepth: default
                            );
            }
        }

        private void DrawRails(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                            texture: Texture,
                            destinationRectangle: DestinationRectangle,
                            sourceRectangle: default,
                            color: TrainTrackColor,
                            rotation: Rotation,
                            origin: RotationOrigin,
                            effects: default,
                            layerDepth: default
                            );
        }
    }
}
