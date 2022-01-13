using System.Collections.Generic;

using FarmGame.Core;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FarmGame.Model.Input
{
    public class InputHandler : IComponent, IInput, IUpdatable
    {
        private static InputHandler _ih = null;

        private static Dictionary<int, IInputDevice> _inputDevices = new Dictionary<int, IInputDevice>();

        public static GameWindow GameWindow { get; private set; } = null;

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

        public Vector2 PlayerDirection { get; private set; }

        public Vector2 CameraDirection { get; private set; }

        public bool Close { get; private set; }

        public bool Pause { get; private set; }

        public bool Interact { get; private set; }

        public bool DetachCamera { get; private set; }

        public bool XAnswer { get; private set; }

        public bool YAnswer { get; private set; }

        public bool Fullscreen { get; private set; }

        public static void RegisterGameWindow(GameWindow gw)
        {
            GameWindow = gw;

            var keyboard = new Keyboard();
            _inputDevices.Add(-1, keyboard);

            GameWindow.JoystickConnected += GamePadConnection;

            GameWindow.KeyDown += keyboard.KeyDown;
            GameWindow.KeyUp += keyboard.KeyUp;
        }

        public void Update(float elapsedTime)
        {
            foreach (var device in _inputDevices)
            {
                PlayerDirection = device.Value.PlayerDirection;
                CameraDirection = device.Value.CameraDirection;
                Close = device.Value.Close;
                DetachCamera = device.Value.DetachCamera;
                Fullscreen = device.Value.Fullscreen;
                Interact = device.Value.Interact;
                XAnswer = device.Value.XAnswer;
                YAnswer = device.Value.YAnswer;
                Fullscreen = device.Value.Fullscreen;
            }

            if (Close)
            {
                GameWindow.Close();
            }

            if (Fullscreen)
            {
                GameWindow.WindowState = GameWindow.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
            }
        }

        private static void GamePadConnection(JoystickEventArgs e)
        {
            if (e.IsConnected)
            {
                _inputDevices.Add(e.JoystickId, new Gamepad(GameWindow.JoystickStates[e.JoystickId]));
            }
            else
            {
                _inputDevices.Remove(e.JoystickId);
            }
        }
    }
}