using FarmGame.Core;

using FarmGame.Model;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class SuspicionBar : IDrawOverlay
    {
        private Suspicion _suspicion;
        private string _spriteName;

        public SuspicionBar(GameObject goSuspicion)
        {
            _suspicion = goSuspicion.GetComponent<Suspicion>();
            _spriteName = "FarmGame.Resources.Graphics.SpriteSheets.Gui.png";
        }

        public void DrawOverlay()
        {
            var texCoords = SpriteRenderer.GetTexCoord(_spriteName, 16, 155);
            SpriteRenderer.DrawSprite(_spriteName, new Vector2(12, 7), 1, texCoords);
            texCoords = SpriteRenderer.GetTexCoord(_spriteName, 16, 156);
            SpriteRenderer.DrawSprite(_spriteName, new Vector2(13, 7), 1, texCoords);
            var barLength = GetBarLength();
            GL.Color4(Color4.Red);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(12.188, 7.25);
            GL.Vertex2(12.188, 7.8125);
            GL.Vertex2(12.188 + barLength, 7.8125);
            GL.Vertex2(12.188 + barLength, 7.25);
            GL.End();
            texCoords = SpriteRenderer.GetTexCoord(_spriteName, 16, 168);
            SpriteRenderer.DrawSprite(_spriteName, new Vector2(12, 7), 1, texCoords);
            texCoords = SpriteRenderer.GetTexCoord(_spriteName, 16, 169);
            SpriteRenderer.DrawSprite(_spriteName, new Vector2(13, 7), 1, texCoords);
        }

        private float GetBarLength()
        {
            return _suspicion.Value / 100 * 1.615f;
        }
    }
}