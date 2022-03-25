using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public class Entity
    {
        private Texture2D _entityTexture;
        private Vector2 _entityPosition;
        private Vector2 _entityScale;
        private Rectangle _entityRectangle;
        private Color _entityColor;
        private bool _isActive;

        public Entity(Texture2D texture, Vector2 position, Vector2 scale)
        {
            _entityTexture = texture;
            _entityPosition = position;
            _entityScale = scale;

            _entityRectangle = new Rectangle((int)_entityPosition.X, (int)_entityPosition.Y, (int)_entityScale.X, (int)_entityScale.Y);
        }

        public Rectangle EntityRectangle
        {
            get { return _entityRectangle; }
            set { _entityRectangle = value; }
        }

        public Texture2D EntityTexture
        {
            get { return _entityTexture; }
            set { _entityTexture = value; }
        }

        public Color EntityColor
        {
            get { return _entityColor; }
            set { _entityColor = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_isActive)
            {
                spriteBatch.Draw(_entityTexture, _entityRectangle, _entityColor);
            }
        }
    }
}
