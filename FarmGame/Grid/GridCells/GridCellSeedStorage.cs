using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellSeedStorage : GridCell
    {
        public override Item TakeItem()
        {
            return new Item(ItemType.SEED);
        }
    }
}