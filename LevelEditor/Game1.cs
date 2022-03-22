using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LevelEditor
{
    public class Game1 : Game
    {
        public static int screenWidth = 960;
        public static int screenHeight = 540;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MapManager _mapManager;

        private CameraController _playerCam;
        private FollowTarget _cameraTarget;
        private InputManager _inputManager;

        private Editor _editor;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.IsFullScreen = false;
        }

        public Texture2D GetTexture(string path)
        {
            return Content.Load<Texture2D>(path);
        }

        protected override void Initialize()
        {
            _editor = new Editor(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            List<Texture2D> tileTextureList = new List<Texture2D>();
            tileTextureList.Add(GetTexture("Tiles/DefaultTile"));
            tileTextureList.Add(GetTexture("Tiles/Tile-Fire"));

            _mapManager = new MapManager(14, 10, tileTextureList);
            _cameraTarget = new FollowTarget(new Vector2(0, 0), GetTexture("Tiles/DefaultTile"));
            _playerCam = new CameraController();
            _inputManager = new InputManager(_cameraTarget, _mapManager, _playerCam);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _playerCam.MoveCamera(_cameraTarget);
            _inputManager.UpdateInput();
            _cameraTarget.UpdatePosition();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _playerCam.Transform);

            _mapManager.DrawTileMap(_spriteBatch);
            // _cameraTarget.Draw(_spriteBatch);

            _editor.DrawButtons(_spriteBatch);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
