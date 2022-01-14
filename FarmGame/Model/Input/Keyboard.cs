using System.Collections.Generic;

using FarmGame.Core;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model.Input
{
    public class Keyboard : IInputDevice
    {
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        private GameWindow _gameWindow;

        public Keyboard(GameWindow gw)
        {
            _gameWindow = gw;
        }

        public Vector2 PlayerDirection
        {
            get
            {
                Vector2 moveDirection = new ();
                moveDirection.X = (_pressedKeys.Contains(Keys.Right) ? 1 : 0) - (_pressedKeys.Contains(Keys.Left) ? 1 : 0);
                moveDirection.Y = (_pressedKeys.Contains(Keys.Down) ? 1 : 0) - (_pressedKeys.Contains(Keys.Up) ? 1 : 0);
                return moveDirection;
            }
        }

        public Vector2 CameraDirection
        {
            get
            {
                Vector2 moveDirection = new ();
                moveDirection.X = (_pressedKeys.Contains(Keys.D) ? 1 : 0) - (_pressedKeys.Contains(Keys.A) ? 1 : 0);
                moveDirection.Y = (_pressedKeys.Contains(Keys.S) ? 1 : 0) - (_pressedKeys.Contains(Keys.W) ? 1 : 0);
                return moveDirection;
            }
        }

        public bool Close
        {
            get
            {
                return _gameWindow.IsKeyPressed(Keys.F10);
            }
        }

        public bool Pause
        {
            get
            {
                return _gameWindow.IsKeyPressed(Keys.Escape);
            }
        }

        public bool Interact
        {
            get
            {
                return _gameWindow.IsKeyPressed(Keys.Space);
            }
        }

        public bool XAnswer
        {
            get
            {
                return _gameWindow.IsKeyPressed(Keys.X);
            }
        }

        public bool YAnswer { get; private set; }

        public bool DetachCamera
        {
            get
            {
                return _gameWindow.IsKeyPressed(Keys.F3);
            }
        }

        public bool Fullscreen
        {
            get
            {
                return _gameWindow.IsKeyPressed(Keys.F9);
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Add(args.Key);
            if (GLFW.GetKeyName(args.Key, args.ScanCode) == "y")
            {
                YAnswer = true;
            }
        }

        public void KeyUp(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Remove(args.Key);
            if (GLFW.GetKeyName(args.Key, args.ScanCode) == "y")
            {
                YAnswer = false;
            }
        }
    }
}