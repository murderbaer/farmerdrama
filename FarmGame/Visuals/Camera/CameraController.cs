using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class CameraController : IUpdatable, IKeyDownListener, IKeyUpListener
    {
        private IPosition _followPosition;

        private SmoothCamera _smoothCamera;

        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        public CameraController(GameObject goCamera)
        {
            _smoothCamera = goCamera.GetComponent<SmoothCamera>();
        }

        public bool FreeCamActive { get; set; }

        public float Speed { get; set; } = 10f;

        public Vector2 Focus { get; set; }

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

        public void FollowGameObject(GameObject go)
        {
            _followPosition = go.GetComponent<IPosition>();
            FreeCamActive = false;
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