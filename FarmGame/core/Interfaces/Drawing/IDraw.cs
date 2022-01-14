using FarmGame.Visuals;

namespace FarmGame.Core
{
    public interface IDraw : IComponent
    {
        ISpriteObject Sprite { get; }

        void Draw();
    }
}