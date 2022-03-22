using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LevelEditor
{
    class Editor
    {
        private TileButton _tileButtonOne;
        private TileButton _tileButtonTwo;
        private TileButton _tileButtonThree;

        private Button _rightCycleButton;
        private Button _leftCycleButton;

        private Button _exportButton;

        private Button _environmentTilesButton;
        private Button _obstacleTilesButton;
        private Button _enemyTilesButton;
        private Button _miscTilesButton;

        private int _tileTypeIndex;
        private int _tileIndex;

        private List<Button> _buttonList = new List<Button>();

        private Game1 _game1;

        public Editor(Game1 game1)
        {
            _game1 = game1;

            //_tileButtonOne = new TileButton();
          //  _tileButtonOne.InitialiseButton(_game1.GetTexture("Buttons/TileButton"), new Vector2(0, 0), new Vector2(64, 64), new Rectangle(0, 0, 64, 64), 0);
            //_buttonList.Add(_tileButtonOne);
            

        }

        public void DrawButtons(SpriteBatch spriteBatch)
        {
            foreach(Button button in _buttonList)
            {
                button.Draw(spriteBatch);
            }
        }

        public void NewButton()
        {

        }
    }
}
