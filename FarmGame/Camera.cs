using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Camera : ICamera
    {
        // Screen Ratio: 16:9 Screen Ratio is used on most computer screens. Static, black bars limit the view
        private static Vector2 _screenRatio = new Vector2(16, 9);

        // How many tiles are displayed in the camera at once
        private static Vector2 _tilesOnCamera = new Vector2(20, 20 / _screenRatio.X * _screenRatio.Y);

        private static Matrix4 _cameraCenterTransformation = Matrix4.CreateTranslation(_tilesOnCamera.X / 2, _tilesOnCamera.Y / 2, 0f);

        private Matrix4 _windowSizeTransformation = Matrix4.Identity;

        private Matrix4 _cameraPositionTransformation = Matrix4.Identity;

        private Matrix4 _cameraMatrix = Matrix4.Identity;

        private Matrix4 _overlayMatrix = Matrix4.Identity;

        private Vector2 _windowSize;

        private Vector2 _cameraSize;

        public int CameraWidth { get => (int)_cameraSize.X; }

        public int CameraHeight { get => (int)_cameraSize.Y; }

        public Vector2 CameraSize { get => _cameraSize; }

        public Vector2 CameraFocus { get; set; }

        public Vector2 CameraPosition { get; set; }

        public void Update(float elapsedTime, IWorld world)
        {
            Vector2 cameraOffset = CameraFocus - CameraPosition;
            Vector2 cameraMovement = cameraOffset * 2f * elapsedTime;
            CameraPosition += cameraMovement;
            _cameraPositionTransformation = Matrix4.CreateTranslation(-CameraPosition.X, -CameraPosition.Y, 0);
            UpdateCameraMatrix();
        }

        public void Resize(int width, int height)
        {
            _windowSize = _cameraSize = new Vector2(width, height);
            double screenRatio = _screenRatio.X / (double)_screenRatio.Y;
            double windowAspectRatio = _windowSize.X / (double)_windowSize.Y;
            if (windowAspectRatio > screenRatio)
            {
                // Black bars on the side
                int bar_width = (int)(_windowSize.X - (_windowSize.Y * screenRatio));
                GL.Viewport(bar_width / 2, 0, width - bar_width, height);
                _cameraSize.X = width - bar_width;
            }
            else if (windowAspectRatio < screenRatio)
            {
                // Black bars bottom and top
                int bar_height = (int)(_windowSize.Y - (_windowSize.X / screenRatio));
                GL.Viewport(0, bar_height / 2, width, height - bar_height);
                _cameraSize.Y = height - bar_height;
            }
            else
            {
                GL.Viewport(0, 0, width, height);
            }

            // Move Coordination System Origin to top left corner
            var translate = Matrix4.CreateTranslation(-1f, 1f, 0f);

            // Scale the Window to be per-pixel
            var scale = Matrix4.CreateScale(2f / _cameraSize.X, -2f / _cameraSize.Y, 1f);

            _windowSizeTransformation = scale * translate;
            UpdateCameraMatrix();
            UpdateOverlayMatrix();
        }

        public void UpdateCameraMatrix()
        {
            var scale = Matrix4.CreateScale(_cameraSize.X / _tilesOnCamera.X, _cameraSize.Y / _tilesOnCamera.Y, 1f);
            _cameraMatrix = _cameraPositionTransformation * _cameraCenterTransformation * scale * _windowSizeTransformation;
        }

        public void UpdateOverlayMatrix()
        {
            var scale = Matrix4.CreateScale(_cameraSize.X / (float)_screenRatio.X, _cameraSize.Y / (float)_screenRatio.Y, 1f);
            _overlayMatrix = scale * _windowSizeTransformation;
        }

        public void SetOverlayMatrix()
        {
            GL.LoadMatrix(ref _overlayMatrix);
        }

        public void SetCameraMatrix()
        {
            GL.LoadMatrix(ref _cameraMatrix);
        }
    }
}