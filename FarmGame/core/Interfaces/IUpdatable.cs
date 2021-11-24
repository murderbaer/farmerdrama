namespace FarmGame
{
    public interface IUpdatable : IComponent
    {
        void Update(float elapsedTime);
    }
}