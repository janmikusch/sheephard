/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sheephard
{


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Game1 Reference { get; private set; }
        public GraphicsDeviceManager GraphicsDeviceManager;
        SpriteBatch _spriteBatch;

        public Game1()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GraphicsDeviceManager.PreferredBackBufferHeight = 720;
            GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
            GraphicsDeviceManager.ApplyChanges();
            Reference = this;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameController.Init();
            InputManager.Init();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            FontHelper.Reference.LoadContent(this);

            GameController.LoadContent(this);

            ScoreBoard.Reference.LoadContent(this);

            MainMenu.Reference.LoadContent(this);

            AnimationManager.Reference.LoadContent(this);

            //finished loading
            GameController.PlayMusic();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameController.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            GameController.Draw(gameTime, _spriteBatch);


            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
