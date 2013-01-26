using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TutorialPong.Objects.Interfaces;

namespace TutorialPong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<IObject> objectsOnScreen;
        Texture2D _background;
        Ball _ball;
        Bat _bat1, _bat2;


        Kinect kinect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Window.Title = "Pong";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.objectsOnScreen = new List<IObject>();
            this.objectsOnScreen
                .Add(new Objects.Ball(this) 
                { 
                    Direction = new Vector2(1, 0), Velocity = 100.0f 
                }
            );
            kinect = new Kinect();
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            foreach (IObject item in this.objectsOnScreen)
            {
                item.LoadContent();
            }

            // TODO: use this.Content to load your game content here
            _background = this.Content.Load<Texture2D>(@"Texture\ping_pong");
            _ball = new Ball(this, new Vector2(386.0f, 310.0f));
            _bat1 = new Bat(this, new Vector2(10.0f, 290.0f), Keys.Up, Keys.Down, 0);
            _bat2 = new Bat(this, new Vector2(765.0f, 290.0f), Keys.W, Keys.S , 1);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (IObject item in this.objectsOnScreen)
            {
                item.Update(gameTime);
            }


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            _bat1.Update(gameTime, kinect, _ball);
            _bat2.Update(gameTime, kinect, _ball);
            _ball.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (IObject item in this.objectsOnScreen)
            {
                item.Draw(gameTime);
            }

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            _ball.Draw(spriteBatch);
            _bat1.Draw(spriteBatch);
            _bat2.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
