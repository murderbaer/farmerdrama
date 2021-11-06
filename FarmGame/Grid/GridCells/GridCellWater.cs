using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellWater : GridCell
    {
        public GridCellWater()
        {
            CellColor = Color4.DodgerBlue;
        }

        public override Item TakeItem()
        {
            return new Item(ItemType.WATERBUCKET);
        }
    }
}