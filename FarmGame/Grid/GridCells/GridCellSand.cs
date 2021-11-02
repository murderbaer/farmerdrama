using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellSand : IGridCell
    {
        public bool HasCollision { get; } = false;

        public Color4 CellColor { get; set; } = Color4.Orange;

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
        }

        public Item TakeItem()
        {
            return new Item(ItemType.EMPTY);
        }

        public bool InteractWithItem(Item item)
        {
            return false;
        }
    }
}