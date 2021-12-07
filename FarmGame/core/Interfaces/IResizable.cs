namespace FarmGame.Core
{
    public interface IResizable : IComponent
    {
        void Resize(int width, int height);
    }
}