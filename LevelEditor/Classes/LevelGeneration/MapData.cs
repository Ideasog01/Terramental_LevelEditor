using System;
using System.Collections.Generic;
using System.Text;

namespace LevelEditor
{
    public class MapData
    {
        public int _mapHeight;
        public int _mapWidth;

        public int[,] _tileMap;
        public int[,] _entityMap;
        public int[,] _assetMap;

        public string _levelName;

        public MapData(int mapWidth, int mapHeight, int[,] tileMap, int[,] entityMap, int[,] assetMap, string levelName)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _tileMap = tileMap;
            _entityMap = entityMap;
            _assetMap = assetMap;
            _levelName = levelName;
        }
    }
}
