using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace LevelEditor
{
    public class MapData
    {
        public int mapHeight;
        public int mapWidth;

        public int[,] tileMap;
        public int[,] entityMap;
        public List<int> assetList = new List<int>();
        public List<Vector2> assetPositionList = new List<Vector2>();
        public string levelName;

        public MapData(int mapWidth, int mapHeight, int[,] tileMap, int[,] entityMap, string levelName, List<int> assetList, List<Vector2> assetPositionList)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileMap = tileMap;
            this.entityMap = entityMap;
            this.levelName = levelName;
            this.assetList = assetList;
            this.assetPositionList = assetPositionList;
        }
    }
}
