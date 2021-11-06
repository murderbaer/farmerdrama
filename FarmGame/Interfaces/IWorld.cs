using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public interface IWorld : IDrawable
    {
        public GameWindow Window { get; set; }

        public IReadOnlyGrid Grid { get; set; }

        public ICamera Camera { get; }

        public IPlayer Player { get; }
    }
}