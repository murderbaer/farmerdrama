using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Movable : IMovable
    {
        private int currentPath = -1;

        private int currentPathPosition = -1;

        private Random random = new Random();

        public Movable()
        {
            // Set starting position
            Position = new Vector2(10, 10);
            Paths = new List<List<Vector2>>
            {
                new List<Vector2>
                {
                    new Vector2(0, 0),
                    new Vector2(10, 10),
                    new Vector2(15, 5),
                    new Vector2(5, 2),
                },
                new List<Vector2>
                {
                    new Vector2(6, 0),
                    new Vector2(12, 1),
                    new Vector2(4, 20),
                    new Vector2(1, 23),
                },
            };
        }

        public Vector2 Position { get; set; }

        public List<List<Vector2>> Paths { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 1.5f;

        public void Draw()
        {
            GL.Color4(Color4.DarkBlue);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(Position.X - 0.4, Position.Y + 0.4);
            GL.Vertex2(Position.X + 0.4, Position.Y + 0.4);
            GL.Vertex2(Position.X, Position.Y - .4);
            GL.End();
        }

        public void Update(float elapsedTime, IWorld world)
        {
            Move(elapsedTime, world);
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

        private void Move(float elapsedTime, IWorld world)
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