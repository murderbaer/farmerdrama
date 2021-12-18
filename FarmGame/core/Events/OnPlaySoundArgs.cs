using System;

using OpenTK.Mathematics;

namespace FarmGame.Core
{
    public class OnPlaySoundArgs
    {
        public OnPlaySoundArgs(Vector2 position, Vector2 speed)
        {
            Position = position;
            Speed = speed;
        }

        public Vector2 Position { get; private set; }

        public Vector2 Speed { get; private set; }
    }
}