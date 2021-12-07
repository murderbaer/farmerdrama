using System;
using System.Collections.Generic;

using FarmGame.Core;
using FarmGame.Helpers;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class Police : IUpdatable, IPosition, IChangeDirection, IMoving
    {
        private int _currentPath = -1;

        private int _currentPathPosition = -1;

        private Random _random = new Random();

        private TiledHandler _tiledHandler = TiledHandler.Instance;

        public Police()
        {
            // sets position to be a bit outside of the fence
            Position = _tiledHandler.TiledPolicePos;

            Paths = _tiledHandler.TiledPolicePaths;
        }

        public event EventHandler<OnChangeDirectionArgs> OnChangeDirection;

        public Vector2 Position { get; set; }

        public Vector2 MovementVector { get; set; }

        public float CollisionRadius { get; set; } = 0.5f;

        public List<List<Vector2>> Paths { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 1.5f;

        public void Update(float elapsedTime)
        {
            Move(elapsedTime);
        }

        public void StartPath(int pathID)
        {
            _currentPath = pathID;
            _currentPathPosition = 0;
        }

        public void StartRandomPath()
        {
            _currentPath = _random.Next(0, Paths.Count);
            _currentPathPosition = 0;
        }

        public void EndPath()
        {
            _currentPath = -1;
            _currentPathPosition = -1;
        }

        public bool IsMoving()
        {
            return _currentPath != -1;
        }

        private void Move(float elapsedTime)
        {
            if (_currentPath < 0 || _currentPath >= Paths.Count)
            {
                EndPath();
                return;
            }

            var path = Paths[_currentPath];
            if (_currentPathPosition < 0 || _currentPathPosition >= path.Count)
            {
                EndPath();
                return;
            }

            var goal = path[_currentPathPosition];
            var offset = goal - Position;
            var movement = offset.Normalized() * elapsedTime * MovementSpeed;
            OnChangeDirection?.Invoke(null, new OnChangeDirectionArgs(movement));
            if (movement.Length < offset.Length)
            {
                MovementVector = movement;
                return;
            }

            // Eckpunkt erreicht
            _currentPathPosition += 1;
        }
    }
}