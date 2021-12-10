namespace FarmGame.Model.GridCells
{
    public class GridCellSeedStorage : GridCell
    {
        public GridCellSeedStorage(float hiddenFactor)
        : base(hiddenFactor)
        {
        }

        public override Item TakeItem()
        {
            return new Item(ItemType.SEED);
        }
    }
}