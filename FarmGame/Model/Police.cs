using System;
using System.Collections.Generic;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Police: IUpdatable, IPosition
    {
        private int currentPath = -1;

        private int currentPathPosition = -1;

        private Random random = new Random();

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

        public Vector2 Position { get; set; }

        public List<List<Vector2>> Paths { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 1.5f;

        public void Update(float elapsedTime)
        {
            Move(elapsedTime);
        }

        public void StartPath(int pathID)
        {
            currentPath = pathID;
            currentPathPosition = 0;
        }

        public void StartRandomPath()
        {
            currentPath = random.Next(0, Paths.Count);
            currentPathPosition = 0;
        }

        public void EndPath()
        {
            currentPath = -1;
            currentPathPosition = -1;
        }

        public bool IsMoving()
        {
            return currentPath != -1;
        }

        private void Move(float elapsedTime)
        {
            if (currentPath < 0 || currentPath >= Paths.Count)
            {
                EndPath();
                return;
            }

            var path = Paths[currentPath];
            if (currentPathPosition < 0 || currentPathPosition >= path.Count)
            {
                EndPath();
                return;
            }

            var goal = path[currentPathPosition];
            var offset = goal - Position;
            var movement = offset.Normalized() * elapsedTime * MovementSpeed;
            if (movement.Length < offset.Length)
            {
                Position += movement;
                return;
            }

            // Eckpunkt erreicht
            Position = goal;
            currentPathPosition += 1;
        }
    }
}