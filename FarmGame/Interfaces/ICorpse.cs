using OpenTK.Mathematics;

namespace FarmGame
{
    public interface ICorpse : IUpdatable, IDrawable
    {
        public bool IsPlaced { get; set; }

        public Vector2 Position { get; set; }
    }
}
