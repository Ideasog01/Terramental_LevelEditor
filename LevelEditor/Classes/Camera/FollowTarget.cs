using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public class FollowTarget
    {
        private Vector2 _targetPosition;
        private Rectangle _targetRectangle;
        private Texture2D _targetTexture;

        public FollowTarget(Vector2 startPosition, Texture2D texture)
        {
            _targetPosition = startPosition;
            _targetRectangle = new Rectangle((int)startPosition.X, (int)startPosition.Y, 64, 64);

            _targetTexture = texture;
        }

        public void UpdatePosition()
        {
            _targetRectangle.X = (int)_targetPosition.X;
            _targetRectangle.Y = (int)_targetPosition.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_targetTexture, _targetRectangle, Color.White);
        }

        public Vector2 TargetPosition
        {
            get { return _targetPosition; }
            set { _targetPosition = value; }
        }

        public Rectangle TargetRectangle
        {
            get { return _targetRectangle; }
            set { _targetRectangle = value; }
        }
    }
}
