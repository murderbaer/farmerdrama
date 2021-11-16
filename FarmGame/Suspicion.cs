using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Suspicion : IDrawable
    {
        private float _value;

        public Suspicion(float value = 0)
        {
            Value = value;
        }

        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value > 100)
                {
                    _value = 100;
                }
                else if (value < 0)
                {
                    _value = 0;
                }
                else
                {
                    _value = value;
                }
            }
        }

        public void Draw()
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
            return _value / 100 * 2.6f;
        }
    }
}