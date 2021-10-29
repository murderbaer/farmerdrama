using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace FarmGame
{
    public class Player
    {
        public Vector2 Position { get; set; } = new ();

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;
    }
}