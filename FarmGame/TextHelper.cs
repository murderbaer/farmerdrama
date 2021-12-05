using System;
using ImageMagick;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace FarmGame
{
    public class TextHelper
    {
        private const int CHAROFFSET = 31;

        private const float CHARWIDTH = 0.35f;

        private const float CHARHEIGHT = 0.35f;

        private const float CHARSPACE = 0.27f;

        private const float LINESPACING = 0.2f;

        private static TextHelper _instance;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        public TextHelper()
        {
            if (_instance != null)
            {
                throw new UnauthorizedAccessException("No second object is allowed to be created!");
            }

            // sprite sheet found on https://stackoverflow.com/questions/43494615/use-a-font-sprite-sheet-as-font-in-sfml-2-4
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.Font.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
        }

        public static TextHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TextHelper();
                }

                return _instance;
            }
        }

        public void DrawChar(char c, float x, float y, Color4 color)
        {
            Box2 spritePos = SpriteHelper.GetTexCoordFromSprite(new SpriteObject(_spriteSheet, c - CHAROFFSET), 20);

            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(color);

            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(x, y);

            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(x + CHARWIDTH, y);

            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(x + CHARWIDTH, y + CHARHEIGHT);

            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(x, y + CHARHEIGHT);

            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void DrawText(string text, float x, float y, Color4 color)
        {
            ParseText(ref text);
            float xOffset = x;
            float yOffset = y;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    xOffset = x;
                    yOffset += CHARHEIGHT + LINESPACING;
                }
                else
                {
                    DrawChar(text[i], xOffset, yOffset, color);
                    xOffset += CHARSPACE;
                }
            }
        }

        private void ParseText(ref string text)
        {
            text = text.Replace("\\n", "\n");
        }
    }
}