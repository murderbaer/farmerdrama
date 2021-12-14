using FarmGame.Core;

namespace FarmGame.Visuals
{
    public class PoliceAnimation : IAnimate
    {
        private SpriteObject _playerSprite;

        private float _animationDuration = 0.4f;

        private float _animationCurrentTime = 0f;

        private bool _animationSwitchSprite = true;

        private int _moveUp = 17;
        private int _moveDown = 15;
        private int _moveLeft = 20;
        private int _moveRight = 23;
        private int _noMove = 23;

        public PoliceAnimation(GameObject goPlayer)
        {
            _playerSprite = goPlayer.GetComponent<IDraw>().Sprite;
        }

        public void Update(float elapsedTime)
        {
            _animationCurrentTime += elapsedTime;
            if (_animationCurrentTime > _animationDuration)
            {
                _animationCurrentTime = 0f;
                if (_animationSwitchSprite)
                {
                    _moveUp = 4;
                    _moveDown = 2;
                    _moveLeft = 7;
                    _moveRight = 10;
                }
                else
                {
                    _moveUp = 6;
                    _moveDown = 3;
                    _moveLeft = 9;
                    _moveRight = 12;
                }

                _animationSwitchSprite = !_animationSwitchSprite;
            }
        }

        public void Animate(object sender, OnChangeDirectionArgs e)
        {
            float tolerance = 0.01f;
            if (e.Direction.X > tolerance)
            {
                _playerSprite.Gid = _moveRight;
                _noMove = 12;
            }
            else if (e.Direction.X < -tolerance)
            {
                _playerSprite.Gid = _moveLeft;
                _noMove = 7;
            }
            else if (e.Direction.Y < -tolerance)
            {
                _playerSprite.Gid = _moveUp;
                _noMove = 1;
            }
            else if (e.Direction.Y > tolerance)
            {
                _playerSprite.Gid = _moveDown;
                _noMove = 5;
            }
            else
            {
                _playerSprite.Gid = _noMove;
            }
        }
    }
}