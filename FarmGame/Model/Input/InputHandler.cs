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
        private GameWindow _gameWindow;

        private Dictionary<int, IInput> _inputDevices = new Dictionary<int, IInput>();

        public Vector2 PlayerDirection { get; private set; }

        public bool Close { get; private set; }

        public InputHandler(GameWindow gw)
        {
            _gameWindow = gw;

            var keyboard = new Keyboard();
            _gameWindow.KeyDown += keyboard.KeyDown;
            _gameWindow.KeyUp += keyboard.KeyUp;
            _inputDevices.Add(-1, keyboard);

            _gameWindow.JoystickConnected += GamePadConnection;
        }

        public void Update(float elapsedTime)
        {
            foreach (var device in _inputDevices)
            {
                PlayerDirection = device.Value.PlayerDirection;
                Close = device.Value.Close;
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