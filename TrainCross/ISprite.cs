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
    abstract class Sprite
    {
        abstract public void Draw(SpriteBatch spriteBatch);
    }
}
