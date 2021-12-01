using System;

using OpenTK.Mathematics;

namespace FarmGame
{
    public class OnChangeDirectionArgs : EventArgs
    {
        public OnChangeDirectionArgs(Vector2 direction)
        {
            Direction = direction;
        }
        
        public Vector2 Direction { get; private set; }
    }
}