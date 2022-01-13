using System.Collections.Generic;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

using FarmGame.Core;

namespace FarmGame.Model.Input
{
    public class InputHandler : IComponent, IInput, IUpdatable
    {
        // TODO make static so no reference needd to be given 
        public static GameWindow _gameWindow { get; private set; } = null;

        private Dictionary<int, IInput> _inputDevices = new Dictionary<int, IInput>();

        public Vector2 PlayerDirection { get; private set; }
        
        public Vector2 CameraDirection { get; private set; }

        public bool Close { get; private set; }

        public bool Pause { get; private set; }

        public bool DetachCamera { get; private set; }

        private static InputHandler _ih = null;

        private  InputHandler()
        {
            if (_gameWindow == null)
            {
                throw new System.Exception("Init needs to be called first");
            }
            var keyboard = new Keyboard();
            _gameWindow.KeyDown += keyboard.KeyDown;
            _gameWindow.KeyUp += keyboard.KeyUp;
            _inputDevices.Add(-1, keyboard);

            _gameWindow.JoystickConnected += GamePadConnection;
        }

        public static InputHandler Instance
        {
            get 
            {
                if (_ih == null)
                {
                    _ih = new InputHandler();
                }
                return _ih;
            }
        }

        public static void Init(GameWindow gw)
        {
            _gameWindow = gw;
        }

        public void Update(float elapsedTime)
        {
            foreach (var device in _inputDevices)
            {
                PlayerDirection = device.Value.PlayerDirection;
                CameraDirection = device.Value.CameraDirection;
                Close = device.Value.Close;
                Pause = device.Value.Pause;
                DetachCamera = device.Value.DetachCamera;
            }
            if (Close)
            {
                _gameWindow.Close();
            }
        }

        private void GamePadConnection(JoystickEventArgs e)
        {
            if (e.IsConnected)
            {
                _inputDevices.Add(e.JoystickId, new Gamepad(_gameWindow.JoystickStates[e.JoystickId]));
            }
            else
            {
                _inputDevices.Remove(e.JoystickId);
            } 
        }
    }
}