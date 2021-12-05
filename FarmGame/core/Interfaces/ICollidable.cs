namespace FarmGame
{
    public interface ICollidable : IMoving
    {
        float CollisionRadius { get; }
    }
}