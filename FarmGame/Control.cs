using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace FarmGame {
    internal class Control {
        private readonly Model _model;
        private readonly View _view;
        public Control(Model model, View view)
        {
            _model = model;
            _view = view;
        }

        public void Update(float elapsedTime, KeyboardState keyboard)
        {
            MovePlayer(elapsedTime, keyboard);
        }

        private void MovePlayer(float elapsedTime, KeyboardState keyboard)
        {
            Vector2 moveDirection = new ();
            moveDirection.X = (keyboard.IsKeyDown(Keys.Right) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Left) ? 1 : 0);
            moveDirection.Y = (keyboard.IsKeyDown(Keys.Down) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Up) ? 1 : 0);
            if (moveDirection.X == 0 && moveDirection.Y == 0)
            {
                return;
            }

            moveDirection.Normalize();
            _model.Player.Position += moveDirection * elapsedTime * _model.Player.MovementSpeed;
        }
    }
}