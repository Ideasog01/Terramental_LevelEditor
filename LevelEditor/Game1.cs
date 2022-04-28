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
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Corner_Tile_UpwardsLeft")); //0
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Corner_Tile_UpwardsRight")); //1
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_BottomLeft_CornerTile")); //2
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_BottomRight_CornerTile")); //3
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_Left_CornerTile")); //4
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_LeftSide_Tile")); //5
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_Right_CornerTile")); //6
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_RightSlide_Tile")); //7
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Grass_Tile")); //8
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Left_Corner")); //9
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Left_Slide")); //10
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Right_Corner")); //11
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Right_Slide")); //12
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_FifthTile")); //13
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_FirstTile")); //14
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_FourthTile")); //15
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_SecondTile")); //16
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_SeventhTile")); //17
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_SixthTile")); //18
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Sky_ThirdTile")); //19
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Thin_Tile_64x32")); //20
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Tile_Filler")); //21
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Tile_Sand")); //22
            tileTextureList.Add(GetTexture("Tiles/WaterLevelTiles/Tile_SandReverse")); //23

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
            entityTextureList.Add(GetTexture("Entities/Collectible")); //12
            entityTextureList.Add(GetTexture("Entities/Collectible")); //13
            entityTextureList.Add(GetTexture("Entities/DarkMage_Sprite")); //14
            entityTextureList.Add(GetTexture("Entities/Spikes")); //15

            //**** Change Asset Textures Based on Level ****
            List<Texture2D> assetTextureList = new List<Texture2D>();
            assetTextureList.Add(GetTexture("Tiles/DefaultTile")); //0
            assetTextureList.Add(GetTexture("Assets/Big_Palm"));
            assetTextureList.Add(GetTexture("Assets/Grass_1"));
            assetTextureList.Add(GetTexture("Assets/Grass_2"));
            assetTextureList.Add(GetTexture("Assets/Grass_3"));
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
            // _cameraTarget.Draw(_spriteBatch);

            _editor.DrawButtons(_spriteBatch);

            _spriteBatch.DrawString(_modeText, _modeString, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
