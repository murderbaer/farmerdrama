using FarmGame.Core;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class CorpseVisual : IDraw
    {
        private IPosition _position;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        public CorpseVisual(GameObject goCorpse)
        {
            _position = goCorpse.GetComponent<IPosition>();
            _spriteSheet = SpriteRenderer.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.FarmPerson.png");
            _spriteHandle = SpriteRenderer.GenerateHandle(_spriteSheet);
            Sprite = new SpriteObject(_spriteSheet, 27, 16, isCentered: true);
        }

        public SpriteObject Sprite { get; private set; }

        public void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            SpriteRenderer.DrawSprite(Sprite, _position.Position);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}