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
            Position = new Vector2(935f / 16f, 360f / 16f);

            // read all paths in
            Paths = new List<List<Vector2>>();
            var policePathsTiled = _tiledHandler.TiledPolicePaths.SelectNodes("object");
            float offset_x = 0f;
            float offset_y = 0f;
            for (int i = 0; i < policePathsTiled.Count; i++)
            {
                offset_x = float.Parse(policePathsTiled[i].Attributes["x"].Value);
                offset_y = float.Parse(policePathsTiled[i].Attributes["y"].Value);
                string polPath = policePathsTiled[i].SelectNodes("polygon")[0].Attributes["points"].Value;
                List<Vector2> singelPath = new List<Vector2>();
                string[] cords = polPath.Split(' ');

                for (int j = 0; j < cords.Length; j++)
                {
                    string[] singleCoord = cords[j].Split(',');
                    float x = (float.Parse(singleCoord[0]) + offset_x) / 16f;
                    float y = (float.Parse(singleCoord[1]) + offset_y) / 16f;
                    singelPath.Add(new Vector2(x, y));
                }

                singelPath.Add(Position);
                Paths.Add(singelPath);
            }
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