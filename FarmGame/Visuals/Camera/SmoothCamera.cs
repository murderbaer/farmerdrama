using FarmGame.Core;

using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class SmoothCamera : IUpdatable
    {
        private Camera _camera;

        public SmoothCamera(GameObject goCamera)
        {
            _camera = goCamera.GetComponent<Camera>();
            CameraFocus = _camera.CameraPosition;
        }

        public Vector2 CameraFocus { get; set; }

        public void Update(float elapsedTime)
        {
            Vector2 cameraOffset = CameraFocus - _camera.CameraPosition;
            Vector2 cameraMovement = cameraOffset * 2f * elapsedTime;
            _camera.CameraPosition += cameraMovement;
        }
    }
}