namespace FarmGame.Core
{
    public interface ICollidable : IMoving
    {
        float CollisionRadius { get; }
    }
}