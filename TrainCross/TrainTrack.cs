using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainCross
{
    class TrainTrack : ISprite
    {
        protected Texture2D Texture;
        protected Rectangle R;
        public int Length { get; set; }
        public float Rotation { get; set; }

        public TrainTrack(Texture2D texture, Point p, int length)
        {
            Length = length;
            Texture = texture;
            Rotation = 0;
            R = new Rectangle(p, new Point(length, 25));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw track
            R.Width = Length;
            spriteBatch.Draw(
                texture: Texture,
                destinationRectangle: R,
                sourceRectangle: null,
                color: Color.Black,
                rotation: Rotation,
                origin: new Vector2(100, 50),
                effects: default,
                layerDepth: default);
        }
    }
}
