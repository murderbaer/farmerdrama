using FarmGame.Core;
using FarmGame.Model;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class SuspicionBar : IDrawOverlay
    {
        private Suspicion _suspicion;

        public SuspicionBar(GameObject goSuspicion)
        {
            _suspicion = goSuspicion.GetComponent<Suspicion>();
        }

        public void DrawOverlay()
        {
            var barLength = GetBarLength();
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(new Color4(28 / 255f, 22 / 255f, 11 / 255f, 1f));
            GL.Vertex2(12, 7);
            GL.Vertex2(12, 8);
            GL.Vertex2(15, 8);
            GL.Vertex2(15, 7);
            GL.Color4(Color4.Red);
            GL.Vertex2(12.2, 7.2);
            GL.Vertex2(12.2, 7.8);
            GL.Vertex2(12.2 + barLength, 7.8);
            GL.Vertex2(12.2 + barLength, 7.2);
            GL.End();
        }

        private float GetBarLength()
        {
            return _suspicion.Value / 100 * 2.6f;
        }
    }
}