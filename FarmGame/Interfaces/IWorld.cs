namespace FarmGame
{
    public interface IWorld : IUpdatable, IDrawable
    {
        public IReadOnlyGrid Grid { get; set; }

        public ICamera Camera { get; }

        public IPlayer Player { get; }
    }
}