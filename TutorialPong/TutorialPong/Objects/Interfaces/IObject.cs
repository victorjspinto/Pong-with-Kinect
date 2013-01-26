using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TutorialPong.Objects.Interfaces
{
    interface IObject
    {
        Vector2 Position { get; set; }
        Texture2D Texture { get; set; }
        Color DrawColor { get; set; }

        void Draw(GameTime gameTime);
        void Update(GameTime gametime);
        void LoadContent();
    }
}
