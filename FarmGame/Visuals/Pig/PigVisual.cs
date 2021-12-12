using FarmGame.Core;
using FarmGame.Helpers;

using ImageMagick;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class PigVisual : IDraw
    {
        private IPosition _position;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        private Box2 _texCoords;

        public PigVisual(GameObject goPig)
        {
            _position = goPig.GetComponent<IPosition>();
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.Pigs.png");
            Sprite = new SpriteObject(_spriteSheet, 1);
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
        }

        public SpriteObject Sprite { get; private set; }

        //TODO: use generic sprite draw method
        public void Draw()
        {
            Box2 spritePos = SpriteHelper.GetTexCoordFromSprite(Sprite, 20);
            GL.Color4(Color4.White);

            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(_position.Position.X - 0.5f, _position.Position.Y - 0.5f);

            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(_position.Position.X + 0.5f, _position.Position.Y - 0.5f);

            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(_position.Position.X + 0.5f, _position.Position.Y + 0.5f);

            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(_position.Position.X - 0.5f, _position.Position.Y + 0.5f);

            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}