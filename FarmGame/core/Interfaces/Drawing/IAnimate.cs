namespace FarmGame.Core
{
    public interface IAnimate : IUpdatable, IComponent
    {
        void Animate(object sender, OnChangeDirectionArgs e);
    }
}