using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class FreeCamComponent : IUpdatable
    {
        public bool IsActive { get; set; }

        public float Speed { get; set; } = 10f;

        public Vector2 Focus { get; set; }

        public void Update(float deltaTime, IWorld world)
        {
            if (IsActive)
            {
                var keyboard = world.Window.KeyboardState;

                var cameraMovement = new Vector2(0, 0);
                cameraMovement.X = (keyboard.IsKeyDown(Keys.D) ? 1 : 0) - (keyboard.IsKeyDown(Keys.A) ? 1 : 0);
                cameraMovement.Y = (keyboard.IsKeyDown(Keys.S) ? 1 : 0) - (keyboard.IsKeyDown(Keys.W) ? 1 : 0);

                Focus += cameraMovement * deltaTime * Speed;
            }

            world.Camera.CameraFocus = Focus;
        }

        public void OnKeyDown(IWorld world)
        {
            Focus = world.Camera.CameraFocus;
            IsActive = !IsActive;
        }
    }
}