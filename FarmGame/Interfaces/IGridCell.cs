using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IGridCell : IUpdatable
    {
        public bool HasCollision { get; }

        public Color4 CellColor { get; }

        public bool InteractWithItem(Item item);

        public int SpriteId { get; }

        public Item TakeItem();

        public void DrawGridCellTextured(int positionX, int positionY, Box2 sprite);
        public void DrawGridCell(int positionX, int positionY);
    }
}