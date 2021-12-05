using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IMoving : IPosition
    {
        public float MovementSpeed { get; set; }

        public Vector2 MovementVector { get; set; }
    }
}