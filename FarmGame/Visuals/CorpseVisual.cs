using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class CorpseVisual : IDrawable
    {
        private IPosition _position;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        private SpriteObject _playerSprite;

        public CorpseVisual(GameObject goCorpse)
        {
            _position = goCorpse.GetComponent<IPosition>();
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.FarmPerson.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
            _playerSprite = new SpriteObject(_spriteSheet, 27);
        }

        public void Draw()
        {
            Box2 spritePos = SpriteHelper.GetTexCoordFromSprite(_playerSprite);
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