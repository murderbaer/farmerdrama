namespace FarmGame
{
    public interface IDrawable : IComponent
    {
        SpriteObject Sprite { get;  }

        void Draw();
    }
}