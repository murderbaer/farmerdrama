using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IGridCell : IUpdatable
    {
        public bool HasCollision { get; }

        public Color4 CellColor { get; }

        public bool InteractWithItem(Item item);

        public Item TakeItem();

        public void DrawGridCell(int positionX, int positionY);
    }
}