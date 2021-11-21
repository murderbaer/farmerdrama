namespace FarmGame
{
    public interface IReadOnlyGrid : IUpdatable, IDrawable
    {
        int Column { get; }

        int Row { get; }

        IGridCell this[int column, int row] { get; }

        public void DrawLayer(int layer);
    }
}