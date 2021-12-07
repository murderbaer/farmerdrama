namespace FarmGame.Core
{
    public interface IReadOnlyGrid : IUpdatable
    {
        int Column { get; }

        int Row { get; }

        IGridCell this[int column, int row] { get; }
    }
}