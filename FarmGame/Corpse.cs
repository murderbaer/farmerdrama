using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Corpse : IUpdatable, IDrawable
    {
        public Corpse()
        {
            Position = new Vector2(6, 6);
        }

        public bool IsPlaced { get; set; } = true;

        public Vector2 Position { get; set; }

        public void Update(float elapsedTime, IWorld world)
        {
            if (!IsPlaced)
            {
                Position = world.Player.Position;
            }
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(Color4.WhiteSmoke);
            GL.Vertex2(Position.X - 0.2, Position.Y + 0.2);
            GL.Vertex2(Position.X + 0.2, Position.Y + 0.2);
            GL.Vertex2(Position.X, Position.Y - .2);
            GL.End();
        }
    }
}