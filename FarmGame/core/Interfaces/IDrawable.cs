namespace FarmGame
{
    public interface IDrawBackground : IComponent
    {
        void DrawBackground();
    }

    public interface IDrawGround : IComponent
    {
        void DrawGround();
    }

    public interface IDrawable : IComponent
    {
        void Draw();
    }

    public interface IDrawAbove : IComponent
    {
        void DrawAbove();
    }

    public interface IDrawOverlay : IComponent
    {
        void DrawOverlay();
    }
}