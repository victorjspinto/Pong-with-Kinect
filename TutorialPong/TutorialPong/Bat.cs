using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TutorialPong
{
    class Bat
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _direction;
        private float _velocity;

        int side;

        #region Get's and Set's

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

        public Keys KeyUp { get; set; }

        public Keys KeyDown { get; set; }

        #endregion

        public Bat(Game game, Vector2 startPosition, Keys keyUp, Keys keyDown, int Side)
        {
            _texture = game.Content.Load<Texture2D>(@"Texture\bat");
            _position = startPosition;
            _direction = new Vector2(0, 0);
            _velocity = 500.0f;
            this.side = Side;

            this.KeyUp = keyUp;
            this.KeyDown = keyDown;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }


        public void Update(GameTime gameTime, Kinect kinectState, Ball ball)
        {
            //if (kinectState.Side == KinectSide.Left && Position.Y > 0)
            //{
            //    _direction = new Vector2(0, -1);
            //}
            //else if (kinectState.Side == KinectSide.Rigth && Position.Y + Texture.Height < 600)
            //{
            //    _direction = new Vector2(0, 1);
            //}
            //else
            //{
            //    _direction = Vector2.Zero;
            //}
            //Position += _direction * _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Position = new Vector2(Position.X, kinectState.Posicao);

            float distance;
            if (side == 0)
            {
                distance = Position.Y - kinectState.PosicaoLeft;
            }
            else
            {
                distance = Position.Y - kinectState.PosicaoRigth;
            }
            if (distance < 0)
            {
                _direction = new Vector2(0, 1);
            }
            else
            {
                _direction = new Vector2(0, -1);
            }
            distance *= Math.Sign(distance);

            Position += _direction * (distance * 30) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateBallState(ball);
        }

        private void UpdateBallState(Ball ball)
        {
            BoundingBox bat = new BoundingBox(new Vector3(Position, 0),
                new Vector3(Position.X + Texture.Width, Position.Y + Texture.Height, 0));

            BoundingBox ballRec = new BoundingBox(new Vector3(ball.Position, 0),
                new Vector3(ball .Position.X + ball.Texture.Width, ball.Position.Y + ball.Texture.Height, 0));

            if (bat.Intersects(ballRec))
            {
                ball.Direction *= new Vector2(-1, 1);
            }
        }


    }
}
