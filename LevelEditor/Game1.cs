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

        private SpriteFont _modeText;
        private string _modeString;

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
            tileTextureList.Add(GetTexture("Tiles/DefaultTile")); //0
            tileTextureList.Add(GetTexture("Tiles/Tile_Sand")); //1
            tileTextureList.Add(GetTexture("Tiles/Tile_Filler")); //2
            tileTextureList.Add(GetTexture("Tiles/Right_Corner")); //3
            tileTextureList.Add(GetTexture("Tiles/Left_Slide")); //4
            tileTextureList.Add(GetTexture("Tiles/Right_Slide")); //5
            tileTextureList.Add(GetTexture("Tiles/First_Sky_Tile")); //6
            tileTextureList.Add(GetTexture("Tiles/Second_Sky_Tile")); //7
            tileTextureList.Add(GetTexture("Tiles/Third_Sky_Tile")); //8
            tileTextureList.Add(GetTexture("Tiles/Fourth_Sky_Tile")); //9
            tileTextureList.Add(GetTexture("Tiles/Left_Corner")); //10

            List<Texture2D> entityTextureList = new List<Texture2D>();
            entityTextureList.Add(GetTexture("Tiles/DefaultTile")); //0
            entityTextureList.Add(GetTexture("Entities/KnightCharacter_Sprite")); //1
            entityTextureList.Add(GetTexture("Entities/Health_Pickup")); // 2
            entityTextureList.Add(GetTexture("Entities/Fire_Pickup")); //3
            entityTextureList.Add(GetTexture("Entities/Water_Pickup")); //4
            entityTextureList.Add(GetTexture("Entities/Snow_Pickup")); //5
            entityTextureList.Add(GetTexture("Entities/Collectible")); //6
            entityTextureList.Add(GetTexture("Entities/PlayerStart_Tile")); //7
            entityTextureList.Add(GetTexture("Entities/FireTile")); //8
            entityTextureList.Add(GetTexture("Entities/WaterTile")); //9
            entityTextureList.Add(GetTexture("Entities/SnowTile")); //10
            entityTextureList.Add(GetTexture("Entities/DialogueTrigger_Tile")); //11

            //Set the width, height, available textures and entities
            _mapManager = new MapManager(28, 20, tileTextureList, entityTextureList);
            _cameraTarget = new FollowTarget(new Vector2(0, 0), GetTexture("Tiles/DefaultTile"));
            _playerCam = new CameraController();
            _inputManager = new InputManager(_cameraTarget, _mapManager, _playerCam);

            _modeText = Content.Load<SpriteFont>("Text/ModeText");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _playerCam.MoveCamera(_cameraTarget);
            _inputManager.UpdateInput();
            _cameraTarget.UpdatePosition();

            if(MapManager.tileSelection)
            {
                _modeString = "Tile Mode\nIndex = " + _mapManager.currentTileIndex;
            }
            else
            {
                _modeString = "Entity Mode\nIndex = " + _mapManager.currentEntityIndex;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _playerCam.Transform);

            _mapManager.DrawTileMap(_spriteBatch);
            // _cameraTarget.Draw(_spriteBatch);

            _editor.DrawButtons(_spriteBatch);

            _spriteBatch.DrawString(_modeText, _modeString, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
