using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TutorialPong
{
    class Ball
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _direction;
        private float _velocity;

        #region Get's and Set's
        
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public float Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        
        #endregion

        public Ball(Game game, Vector2 startPosition)
        {
            _texture = game.Content.Load<Texture2D>(@"Texture\ball");
            _position = startPosition;
            _direction = new Vector2(1.0f, 1.0f);
            _velocity = 350.0f;
        }

        public void Update(GameTime gameTime)
        {
            if (Position.X < 0 || Position.X + Texture.Width > 800)
            {
                Position = new Vector2(386.0f, 310.0f);
            }

            if (Position.Y < 0 || Position.Y + Texture.Height > 600.0f)
            {
                Direction *= new Vector2(1.0f, -1.0f);
            }

            if (Position.X < 0.0f || Position.X + Texture.Width > 800)
            {
                Direction *= new Vector2(-1, 1);
            }

            _position += _direction * _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
