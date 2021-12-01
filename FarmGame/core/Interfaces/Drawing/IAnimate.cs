namespace FarmGame
{
    public interface IAnimate : IUpdatable, IComponent
    {
        void Animate(object sender, OnChangeDirectionArgs e);
    }
}