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

        private Tile _highlightedTile;
        private Entity _highlightedEntity;

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

            if(oldKeyboardState.IsKeyDown(Keys.I) && _currentKeyboardState.IsKeyUp(Keys.I))
            {
                if(MapManager.tileSelection && _highlightedTile != null)
                {
                    _mapManager.currentTileIndex = _mapManager.tileTextureList.IndexOf(_highlightedTile.TileTexture);
                    _highlightedTile = null;
                }
                
                if(!MapManager.tileSelection && _highlightedEntity != null)
                {
                    if(_highlightedEntity.IsActive)
                    {
                        _mapManager.currentEntityIndex = _mapManager.entityTextureList.IndexOf(_highlightedEntity.EntityTexture);
                        _highlightedEntity = null;
                    }
                }
            }

            if(oldKeyboardState.IsKeyDown(Keys.Right) && _currentKeyboardState.IsKeyUp(Keys.Right))
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

            if (oldKeyboardState.IsKeyDown(Keys.Left) && _currentKeyboardState.IsKeyUp(Keys.Left))
            {
                if (MapManager.tileSelection)
                {
                    int index = _mapManager.currentTileIndex;

                    index--;

                    if (index < 0)
                    {
                        index = _mapManager.tileTextureList.Count - 1;
                    }

                    _mapManager.currentTileIndex = index;
                }
                else
                {
                    int index = _mapManager.currentEntityIndex;

                    index--;

                    if (index < 0)
                    {
                        index = _mapManager.entityTextureList.Count - 1;
                    }

                    _mapManager.currentEntityIndex = index;
                }
            }

            if (oldKeyboardState.IsKeyDown(Keys.L) && _currentKeyboardState.IsKeyUp(Keys.L))
            {
                _mapManager.LoadMapData();
            }

            if(oldKeyboardState.IsKeyDown(Keys.F) && _currentKeyboardState.IsKeyUp(Keys.F))
            {
                _mapManager.FillMap();
            }

            if (oldMouseState.LeftButton == ButtonState.Pressed && MapManager.tileSelection)
            {
                LeftMouseButton(_camController.ScreenToWorldSpace(new Vector2(oldMouseState.Position.X, oldMouseState.Position.Y)));
            }

            if (oldMouseState.LeftButton == ButtonState.Pressed && _currentMouseState.LeftButton == ButtonState.Released && !MapManager.tileSelection)
            {
                LeftMouseButton(_camController.ScreenToWorldSpace(new Vector2(oldMouseState.Position.X, oldMouseState.Position.Y)));
            }

            if (oldMouseState.RightButton == ButtonState.Pressed)
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

            if(oldKeyboardState.IsKeyDown(Keys.Enter) && _currentKeyboardState.IsKeyUp(Keys.Enter))
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
                        _highlightedTile = tile;
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
                        _highlightedEntity = entity;
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
