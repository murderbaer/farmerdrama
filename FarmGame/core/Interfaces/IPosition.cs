using OpenTK.Mathematics;

namespace FarmGame.Core
{
    public interface IPosition : IComponent
    {
        public Vector2 Position { get; set; }
    }
}