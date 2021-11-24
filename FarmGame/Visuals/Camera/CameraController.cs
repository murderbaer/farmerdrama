using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;


namespace FarmGame
{
    public class CameraController : IUpdatable, IKeyDownListener, IKeyUpListener
    {
        public CameraController(GameObject goCamera)
        {
            _smoothCamera = goCamera.GetComponent<SmoothCamera>();
        }

        public void FollowGameObject(GameObject go)
        {
            _followPosition = go.GetComponent<IPosition>();
            FreeCamActive = false;
        }

        private IPosition _followPosition;

        public bool FreeCamActive { get; set; }

        public float Speed { get; set; } = 10f;

        public Vector2 Focus { get; set; }

        private SmoothCamera _smoothCamera;

        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();


        public void Update(float deltaTime)
        {
            if (FreeCamActive)
            {
                var cameraMovement = new Vector2(0, 0);
                cameraMovement.X = (_pressedKeys.Contains(Keys.D) ? 1 : 0) - (_pressedKeys.Contains(Keys.A) ? 1 : 0);
                cameraMovement.Y = (_pressedKeys.Contains(Keys.S) ? 1 : 0) - (_pressedKeys.Contains(Keys.W) ? 1 : 0);

                Focus += cameraMovement * deltaTime * Speed;
                _smoothCamera.CameraFocus = Focus;

            }
            else
            {
                _smoothCamera.CameraFocus = _followPosition.Position;
            }

        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (args.Key == Keys.P)
            {
                Focus = _smoothCamera.CameraFocus;
                FreeCamActive = !FreeCamActive;
            }
            _pressedKeys.Add(args.Key);
        }

        public void KeyUp(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Remove(args.Key);
        }
    }
}