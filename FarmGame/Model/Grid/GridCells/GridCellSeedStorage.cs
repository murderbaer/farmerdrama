using FarmGame.Core;

namespace FarmGame.Model.GridCells
{
    public class GridCellSeedStorage : GridCell
    {
        public GridCellSeedStorage(float hiddenFactor)
        : base(hiddenFactor)
        {
        }

        public override ItemType TakeItem()
        {
            return ItemType.SEED;
        }
    }
}