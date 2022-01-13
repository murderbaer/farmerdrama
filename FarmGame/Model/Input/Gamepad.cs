using System.Collections.Generic;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

using FarmGame.Core;

namespace FarmGame.Model.Input
{
    public class Gamepad: IComponent, IInputDevice
    {
        private JoystickState _jsStates;

        public Gamepad(JoystickState gw)
        {
            _jsStates = gw;
        }

        public Vector2 PlayerDirection
        { 
            get 
            { 
                Vector2 ret = new Vector2(0,0);
                float xAxis = _jsStates.GetAxis(0);
                if (xAxis < -0.1 || xAxis > 0.1) 
                {
                    ret.X = xAxis;
                }

                float yAxis = _jsStates.GetAxis(1);
                if (yAxis < -0.1 || yAxis > 0.1) 
                {
                    ret.Y = yAxis;
                }
                return ret; 
            }
        }

         public Vector2 CameraDirection
        { 
            get 
            { 
                Vector2 ret = new Vector2(0,0);
                float xAxis = _jsStates.GetAxis(3);
                if (xAxis < -0.1 || xAxis > 0.1) 
                {
                    ret.X = xAxis;
                }

                float yAxis = _jsStates.GetAxis(4);
                if (yAxis < -0.1 || yAxis > 0.1) 
                {
                    ret.Y = yAxis;
                }
                return ret; 
            }
        }

        public bool Close
        {
            get
            {
                return _jsStates.IsButtonDown(6) && !_jsStates.WasButtonDown(6);
            }
        }

        public bool Pause
        {
            get
            {
                return _jsStates.IsButtonDown(7) && !_jsStates.WasButtonDown(7);
            }
        }

        public bool Interact
        {
            get
            {
                return _jsStates.IsButtonDown(0) && !_jsStates.WasButtonDown(0);
            }
        }

         public bool XAnswer
        {
            get
            {
                return _jsStates.IsButtonDown(2) && !_jsStates.WasButtonDown(2);
            }
        }

        public bool YAnswer
        {
            get
            {
                return _jsStates.IsButtonDown(3) && !_jsStates.WasButtonDown(3);
            }
        }
        
        public bool DetachCamera
        {
            get
            {
                return _jsStates.IsButtonDown(10);
            }
        }

        public bool Fullscreen
        {
            get
            {
                return _jsStates.IsButtonDown(8) && !_jsStates.WasButtonDown(8);
            }
        }
    }
}
