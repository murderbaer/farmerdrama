namespace FarmGame
{
    public class PlayerAnimation : IAnimate
    {
        private SpriteObject _playerSprite;

        private float _animationDuration = 0.4f;

        private float _animationCurrentTime = 0f;

        private bool _animationSwitchSprite = true;

        private int _moveUp = 4;
        private int _moveDown = 2;
        private int _moveLeft = 7;
        private int _moveRight = 10;

        public PlayerAnimation(GameObject goPlayer)
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
            if (e.Direction.X == 1)
            {
                _playerSprite.Gid = _moveRight;
            }
            else if (e.Direction.X == -1)
            {
                _playerSprite.Gid = _moveLeft;
            }
            else if (e.Direction.Y == 1)
            {
                _playerSprite.Gid = _moveDown;
            }
            else if (e.Direction.Y == -1)
            {
                _playerSprite.Gid = _moveUp;
            }
            else
            {
                _playerSprite.Gid = 1;
            }
        }
    }
}