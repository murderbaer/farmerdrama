using FarmGame.Core;

using FarmGame.Model;
using FarmGame.Model.Grid;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class CarryItemVisual : IDraw, IComponent
    {
        private IPosition _position;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        private PlayerItemInteraction _playerItem;

        public CarryItemVisual(PlayerItemInteraction playerItem, GameObject goPlayer)
        {
            _playerItem = playerItem;
            _position = goPlayer.GetComponent<IPosition>();
            _spriteSheet = SpriteRenderer.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.global.png");
            _spriteHandle = SpriteRenderer.GenerateHandle(_spriteSheet);
            Sprite = new SpriteObject(_spriteSheet, (int)SpriteType.AIR, 16, isCentered: true);
        }

        public ISpriteObject Sprite { get; private set; }

        public void Draw()
        {
            switch (_playerItem.ItemInHand)
            {
                case ItemType.EMPTY:
                    Sprite.Gid = (int)SpriteType.AIR;
                    return;
                case ItemType.SEED:
                    Sprite.Gid = 5900;
                    break;
                case ItemType.WATERBUCKET:
                    Sprite.Gid = 1038;
                    break;
                case ItemType.WHEET:
                    Sprite.Gid = 5997;
                    break;
                default:
                    return;
            }

            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            SpriteRenderer.DrawSprite(Sprite, new Vector2(_position.Position.X, _position.Position.Y - 1));
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}