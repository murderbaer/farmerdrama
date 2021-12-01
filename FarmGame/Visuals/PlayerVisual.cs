using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

using System;
namespace FarmGame
{
    public class PlayerVisual : IDrawable, IUpdatable
    {
        private IPosition _position;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        private SpriteObject _playerSprite;

        private float _animationDuration = 0.4f;

        private float _animationCurrentTime = 0f;

        private bool _animationSwitchSprite;

        private int _moveUp = 4;
        private int _moveDown = 2;
        private int _moveLeft = 7;
        private int _moveRight = 10;

        public PlayerVisual(GameObject goPlayer)
        {
            _position = goPlayer.GetComponent<IPosition>();
            goPlayer.GetComponent<IChangeDirection>().OnChangeDirection += Animate;
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.FarmPerson.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
            _playerSprite = new SpriteObject(_spriteSheet, 1);
        }
        
        public void Animate(object sender, OnChangeDirectionArgs e)
        {
            if (e.Direction.X == 1)
            {
                _playerSprite.Gid = _moveRight;
            } else if (e.Direction.X == -1 )
            {
                _playerSprite.Gid = _moveLeft;
            }else if (e.Direction.Y == 1)
            {
                _playerSprite.Gid = _moveDown;
            } else if (e.Direction.Y == -1)
            {
                _playerSprite.Gid = _moveUp;
            } else 
            {
                _playerSprite.Gid = 1;
            }
        }

        public void Update(float elapsedTime)
        {
            _animationCurrentTime += elapsedTime;
            if(_animationCurrentTime > _animationDuration)
            {
                _animationCurrentTime = 0f;
                if (_animationSwitchSprite)
                {
                    _moveUp = 4;
                    _moveDown = 2;
                    _moveLeft = 7;
                    _moveRight = 10;
                } else 
                {
                    _moveUp = 6;
                    _moveDown = 3;
                    _moveLeft = 9;
                    _moveRight = 12;
                }

                _animationSwitchSprite = !_animationSwitchSprite;
            }
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