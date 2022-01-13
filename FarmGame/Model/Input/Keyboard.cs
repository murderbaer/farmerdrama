using FarmGame.Core;
using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model.Input
{
    public class Keyboard : IInput
    {
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

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
                return _pressedKeys.Contains(Keys.F10);
            }
        }
         public bool Pause
        {
            get
            {
                return _pressedKeys.Contains(Keys.Escape);
            }
        }

        public bool DetachCamera
        {
            get
            {
                return _pressedKeys.Contains(Keys.F3);
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Add(args.Key);
        }

        public void KeyUp(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Remove(args.Key);
        }
    }
}