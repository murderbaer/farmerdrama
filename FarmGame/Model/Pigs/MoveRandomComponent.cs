using System;

using FarmGame.Core;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public enum MoveRandomComponentState
    {
        Idle,
        Moving,
    }

    public class MoveRandomComponent : IPosition, IUpdatable, IMoving, ICollidable
    {
        private Box2 _bounds;
        private Random _random = new Random();

        private Vector2 _moveGoal;

        private float _timeToWait = 0;

        private float _timeTicker = 0;

        private MoveRandomComponentState _state = MoveRandomComponentState.Idle;

        public MoveRandomComponent(Box2 bounds)
        {
            _bounds = bounds;
            Position = GetRandomPosition();
        }

        // TODO put in Hunger so more noise when pigs are hungry
        public event EventHandler<OnPlaySoundArgs> OnPlaySound;

        public event EventHandler<OnChangeDirectionArgs> OnChangeDirection;

        public Vector2 Position { get; set; }

        public Vector2 MovementVector { get; set; }

        public float MovementSpeed { get; set; } = 1f;

        public float CollisionRadius { get; set; } = 0.5f;

        public Vector2 GetRandomPosition()
        {
            var x = _bounds.Min.X + (float)(_random.NextDouble() * (_bounds.Max.X - _bounds.Min.X));
            var y = _bounds.Min.Y + (float)(_random.NextDouble() * (_bounds.Max.Y - _bounds.Min.Y));
            return new Vector2(x, y);
        }

        public void PickRandomPosition()
        {
            _moveGoal = GetRandomPosition();
        }

        public bool MoveToGoal(float elapsedTime)
        {
            if (_moveGoal.X == 0 && _moveGoal.Y == 0)
            {
                return false;
            }

            var distance = Vector2.Distance(Position, _moveGoal);
            if (distance < 0.05f)
            {
                _moveGoal = new Vector2(0, 0);

                OnChangeDirection?.Invoke(null, new OnChangeDirectionArgs(_moveGoal));
                return false;
            }

            var direction = Vector2.Normalize(_moveGoal - Position);
            var moveAmount = MovementSpeed * elapsedTime;

            if (moveAmount > distance)
            {
                moveAmount = distance;
            }

            MovementVector = direction * moveAmount;
            OnChangeDirection?.Invoke(null, new OnChangeDirectionArgs(MovementVector));

            return true;
        }

        public void Update(float elapsedTime)
        {
            MovementVector = Vector2.Zero;
            if (_state == MoveRandomComponentState.Idle)
            {
                _timeTicker += elapsedTime;
                if (_timeTicker > _timeToWait)
                {
                    _state = MoveRandomComponentState.Moving;
                    PickRandomPosition();
                }
            }
            else if (_state == MoveRandomComponentState.Moving)
            {
                if (!MoveToGoal(elapsedTime))
                {
                    _state = MoveRandomComponentState.Idle;
                    _timeToWait = 1 + (float)(_random.NextDouble() * 10 / MovementSpeed);
                    _timeTicker = 0;
                }

                OnPlaySound?.Invoke(null, new OnPlaySoundArgs(Position, Position));
            }
        }
    }
}
