using FarmGame.Core;

namespace FarmGame.Visuals
{
    public class PlayerAnimation : IUpdatable
    {
        private SpriteObject _playerSprite;

        private float _animationDuration = 0.4f;

        private float _animationCurrentTime = 0f;

        private bool _animationSwitchSprite = true;

        private int _moveUp = 4;

        private int _moveDown = 2;

        private int _moveLeft = 7;

        private int _moveRight = 10;

        private int _noMove = 1;

        private IMoving _direction;

        public PlayerAnimation(GameObject goPlayer)
        {
            _playerSprite = goPlayer.GetComponent<IDraw>().Sprite;
            _direction = goPlayer.GetComponent<IMoving>();
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
                    _moveRight = 14;
                }
                else
                {
                    _moveUp = 6;
                    _moveDown = 3;
                    _moveLeft = 9;
                    _moveRight = 16;
                }

                _animationSwitchSprite = !_animationSwitchSprite;
            }

            Animate();
        }

        private void Animate()
        {
            if (_direction.MovementVector.X > 0)
            {
                _playerSprite.Gid = _moveRight;
                _noMove = 16;
            }
            else if (_direction.MovementVector.X < 0)
            {
                _playerSprite.Gid = _moveLeft;
                _noMove = 7;
            }
            else if (_direction.MovementVector.Y > 0)
            {
                _playerSprite.Gid = _moveDown;
                _noMove = 1;
            }
            else if (_direction.MovementVector.Y < 0)
            {
                _playerSprite.Gid = _moveUp;
                _noMove = 5;
            }
            else
            {
                _playerSprite.Gid = _noMove;
            }
        }
    }
}