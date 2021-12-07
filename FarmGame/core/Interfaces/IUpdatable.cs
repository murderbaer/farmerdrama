namespace FarmGame.Core
{
    public interface IUpdatable : IComponent
    {
        void Update(float elapsedTime);
    }
}