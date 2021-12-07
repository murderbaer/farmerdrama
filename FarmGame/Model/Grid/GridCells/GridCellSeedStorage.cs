namespace FarmGame.Model.GridCells
{
    public class GridCellSeedStorage : GridCell
    {
        public override Item TakeItem()
        {
            return new Item(ItemType.SEED);
        }
    }
}