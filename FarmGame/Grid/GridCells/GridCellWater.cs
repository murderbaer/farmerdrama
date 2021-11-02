using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellWater : IGridCell
    {
        public bool HasCollision { get; } = true;

        public Color4 CellColor { get; set; } = Color4.DodgerBlue;

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
        }

        public void Draw()
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