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
        public List<int> assetIndexList = new List<int>();
        public List<Vector2> assetPositionList = new List<Vector2>();

        private int _mapWidth;
        private int _mapHeight;

        public int currentTileIndex = 1;
        public int currentEntityIndex = 1;
        public int currentAssetIndex = 1;
        
        private int[,] _tileMap;
        private int[,] _entityMap;

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
                
            GenerateMap();
        }

        public void DrawTileMap(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tileList)
            {
                tile.Draw(spriteBatch);
            }

            foreach (Entity entity in entityList)
            {
                entity.Draw(spriteBatch);
            }

            foreach (Asset asset in assetList)
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
                entity.EntityRectangle = new Rectangle(entity.EntityRectangle.X, entity.EntityRectangle.Y, 64, 64);
                entity.IsActive = false;

                foreach(Entity entityInList in entityList)
                {
                    if(!entityInList.IsActive)
                    {
                        _entityMap[(int)entityInList.EntityPosition.X / entityInList.EntityRectangle.Width, (int)entityInList.EntityPosition.Y / entityInList.EntityRectangle.Height] = 0;
                    }
                }
            }
        }

        public void NewAsset(Vector2 mousePos)
        {
            Texture2D assetTexture = assetTextureList[currentAssetIndex];
            Vector2 assetPosition = mousePos;

            Asset asset = new Asset(assetTexture, assetPosition, new Vector2(assetTexture.Width, assetTexture.Height), currentAssetIndex);
            asset.IsActive = true;
            asset.AssetColor = Color.White;
            assetList.Add(asset);
            assetPositionList.Add(assetPosition);
            assetIndexList.Add(currentAssetIndex);
        }

        public void DeleteAsset(Asset asset)
        {
            assetIndexList.RemoveAt(assetList.IndexOf(asset));
            assetPositionList.RemoveAt(assetList.IndexOf(asset));
            assetList.Remove(asset);
            asset.IsActive = false;
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

                    _tileMap[x, y] = 0;
                    _entityMap[x, y] = 0;
                }
            }
        }

        public void SaveMapData()
        {
            _mapData = null;
            _mapData = new MapData(_mapWidth, _mapHeight, _tileMap, _entityMap, "Test Level", assetIndexList, assetPositionList);

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

            _tileMap = _mapData.tileMap;
            _entityMap = _mapData.entityMap;
            _mapWidth = _mapData.mapWidth;
            _mapHeight = _mapData.mapHeight;
            assetIndexList = _mapData.assetList;
            assetPositionList = _mapData.assetPositionList;

            int index = 0;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    tileList[index].TileTexture = tileTextureList[_mapData.tileMap[x, y]];

                    if (_mapData.entityMap[x, y] > 0)
                    {
                        Entity entity = entityList[index];

                        entity.EntityTexture = entityTextureList[_mapData.entityMap[x, y]];
                        entity.EntityRectangle = new Rectangle((int)entity.EntityPosition.X, (int)entity.EntityPosition.Y, entity.EntityTexture.Width, entity.EntityTexture.Height);
                        entity.IsActive = true;
                        entity.EntityColor = Color.White;
                    }

                    index++;
                }
            }

            assetList.Clear();

            int assetCount = 0;

            foreach(int assetIndex in assetIndexList)
            {
                Texture2D assetTexture = assetTextureList[assetIndex];
                Asset asset = new Asset(assetTexture, assetPositionList[assetCount], new Vector2(assetTexture.Width, assetTexture.Height), assetIndex);
                asset.IsActive = true;
                assetList.Add(asset);
                assetCount++;
            }
        }
    }
}
