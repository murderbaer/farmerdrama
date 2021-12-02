namespace FarmGame
{
    public interface ICollidable : IPosition
    {
        float CollisionRadius { get; }
    }
}