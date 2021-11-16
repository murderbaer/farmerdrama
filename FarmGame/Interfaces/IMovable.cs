using System.Collections.Generic;
using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IMovable : IDrawable, IUpdatable
    {
        public Vector2 Position { get; set; }

        public List<List<Vector2>> Paths { get; set; }

        public float MovementSpeed { get; set; }

        public void StartPath(int pathID);

        public void StartRandomPath();

        public void EndPath();

        public bool IsMoving();
    }
}