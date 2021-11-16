using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Suspicion : IDrawable
    {
        public int Value { get; set; } = 0;

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(new Color4(28 / 255f, 22 / 255f, 11 / 255f, 1f));
            GL.Vertex2(12, 7);
            GL.Vertex2(12, 8);
            GL.Vertex2(15, 8);
            GL.Vertex2(15, 7);
            GL.Color4(Color4.Red);
            GL.Vertex2(12.2, 7.2);
            GL.Vertex2(12.2, 7.8);
            GL.Vertex2(14.8, 7.8);
            GL.Vertex2(14.8, 7.2);
            GL.End();
        }
    }
}