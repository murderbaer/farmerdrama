namespace FarmGame.Core
{
    public interface IDraw : IComponent
    {
        SpriteObject Sprite { get;  }

        void Draw();
    }
}