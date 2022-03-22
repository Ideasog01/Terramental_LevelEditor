using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


namespace LevelEditor
{
    public class MapManager
    {
        public List<Tile> tileList = new List<Tile>();
        public List<Texture2D> tileTextureList = new List<Texture2D>();

        private int _mapWidth;
        private int _mapHeight;
        
        private int[,] _tileIndex;
        private int[,] _entityIndex;

        private MapData _mapData;

        public MapManager(int mapWidth, int mapHeight, List<Texture2D> textureList)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            tileTextureList = textureList;
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
        }

        public void ChangeTile(Tile tile, int index)
        {
            int tileIndex = tileList.IndexOf(tile);

            if(index < tileTextureList.Count && tileIndex < tileList.Count)
            {
                tileList[tileIndex].TileTexture = tileTextureList[index];
                _tileIndex[tile.TileRectangle.X / 64, tile.TileRectangle.Y / 64] = index;
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

        }
    }
}
