using FarmGame.Core;

namespace FarmGame.Visuals
{
    public class PigAnimation : IUpdatable
    {
        private ISpriteObject _pigSprite;

        private float _animationDuration = 0.4f;

        private float _animationCurrentTime = 0f;

        private bool _animationSwitchSprite = true;

        private int _moveUp = 4;

        private int _moveDown = 2;

        private int _moveLeft = 7;

        private int _moveRight = 10;

        private int _noMove = 1;

        private IMoving _direction;

        public PigAnimation(GameObject goPig)
        {
            _pigSprite = goPig.GetComponent<IDraw>().Sprite;
            _direction = goPig.GetComponent<IMoving>();
        }

        public void Update(float elapsedTime)
        {
            _animationCurrentTime += elapsedTime;
            if (_animationCurrentTime > _animationDuration)
            {
                _animationCurrentTime = 0f;
                if (_animationSwitchSprite)
                {
                    _moveUp = 6;
                    _moveDown = 2;
                    _moveLeft = 14;
                    _moveRight = 10;
                }
                else
                {
                    _moveUp = 8;
                    _moveDown = 4;
                    _moveLeft = 16;
                    _moveRight = 12;
                }

                _animationSwitchSprite = !_animationSwitchSprite;
            }

            Animate();
        }

        private void Animate()
        {
            if (_direction.MovementVector.X > 0)
            {
                _pigSprite.Gid = _moveRight;
                _noMove = 9;
            }
            else if (_direction.MovementVector.X < 0)
            {
                _pigSprite.Gid = _moveLeft;
                _noMove = 13;
            }
            else if (_direction.MovementVector.Y < 0)
            {
                _pigSprite.Gid = _moveUp;
                _noMove = 5;
            }
            else if (_direction.MovementVector.Y > 0)
            {
                _pigSprite.Gid = _moveDown;
                _noMove = 1;
            }
            else
            {
                _pigSprite.Gid = _noMove;
            }
        }
    }
}