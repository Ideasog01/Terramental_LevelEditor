using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public class Tile
    {
        private Texture2D _tileTexture;
        private Vector2 _tilePosition;
        private Vector2 _tileScale;
        private Rectangle _tileRectangle;
        private Color _tileColor;

        private bool _isWall;
        private bool _isGround;

        public Rectangle TileRectangle
        {
            get { return _tileRectangle; }
        }

        public Texture2D TileTexture
        {
            get { return _tileTexture; }
            set { _tileTexture = value; }
        }

        public Color TileColor
        {
            get { return _tileColor; }
            set { _tileColor = value; }
        }

        public Tile(Texture2D texture, Vector2 position, Vector2 scale, bool isWall, bool isGround)
        {
            _tileTexture = texture;
            _tilePosition = position;
            _tileScale = scale;
            _isWall = isWall;
            _isGround = isGround;
            _tileColor = Color.White;

            _tileRectangle = new Rectangle((int)_tilePosition.X, (int)_tilePosition.Y, (int)_tileScale.X, (int)_tileScale.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tileTexture, _tileRectangle, _tileColor);
        }
    }
}
