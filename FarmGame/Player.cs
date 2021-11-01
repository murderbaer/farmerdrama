using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace FarmGame
{
    public class Player
    {
        public Vector2 Position { get; set; }

        public Player() {
            // Set starting position
            Position = new(12, 12);
        }
        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;
    }
}