using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IPosition : IComponent
    {
        public Vector2 Position { get; set; }
    }
}