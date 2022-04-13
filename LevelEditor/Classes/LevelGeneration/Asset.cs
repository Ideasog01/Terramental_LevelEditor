using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public class Asset
    {
        private Texture2D _assetTexture;
        private Vector2 _assetPosition;
        private Vector2 _assetScale;
        private Rectangle _assetRectangle;
        private Color _assetColor;
        private bool _isActive;

        public Asset(Texture2D texture, Vector2 position, Vector2 scale)
        {
            _assetTexture = texture;
            _assetPosition = position;
            _assetScale = scale;

            _assetRectangle = new Rectangle((int)_assetPosition.X, (int)_assetPosition.Y, (int)_assetScale.X, (int)_assetScale.Y);
        }

        public Vector2 AssetPosition
        {
            get { return _assetPosition; }
            set { _assetPosition = value; }
        }

        public Rectangle AssetRectangle
        {
            get { return _assetRectangle; }
            set { _assetRectangle = value; }
        }

        public Texture2D AssetTexture
        {
            get { return _assetTexture; }
            set { _assetTexture = value; }
        }

        public Color AssetColor
        {
            get { return _assetColor; }
            set { _assetColor = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(_assetTexture, _assetRectangle, _assetColor);
            }
        }
    }
}
