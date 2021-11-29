using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Background : IDrawBackground
    {
        public void DrawBackground()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Color4.Cyan);
            GL.Vertex2(0, 0);
            GL.Vertex2(16, 0);
            GL.Vertex2(16, 9);
            GL.Vertex2(0, 9);
            GL.End();
        }
    }
}