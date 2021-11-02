using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellEarth : IGridCell
    {
        public bool HasCollision { get; } = false;

        public Color4 CellColor { get; set; } = Color4.Green;

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
        }

        public Item TakeItem()
        {
            return new Item(ItemType.WATERBUCKET);
        }

        public bool InteractWithItem(Item item)
        {
            return false;
        }
    }
}