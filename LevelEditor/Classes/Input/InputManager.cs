using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public class InputManager
    {
        private FollowTarget _camTarget;

        private CameraController _camController;

        private MapManager _mapManager;

        private KeyboardState _currentKeyboardState = Keyboard.GetState();
        private MouseState _currentMouseState = Mouse.GetState();

        public InputManager(FollowTarget camTarget, MapManager mapManager, CameraController camController)
        {
            _camTarget = camTarget;
            _mapManager = mapManager;
            _camController = camController;
        }

        public void UpdateInput()
        {
            KeyboardState oldKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            MouseState oldMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(oldMouseState.Position.X, oldMouseState.Position.Y, 1, 1);

            HighlightSelectedTile(_camController.ScreenToWorldSpace(new Vector2(oldMouseState.Position.X, oldMouseState.Position.Y)));

            if(oldKeyboardState.IsKeyDown(Keys.Tab) && _currentKeyboardState.IsKeyUp(Keys.Tab))
            {
                if (MapManager.tileSelection)
                {
                    MapManager.tileSelection = false;
                }
                else
                {
                    MapManager.tileSelection = true;
                }

                foreach(Tile tile in _mapManager.tileList)
                {
                    tile.TileColor = Color.White;
                }

                foreach(Entity entity in _mapManager.entityList)
                {
                    entity.EntityColor = Color.White;
                }
            }

            if(oldKeyboardState.IsKeyDown(Keys.Space) && _currentKeyboardState.IsKeyUp(Keys.Space))
            {
                if(MapManager.tileSelection)
                {
                    int index = _mapManager.currentTileIndex;

                    index++;

                    if(index > _mapManager.tileTextureList.Count - 1)
                    {
                        index = 1;
                    }

                    _mapManager.currentTileIndex = index;
                }
                else
                {
                    int index = _mapManager.currentEntityIndex;

                    index++;

                    if (index > _mapManager.entityTextureList.Count - 1)
                    {
                        index = 1;
                    }

                    _mapManager.currentEntityIndex = index;
                }
            }

            if(oldKeyboardState.IsKeyDown(Keys.L) && _currentKeyboardState.IsKeyUp(Keys.L))
            {
                _mapManager.LoadMapData();
            }

            if (oldMouseState.LeftButton == ButtonState.Pressed && _currentMouseState.LeftButton == ButtonState.Released)
            {
                LeftMouseButton(_camController.ScreenToWorldSpace(new Vector2(oldMouseState.Position.X, oldMouseState.Position.Y)));
            }

            if (oldMouseState.RightButton == ButtonState.Pressed && _currentMouseState.RightButton == ButtonState.Released)
            {
                RightMouseButton(_camController.ScreenToWorldSpace(new Vector2(oldMouseState.Position.X, oldMouseState.Position.Y)));
            }

            if (oldKeyboardState.IsKeyDown(Keys.W))
            {
                _camTarget.TargetPosition += new Vector2(0, -5);
            }

            if (oldKeyboardState.IsKeyDown(Keys.S))
            {
                _camTarget.TargetPosition += new Vector2(0, 5);
            }

            if (oldKeyboardState.IsKeyDown(Keys.D))
            {
                _camTarget.TargetPosition += new Vector2(5, 0);
            }

            if (oldKeyboardState.IsKeyDown(Keys.A))
            {
                _camTarget.TargetPosition += new Vector2(-5, 0);
            }

            if(oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                _mapManager.SaveMapData();
            }
        }

        private void HighlightSelectedTile(Vector2 mousePosition)
        {
            if(MapManager.tileSelection)
            {
                foreach (Tile tile in _mapManager.tileList)
                {
                    if (tile.TileRectangle.Contains(mousePosition))
                    {
                        tile.TileColor = Color.Red;
                    }
                    else
                    {
                        tile.TileColor = Color.White;
                    }
                }
            }
            else
            {
                foreach(Entity entity in _mapManager.entityList)
                {
                    if(entity.EntityRectangle.Contains(mousePosition))
                    {
                        entity.EntityColor = Color.Red;
                    }
                    else
                    {
                        entity.EntityColor = Color.White;
                    }
                }
            }
        }

        private void LeftMouseButton(Vector2 mousePosition)
        {
            if(MapManager.tileSelection)
            {
                foreach (Tile tile in _mapManager.tileList)
                {
                    if (tile.TileRectangle.Contains(mousePosition))
                    {
                        _mapManager.ChangeTile(tile, false);
                    }
                }
            }
            else
            {
                foreach (Entity entity in _mapManager.entityList)
                {
                    if (entity.EntityRectangle.Contains(mousePosition))
                    {
                        _mapManager.ChangeEntity(entity, false);
                    }
                }
            }
        }

        private void RightMouseButton(Vector2 mousePosition)
        {
            if(MapManager.tileSelection)
            {
                foreach (Tile tile in _mapManager.tileList)
                {
                    if (tile.TileRectangle.Contains(mousePosition))
                    {
                        _mapManager.ChangeTile(tile, true);
                    }
                }
            }
            else
            {
                foreach (Entity entity in _mapManager.entityList)
                {
                    if (entity.EntityRectangle.Contains(mousePosition))
                    {
                        _mapManager.ChangeEntity(entity, true);
                    }
                }
            }
        }
    }
}
