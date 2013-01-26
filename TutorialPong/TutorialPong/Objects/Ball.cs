using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TutorialPong.Objects.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TutorialPong.Objects.Interfaces;

namespace TutorialPong.Objects
{
    class Ball: BaseObject, IMove
    {
        public Ball(Game game)
            :base(game)
        {
            this.Position = Vector2.Zero;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.Texture = game.Content.Load<Texture2D>(@"Texture\ball");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Position += Direction * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public Vector2 Direction
        {
            get;
            set;
        }

        public float Velocity
        {
            get;
            set;
        }
    }
}
