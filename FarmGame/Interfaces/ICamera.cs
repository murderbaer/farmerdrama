using OpenTK.Mathematics;

namespace FarmGame
{
    public interface ICamera : IUpdatable
    {
        public int CameraWidth { get; }

        public int CameraHeight { get; }

        public Vector2 CameraSize { get; }

        public Vector2 CameraFocus { get; set; }

        public Vector2 CameraPosition { get; set; }

        public void Resize(int width, int height);

        public void SetOverlayMatrix();

        public void SetCameraMatrix();
    }
}