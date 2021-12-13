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

        // private Box2 _texCoords;
        public PigVisual(GameObject goPig)
        {
            _position = goPig.GetComponent<IPosition>();
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.Pigs.png");
            Sprite = new SpriteObject(_spriteSheet, 1, 20, isPlayer: true);
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
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