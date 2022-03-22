using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelEditor
{
    public class CameraController
    {
        public Matrix Transform { get; private set; }
        public Vector2 cameraOffset;

        public void MoveCamera(FollowTarget target)
        {
            var offset = Matrix.CreateTranslation(Game1.screenWidth / 2, Game1.screenHeight / 2, 0);
            var position = Matrix.CreateTranslation(new Vector3(-target.TargetPosition.X - (target.TargetRectangle.Width / 2), -target.TargetPosition.Y - (target.TargetRectangle.Height / 2), 0));

            Transform = position * offset;
        }

        public Vector2 ScreenToWorldSpace(Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(Transform);
            return Vector2.Transform(point, invertedMatrix);
        }
    }
}
