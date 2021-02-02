using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainCross
{
    class TrainTrack : Sprite
    {
        readonly Texture2D Texture;
        // Rectangle defining shape of train track.
        Rectangle TrackRect;
        // Rectangle defining shape of bars on train track.
        Rectangle BarRect;
        public int Length { get; set; }
        public float Rotation { get; set; }
        public Point CurrentCursorPosition { get; set; }
        private Point OriginalPosition;

        public TrainTrack(Texture2D texture, Point p, int length, int layer = 1)
        {
            Length = length;
            Texture = texture;
            Rotation = 0;
            OriginalPosition = p;
            TrackRect = new Rectangle(OriginalPosition, new Point(length, 25));
        }

        override
        public void Draw(SpriteBatch spriteBatch)
        {
            int segments = (int)(Length / 25) + 1;
            DrawTrack(spriteBatch);
            for (int i = 0; i <= segments; i++)
            {
                Vector2 pVector = (CurrentCursorPosition.ToVector2() - OriginalPosition.ToVector2()) * new Vector2((float)i / segments, (float)i / segments);
                spriteBatch.Draw(
                    texture: Texture,
                    destinationRectangle: new Rectangle(OriginalPosition + pVector.ToPoint(), new Point(10, 40)),
                    sourceRectangle: null,
                    color: Color.Silver,
                    rotation: Rotation,
                    origin: new Vector2(50, 50),
                    effects: default,
                    layerDepth: default);
            }
        }


        private void DrawTrack(SpriteBatch spriteBatch)
        {
            TrackRect.Width = Length;
            spriteBatch.Draw(
                texture: Texture,
                destinationRectangle: TrackRect,
                sourceRectangle: null,
                color: Color.Black,
                rotation: Rotation,
                origin: new Vector2(100, 50),
                effects: default,
                layerDepth: default);
        }
    }
}
