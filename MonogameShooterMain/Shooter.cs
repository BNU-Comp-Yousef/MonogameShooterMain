using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameShooterMain.States;
using System;

namespace MonogameShooterMain
{
    //The main Class for my shooter game, Written By: Yousef Abobaker.
    // This class is the main class for running the game and gets called first when starting it up.
    public class Shooter : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch sb;

        public static Random Random;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        private State _currentState;
        private State _nextState;

        public Shooter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
        //Initialises the game by booting up the screenwidth and screenheight.
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Random = new Random();

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }
        // Loads up all the graphics and menu states.
        protected override void LoadContent()
        {
            sb = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, Content);
            _currentState.LoadContent();

            _nextState = null;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gt)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

            // TODO: Add your update logic here

            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }

            _currentState.Update(gt);

            _currentState.PostUpdate(gt);

            base.Update(gt);
        }
        // Changes state of the game relevant to what is pressed.
        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Draw(GameTime gt)
        {
            GraphicsDevice.Clear(new Color(55, 55, 55));

            // TODO: Add your drawing code here

            _currentState.Draw(gt, sb);

            base.Draw(gt);
        }
    }
}
