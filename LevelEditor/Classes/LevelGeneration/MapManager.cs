using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


namespace LevelEditor
{
    public class MapManager
    {
        public static bool tileSelection = true;
        public static bool entitySelection = false;
        public static bool assetSelection = false;

        public List<Tile> tileList = new List<Tile>();
        public List<Entity> entityList = new List<Entity>();
        public List<Asset> assetList = new List<Asset>();

        public List<Texture2D> tileTextureList = new List<Texture2D>();
        public List<Texture2D> entityTextureList = new List<Texture2D>();
        public List<Texture2D> assetTextureList = new List<Texture2D>();
        public List<Vector2> assetScaleList = new List<Vector2>();

        private int _mapWidth;
        private int _mapHeight;

        public int currentTileIndex = 1;
        public int currentEntityIndex = 1;
        public int currentAssetIndex = 1;
        
        private int[,] _tileMap;
        private int[,] _entityMap;
        private int[,] _assetMap;

        private MapData _mapData;

        public MapManager(int mapWidth, int mapHeight, List<Texture2D> tileTextures, List<Texture2D> entityTextures, List<Texture2D> assetTextures)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;

            tileTextureList = tileTextures;
            entityTextureList = entityTextures;
            assetTextureList = assetTextures;

            _tileMap = new int[mapWidth, mapHeight];
            _entityMap = new int[mapWidth, mapHeight];
            _assetMap = new int[mapWidth, mapHeight];

            GenerateMap();
        }

        public void DrawTileMap(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in tileList)
            {
                tile.Draw(spriteBatch);
            }

            foreach(Entity entity in entityList)
            {
                entity.Draw(spriteBatch);
            }

            foreach(Asset asset in assetList)
            {
                asset.Draw(spriteBatch);
            }
        }

        public void ChangeTile(Tile tile, bool deleteTile)
        {
            int tileIndex = tileList.IndexOf(tile);

            if(currentTileIndex < tileTextureList.Count && tileIndex < tileList.Count)
            {
                if(!deleteTile)
                {
                    tileList[tileIndex].TileTexture = tileTextureList[currentTileIndex];
                    _tileMap[tile.TileRectangle.X / 64, tile.TileRectangle.Y / 64] = currentTileIndex;
                }
                else
                {
                    tileList[tileIndex].TileTexture = tileTextureList[0];
                    _tileMap[tile.TileRectangle.X / 64, tile.TileRectangle.Y / 64] = 0;
                }
            }
        }

        public void FillMap()
        {
            foreach(Tile tile in tileList)
            {
                ChangeTile(tile, false);
            }
        }

        public void ChangeEntity(Entity entity, bool deleteEntity)
        {
            if (!deleteEntity)
            {
                entity.EntityTexture = entityTextureList[currentEntityIndex];
                _entityMap[(int)entity.EntityPosition.X / entity.EntityRectangle.Width, (int)entity.EntityPosition.Y / entity.EntityRectangle.Height] = currentEntityIndex;
                entity.EntityRectangle = new Rectangle(entity.EntityRectangle.X, entity.EntityRectangle.Y, entity.EntityTexture.Width, entity.EntityTexture.Height);
                entity.IsActive = true;
            }
            else
            {
                entity.EntityTexture = entityTextureList[0];
                _entityMap[(int)entity.EntityPosition.X / entity.EntityRectangle.Width, (int)entity.EntityPosition.Y / entity.EntityRectangle.Height] = 0;
                entity.IsActive = false;
            }
        }

        public void ChangeAsset(Asset asset, bool deleteAsset)
        {
            if (!deleteAsset)
            {
                asset.AssetTexture = assetTextureList[currentAssetIndex];
                _assetMap[(int)asset.AssetPosition.X / 64, (int)asset.AssetPosition.Y / 64] = currentAssetIndex;
                asset.AssetRectangle = new Rectangle(asset.AssetRectangle.X, asset.AssetRectangle.Y, asset.AssetTexture.Width, asset.AssetTexture.Height);
                asset.IsActive = true;
            }
            else
            {
                asset.AssetTexture = assetTextureList[0];
                _assetMap[(int)asset.AssetPosition.X / asset.AssetRectangle.Width, (int)asset.AssetPosition.Y / asset.AssetRectangle.Height] = 0;
                asset.IsActive = false;
            }
        }

        private void GenerateMap()
        {
            for(int x = 0; x < _mapWidth; x++)
            {
                for(int y = 0; y < _mapHeight; y++)
                {
                    Tile tile = new Tile(tileTextureList[0], new Vector2(x * 64, y * 64), new Vector2(64, 64), false, false);
                    tileList.Add(tile);

                    Entity entity = new Entity(entityTextureList[0], new Vector2(x * 64, y * 64), new Vector2(64, 64));

                    entity.IsActive = false;

                    entityList.Add(entity);

                    Texture2D assetTexture = assetTextureList[0];
                    Asset asset = new Asset(assetTexture, new Vector2(x * 64, y * 64), new Vector2(assetTexture.Width, assetTexture.Height));
                    assetList.Add(asset);

                    _tileMap[x, y] = 0;
                    _entityMap[x, y] = 0;
                    _assetMap[x, y] = 0;
                }
            }
        }

        public void SaveMapData()
        {
            _mapData = null;
            _mapData = new MapData(_mapWidth, _mapHeight, _tileMap, _entityMap, _assetMap, "Test Level");

            string strResultJson = JsonConvert.SerializeObject(_mapData);
            File.WriteAllText(@"MapData.json", strResultJson);

            strResultJson = File.ReadAllText(@"MapData.json");
            _mapData = JsonConvert.DeserializeObject<MapData>(strResultJson);
        }

        public void LoadMapData()
        {
            _mapData = null;
            string strResultJson = File.ReadAllText(@"MapData.json");
            MapData newMapData = JsonConvert.DeserializeObject<MapData>(strResultJson);
            _mapData = newMapData;

            _tileMap = _mapData._tileMap;
            _entityMap = _mapData._entityMap;
            _assetMap = _mapData._assetMap;
            _mapWidth = _mapData._mapWidth;
            _mapHeight = _mapData._mapHeight;

            int index = 0;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    tileList[index].TileTexture = tileTextureList[_mapData._tileMap[x, y]];

                    if (_mapData._entityMap[x, y] > 0)
                    {
                        Entity entity = entityList[index];

                        entity.EntityTexture = entityTextureList[_mapData._entityMap[x, y]];
                        entity.EntityRectangle = new Rectangle((int)entity.EntityPosition.X, (int)entity.EntityPosition.Y, entity.EntityTexture.Width, entity.EntityTexture.Height);
                        entity.IsActive = true;
                        entity.EntityColor = Color.White;

                        Asset asset = assetList[index];
                        asset.AssetTexture = assetTextureList[_mapData._assetMap[x, y]];
                        asset.AssetRectangle = new Rectangle((int)asset.AssetPosition.X, (int)asset.AssetPosition.Y, entity.EntityTexture.Width, entity.EntityTexture.Height);
                        asset.IsActive = true;
                        asset.AssetColor = Color.White;
                    }

                    index++;
                }
            }
        }
    }
}
