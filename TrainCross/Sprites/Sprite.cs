using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TrainCross
{
    class Sprite
    {
        protected readonly Texture2D Texture;
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }


        public virtual void Update(GameTime gameTime, IList<Sprite> sprites) { }
    }
}
