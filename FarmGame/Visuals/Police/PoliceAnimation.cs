namespace FarmGame
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

        public PoliceAnimation(GameObject goPlayer)
        {
            _playerSprite = goPlayer.GetComponent<IDrawable>().Sprite;
        }

        public void Update(float elapsedTime)
        {
            _animationCurrentTime += elapsedTime;
            if (_animationCurrentTime > _animationDuration)
            {
                _animationCurrentTime = 0f;
                if (_animationSwitchSprite)
                {
                    _moveUp = 17;
                    _moveDown = 15;
                    _moveLeft = 20;
                    _moveRight = 23;
                }
                else
                {
                    _moveUp = 19;
                    _moveDown = 16;
                    _moveLeft = 22;
                    _moveRight = 25;
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
            }
            else if (e.Direction.X < -tolerance)
            {
                _playerSprite.Gid = _moveLeft;
            }
            else if (e.Direction.Y < -tolerance)
            {
                _playerSprite.Gid = _moveUp;
            }
            else if (e.Direction.Y > tolerance)
            {
                _playerSprite.Gid = _moveDown;
            }
            else
            {
                _playerSprite.Gid = 14;
            }
        }
    }
}