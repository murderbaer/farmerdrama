using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellSeedStorage : IGridCell
    {
        public bool HasCollision { get; } = true;

        public Color4 CellColor { get; set; } = Color4.BlueViolet;

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
        }

        public void Draw()
        {
        }

        public Item TakeItem()
        {
            return new Item(ItemType.SEED);
        }

        public bool InteractWithItem(Item item)
        {
            return false;
        }
    }
}