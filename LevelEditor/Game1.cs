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
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_GroundTile")); //0
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_RightCorner")); //1
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_RighSlide")); //2
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_LeftCorner")); //3
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_LeftSlide")); //4
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_DownRightCorner")); //5
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_DownLeftCorner")); //6
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_Backwards")); //7
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Snow_Filler")); //8
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_GroundTile")); //9
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_RightCorner")); //10
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_RightSlide")); //11
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_LeftCorner")); //12
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Sky_FirstTile")); //13
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Sky_SecondTile")); //14
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Sky_ThirdTile")); //15
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_FourthTile")); //16
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_FifthTile")); //17
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Sky_SixthTile")); //18
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Sky_SeventhTile")); //19
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_LeftSlide")); //20
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_DownRightCorner")); //21
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_DownLeftCorner")); //22
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_Filler")); //23
            tileTextureList.Add(GetTexture("Tiles/SnowLevelTiles/Ice_Backwards")); //24

            List<Texture2D> entityTextureList = new List<Texture2D>();
            entityTextureList.Add(GetTexture("Tiles/DefaultTile")); //0
            entityTextureList.Add(GetTexture("Entities/PlayerStart_Tile")); //1
            entityTextureList.Add(GetTexture("Entities/CampFire")); //2
            entityTextureList.Add(GetTexture("Entities/FireKnight")); //3
            entityTextureList.Add(GetTexture("Entities/WaterKnight")); //4
            entityTextureList.Add(GetTexture("Entities/SnowKnight")); //5
            entityTextureList.Add(GetTexture("Entities/FireMage")); //6
            entityTextureList.Add(GetTexture("Entities/WaterMage")); //7
            entityTextureList.Add(GetTexture("Entities/SnowMage")); //8
            entityTextureList.Add(GetTexture("Entities/Health_Pickup")); //9
            entityTextureList.Add(GetTexture("Entities/Collectible")); //10
            entityTextureList.Add(GetTexture("Entities/Fire_Pickup")); //11
            entityTextureList.Add(GetTexture("Entities/Water_Pickup")); //12
            entityTextureList.Add(GetTexture("Entities/Snow_Pickup")); //13
            entityTextureList.Add(GetTexture("Entities/FireTile")); //14
            entityTextureList.Add(GetTexture("Entities/WaterTile")); //15
            entityTextureList.Add(GetTexture("Entities/SnowTile")); //16
            entityTextureList.Add(GetTexture("Entities/Spikes")); //17
            entityTextureList.Add(GetTexture("Entities/Cannon_Right")); //18
            entityTextureList.Add(GetTexture("Entities/Cannon_Left")); //19
            entityTextureList.Add(GetTexture("Entities/Fire_Spectre")); //20
            entityTextureList.Add(GetTexture("Entities/Water_Spectre")); //21
            entityTextureList.Add(GetTexture("Entities/Snow_Spectre")); //22

            //**** Change Asset Textures Based on Level ****
            List<Texture2D> assetTextureList = new List<Texture2D>();
            assetTextureList.Add(GetTexture("Tiles/DefaultTile")); //0
            assetTextureList.Add(GetTexture("Assets/Igloo1"));
            assetTextureList.Add(GetTexture("Assets/Snow_Pile"));
            assetTextureList.Add(GetTexture("Assets/Snow_Pile2"));
            assetTextureList.Add(GetTexture("Assets/Snow_Pine"));
            assetTextureList.Add(GetTexture("Assets/Palm_Tree"));
            assetTextureList.Add(GetTexture("Assets/Palm_Tree2"));

            //Set the width, height, available textures and entities
            _mapManager = new MapManager(80, 15, tileTextureList, entityTextureList, assetTextureList);
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
            else if(MapManager.entitySelection)
            {
                _modeString = "Entity Mode\nIndex = " + _mapManager.currentEntityIndex;
            }
            else if(MapManager.assetSelection)
            {
                _modeString = "Asset Mode\nIndex = " + _mapManager.currentAssetIndex;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _playerCam.Transform);

            _mapManager.DrawTileMap(_spriteBatch);

            _editor.DrawButtons(_spriteBatch);

            _spriteBatch.DrawString(_modeText, _modeString, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
