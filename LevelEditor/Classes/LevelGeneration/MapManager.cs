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

        public List<Tile> tileList = new List<Tile>();
        public List<Entity> entityList = new List<Entity>();

        public List<Texture2D> tileTextureList = new List<Texture2D>();
        public List<Texture2D> entityTextureList = new List<Texture2D>();
        public List<Vector2> entityScaleList = new List<Vector2>();

        private int _mapWidth;
        private int _mapHeight;

        public int currentTileIndex = 1;
        public int currentEntityIndex = 1;
        
        private int[,] _tileIndex;
        private int[,] _entityIndex;

        private MapData _mapData;

        public MapManager(int mapWidth, int mapHeight, List<Texture2D> tileTextures, List<Texture2D> entityTextures)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            tileTextureList = tileTextures;
            entityTextureList = entityTextures;
            _tileIndex = new int[mapWidth, mapHeight];
            _entityIndex = new int[mapWidth, mapHeight];

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
        }

        public void ChangeTile(Tile tile, bool deleteTile)
        {
            int tileIndex = tileList.IndexOf(tile);

            if(currentTileIndex < tileTextureList.Count && tileIndex < tileList.Count)
            {
                if(!deleteTile)
                {
                    tileList[tileIndex].TileTexture = tileTextureList[currentTileIndex];
                    _tileIndex[tile.TileRectangle.X / 64, tile.TileRectangle.Y / 64] = currentTileIndex;
                }
                else
                {
                    tileList[tileIndex].TileTexture = tileTextureList[0];
                    _tileIndex[tile.TileRectangle.X / 64, tile.TileRectangle.Y / 64] = 0;
                }
            }
        }

        public void ChangeEntity(Entity entity, bool deleteEntity)
        {
            int entityindex = entityList.IndexOf(entity);

            if(currentEntityIndex < entityTextureList.Count && entityindex < entityList.Count)
            {
                if(!deleteEntity)
                {
                    entityList[entityindex].EntityTexture = entityTextureList[currentEntityIndex];
                    _entityIndex[entity.EntityRectangle.X / entity.EntityRectangle.Width, entity.EntityRectangle.Y / entity.EntityRectangle.Height] = currentEntityIndex;
                    entity.EntityRectangle = new Rectangle(entity.EntityRectangle.X, entity.EntityRectangle.Y, entity.EntityTexture.Width, entity.EntityTexture.Height);
                    entity.IsActive = true;
                }
                else
                {
                    entityList[entityindex].EntityTexture = entityTextureList[0];
                    _entityIndex[entity.EntityRectangle.X / entity.EntityRectangle.Width, entity.EntityRectangle.Y / entity.EntityRectangle.Height] = 0;
                    entity.IsActive = false;
                }
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

                    _tileIndex[x, y] = 0;
                    _entityIndex[x, y] = 0;
                }
            }
        }

        public void SaveMapData()
        {
            _mapData = new MapData(_mapWidth, _mapHeight, _tileIndex, _entityIndex, "Level 1");

            string strResultJson = JsonConvert.SerializeObject(_mapData);
            File.WriteAllText(@"MapData.json", strResultJson);

            strResultJson = string.Empty;
            strResultJson = File.ReadAllText(@"MapData.json");

            _mapData = JsonConvert.DeserializeObject<MapData>(strResultJson);
        }

        public void LoadMapData()
        {
            string strResultJson = File.ReadAllText(@"MapData.json");
            MapData newMapData = JsonConvert.DeserializeObject<MapData>(strResultJson);
            _mapData = newMapData;

            int index = 0;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if(index < tileList.Count)
                    {
                        tileList[index].TileTexture = tileTextureList[_mapData._tileMap[x, y]];

                        if(_mapData._entityMap[x, y] > 0)
                        {
                            Entity entity = entityList[index];

                            entity.EntityTexture = entityTextureList[_mapData._entityMap[x, y]];
                            entity.EntityRectangle = new Rectangle(entity.EntityRectangle.X, entity.EntityRectangle.Y, entity.EntityTexture.Width, entity.EntityTexture.Height);
                            entity.IsActive = true;
                        }

                        index++;
                        
                    }
                }
            }
        }
    }
}
