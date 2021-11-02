using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IPlayer : IDrawable, IUpdatable
    {
        public Vector2 Position { get; set; }
        public void Interact(IGridCell cell);
    }
}