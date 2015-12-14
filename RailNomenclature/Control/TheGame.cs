using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RailNomenclature
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TheGame : Game
    {
        public const int WIDTH = 960;
        public const int HEIGHT = 540;
        public const int FPS = 60;

        public static TheGame Instance { get; protected set; }

        public static void Load()
        {
            if(Instance == null)
                Instance = new TheGame();
        }

        public static GraphicsDeviceManager Graphics;

        public GameState CurrentState { get; protected set; }
        public GameStateLeftPanel LeftPanel { get; protected set; }

        private TheGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            
            Graphics.PreferredBackBufferWidth = WIDTH;
            Graphics.PreferredBackBufferHeight = HEIGHT;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            CurrentState = new GameStateNull();

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            World w = new World();

            ChangeState(new GameStatePlaying(w));
            LeftPanel = new GameStateLeftPanel(w);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Assets.Load();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Assets.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseHandler.Instance.Update();

            if (CurrentState != null)
            {
                GameState previousState = CurrentState;
                
                CurrentState.HandleInput();

                if(previousState == CurrentState)
                    CurrentState.Update();
            }

            LeftPanel.HandleInput();
            LeftPanel.Update();

            base.Update(gameTime);
        }

        public bool IsMouseOnUIElement()
        {
            return LeftPanel.IsMouseOnUIElement();
        }

        public void ChangeState(GameState newState)
        {
            if (CurrentState != null)
                CurrentState.LeaveState();

            newState.EnterState();
           
            CurrentState = newState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Assets.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if (CurrentState != null)
                CurrentState.Draw();

            LeftPanel.Draw();

            Assets.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
