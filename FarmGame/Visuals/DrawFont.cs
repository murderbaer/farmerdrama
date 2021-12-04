using System.Linq;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class DrawFont : IComponent, IDrawFont
    {
        private const float HPADDING = 1f;

        private const float VPADDING = 7f;

        private const int CHAROFFSET = 31;

        private const float LINEEND = 10.5f;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        private string _textToDraw;

        public DrawFont(GameObject goText)
        {
            _textToDraw = goText.GetComponent<ITextToDraw>().Question;

            // sprite sheet found on https://stackoverflow.com/questions/43494615/use-a-font-sprite-sheet-as-font-in-sfml-2-4
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.Font.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
        }

        public void Draw()
        {
            char[] arr = _textToDraw.ToArray();
            float xOff = 0f;
            float yOff = 0f;
            for (int i = 0; i < arr.Length; i++)
            {
                Draw((int)arr[i] - CHAROFFSET, xOff, yOff);
                xOff += 0.5f;

                if (xOff >= LINEEND)
                {
                    yOff += 0.5f;
                    xOff = 0f;
                }
            }
        }

        private void Draw(int gid, float xOffset, float yOffset)
        {
            Box2 spritePos = SpriteHelper.GetTexCoordFromSprite(new SpriteObject(_spriteSheet, gid), 20);

            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Color4.White);

            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(HPADDING + xOffset, VPADDING + yOffset);

            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(HPADDING + xOffset + 0.5f, VPADDING + yOffset);

            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(HPADDING + xOffset + 0.5f, VPADDING + yOffset + 0.5f);

            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(HPADDING + xOffset, VPADDING + yOffset + 0.5f);

            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}