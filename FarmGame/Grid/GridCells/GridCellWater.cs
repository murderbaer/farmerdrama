using OpenTK.Mathematics;
using System;

namespace FarmGame
{
    public class GridCellWater : GridCell
    {
        public GridCellWater(int spriteId)
        : base(spriteId)
        {
            CellColor = Color4.DodgerBlue;
        }

        public override Item TakeItem()
        {
            return new Item(ItemType.WATERBUCKET);
        }
    }
}