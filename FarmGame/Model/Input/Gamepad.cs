using System.Collections.Generic;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

using FarmGame.Core;

namespace FarmGame.Model.Input
{
    public class Gamepad: IComponent, IInput
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
                System.Console.WriteLine(ret);
                return ret; 
            }
        }

        public bool Close
        {
            get
            {
                return _jsStates.IsButtonDown(7);
            }
        }

        public void Update(float elapsedTime)
        {
        }
    }
}
