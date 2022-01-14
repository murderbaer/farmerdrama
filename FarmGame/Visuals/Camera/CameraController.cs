using System;
using System.Collections.Generic;

using FarmGame.Core;
using FarmGame.Model.Input;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Visuals
{
    public class CameraController : IUpdatable
    {
        private IPosition _followPosition;

        private SmoothCamera _smoothCamera;

        private Camera _camera;

        private InputHandler _input = InputHandler.Instance;

        public CameraController(GameObject goCamera)
        {
            _smoothCamera = goCamera.GetComponent<SmoothCamera>();
            _camera = goCamera.GetComponent<Camera>();
            goCamera.Components.Add(_input);
        }

        public bool FreeCamActive { get; set; }

        public float Speed { get; set; } = 10f;

        public Vector2 Focus { get; set; }

        public void Update(float deltaTime)
        {
            if (_input.DetachCamera)
            {
                Focus = _smoothCamera.CameraFocus;
                FreeCamActive = !FreeCamActive;
            }

            if (FreeCamActive)
            {
                var cameraMovement = _input.CameraDirection;
                Focus += cameraMovement * deltaTime * Speed;
                _smoothCamera.CameraFocus = Focus;
            }
            else
            {
                _smoothCamera.CameraFocus = _followPosition.Position;
            }
        }

        public void FollowGameObject(GameObject go, bool setPosition = false)
        {
            _followPosition = go.GetComponent<IPosition>();
            FreeCamActive = false;
            if (setPosition)
            {
                _smoothCamera.CameraFocus = _followPosition.Position;
                _camera.Position = _followPosition.Position;
            }
        }
    }
}