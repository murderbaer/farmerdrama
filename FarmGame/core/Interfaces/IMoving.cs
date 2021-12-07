using OpenTK.Mathematics;

namespace FarmGame.Core
{
    public interface IMoving : IPosition
    {
        public float MovementSpeed { get; set; }

        public Vector2 MovementVector { get; set; }
    }
}