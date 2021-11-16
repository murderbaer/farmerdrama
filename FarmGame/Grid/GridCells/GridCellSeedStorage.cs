using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellSeedStorage : GridCell
    {
        public GridCellSeedStorage(int spriteId)
        : base(spriteId)
        {
            CellColor = Color4.BlueViolet;
        }

        public override Item TakeItem()
        {
            return new Item(ItemType.SEED);
        }
    }
}