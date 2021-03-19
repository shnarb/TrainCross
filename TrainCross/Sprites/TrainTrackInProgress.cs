using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainCross.Sprites
{
    class TrainTrackInProgress : TrainTrack
    {
        protected ButtonState PreviousState;

        public TrainTrackInProgress(Texture2D texture) : base(texture) { }

        public override void Update(GameTime gameTime, IList<Sprite> sprites)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (PreviousState != ButtonState.Pressed)
                {
                    IsVisible = true;
                    Origin = Mouse.GetState().Position.ToVector2();
                    Destination = Origin;
                }
                else
                {
                    Destination = Mouse.GetState().Position.ToVector2();

                    if(Vector2.Distance(Origin, Destination) < 100)
                    {
                        Vector2 trackVector = Destination - Origin;
                        trackVector.Normalize();
                        trackVector *= 100;
                        Destination = Origin + trackVector;
                    }

                    Vector2 delta = Destination - Origin;
                    Rotation = (float)Math.Atan2(delta.Y, delta.X);
                    int trainTrackLength = (int)Vector2.Distance(Origin, Destination);
                    DestinationRectangle = new Rectangle(Origin.ToPoint(), new Point(trainTrackLength, TrainTrackWidth));
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released && PreviousState == ButtonState.Pressed)
            {
                sprites.Add(new TrainTrack(Texture, this));
                IsVisible = false;
            }

            PreviousState = Mouse.GetState().LeftButton;
        }
    }
}
