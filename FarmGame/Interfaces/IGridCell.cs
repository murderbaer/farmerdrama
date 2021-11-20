using OpenTK.Mathematics;

namespace FarmGame
{
    public interface IGridCell : IUpdatable
    {
        public bool HasCollision { get; }

        public bool InteractWithItem(Item item);

        public Item TakeItem();

        public void DrawGridCellTextured(int positionX, int positionY, Box2 spriteBox);
    }
}