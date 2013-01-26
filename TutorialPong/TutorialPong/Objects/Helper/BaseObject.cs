using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TutorialPong.Objects.Interfaces;

namespace TutorialPong.Objects.Helper
{
    public  class BaseObject: DrawableGameComponent, IObject
    {
        protected Game game;
        protected SpriteBatch spriteBatch;

        public BaseObject(Game game)
            :base(game)
        {
            this.game = game;
            this.DrawColor = Color.White;
            this.Position = new Vector2(400.0f, 300.0f);
        }

        #region Fields

        public Vector2 Position
        {
            get;
            set;
        }

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture
        {
            get;
            set;
        }

        public Color DrawColor
        {
            get;
            set;
        }

        #endregion
        
        public override void  Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.Texture, this.Position, this.DrawColor);
            spriteBatch.End();

            base.Draw(gameTime);
         
        }

        public new virtual void LoadContent()
        {
            base.LoadContent();
            this.spriteBatch = game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
        }
    }
}
