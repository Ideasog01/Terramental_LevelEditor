using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    class Button
    {
        private int _buttonIndex;

        private Texture2D _buttonTexture;
        private Vector2 _buttonPosition;
        private Vector2 _buttonScale;
        private Rectangle _buttonRectangle;

        public void InitialiseButton(Texture2D texture, Vector2 position, Vector2 scale, Rectangle rectangle, int index)
        {
            _buttonTexture = texture;
            _buttonPosition = position;
            _buttonScale = scale;
            _buttonRectangle = rectangle;
            _buttonIndex = index;
        }

        public void Interact()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buttonTexture, _buttonRectangle, Color.White);
        }
    }
}
