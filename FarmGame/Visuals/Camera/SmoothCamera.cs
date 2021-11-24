using OpenTK.Mathematics;

namespace FarmGame
{
    public class SmoothCamera : IUpdatable
    {
        public SmoothCamera(GameObject goCamera)
        {
            _camera = goCamera.GetComponent<Camera>();
            CameraFocus = _camera.CameraPosition;
        }

        public void Update(float elapsedTime)
        {
            Vector2 cameraOffset = CameraFocus - _camera.CameraPosition;
            Vector2 cameraMovement = cameraOffset * 2f * elapsedTime;
            _camera.CameraPosition += cameraMovement;
        }

        public Vector2 CameraFocus { get; set; }

        private Camera _camera;
    }
}