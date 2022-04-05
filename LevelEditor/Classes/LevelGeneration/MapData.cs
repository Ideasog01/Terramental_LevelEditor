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

        public string _levelName;

        public MapData(int mapWidth, int mapHeight, int[,] tileMap, int[,] entityMap, string levelName)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _tileMap = tileMap;
            _entityMap = entityMap;
            _levelName = levelName;
        }
    }
}
