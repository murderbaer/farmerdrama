using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace FarmGame
{
    public class Player
    {
        public Vector2 Position { get; set; } = new ();

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        internal void Draw()
        {
            GL.Color4(Color4.Orange);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(Position.X - 0.5, Position.Y);
            GL.Vertex2(Position.X + 0.5, Position.Y);
            GL.Vertex2(Position.X, Position.Y - 1);
            GL.End();
        }
    }
}