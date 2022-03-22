using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LevelEditor
{
    public class InputManager
    {
        private FollowTarget _camTarget;

        private CameraController _camController;

        private MapManager _mapManager;

        public InputManager(FollowTarget camTarget, MapManager mapManager, CameraController camController)
        {
            _camTarget = camTarget;
            _mapManager = mapManager;
            _camController = camController;
        }

        public void UpdateInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(mouseState.Position.X, mouseState.Position.Y, 1, 1);

            HighlightSelectedTile(_camController.ScreenToWorldSpace(new Vector2(mouseState.Position.X, mouseState.Position.Y)));

            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                LeftMouseButton(_camController.ScreenToWorldSpace(new Vector2(mouseState.Position.X, mouseState.Position.Y)));
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                RightMouseButton(_camController.ScreenToWorldSpace(new Vector2(mouseState.Position.X, mouseState.Position.Y)));
            }


            if (keyboardState.IsKeyDown(Keys.W))
            {
                _camTarget.TargetPosition += new Vector2(0, -5);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                _camTarget.TargetPosition += new Vector2(0, 5);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                _camTarget.TargetPosition += new Vector2(5, 0);
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                _camTarget.TargetPosition += new Vector2(-5, 0);
            }

            if(keyboardState.IsKeyDown(Keys.Enter))
            {
                _mapManager.SaveMapData();
            }
        }

        private void HighlightSelectedTile(Vector2 mousePosition)
        {
            foreach(Tile tile in _mapManager.tileList)
            {
                if(tile.TileRectangle.Contains(mousePosition))
                {
                    tile.TileColor = Color.Red;
                }
                else
                {
                    tile.TileColor = Color.White;
                }
            }
        }

        private void LeftMouseButton(Vector2 mousePosition)
        {
            foreach(Tile tile in _mapManager.tileList)
            {
                if(tile.TileRectangle.Contains(mousePosition))
                {
                    _mapManager.ChangeTile(tile, 1);
                }
            }
        }

        private void RightMouseButton(Vector2 mousePosition)
        {
            foreach (Tile tile in _mapManager.tileList)
            {
                if (tile.TileRectangle.Contains(mousePosition))
                {
                    _mapManager.ChangeTile(tile, 0);
                }
            }
        }
    }
}
