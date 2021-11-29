namespace FarmGame
{
    public class GridCellWater : GridCell
    {
        public override Item TakeItem()
        {
            return new Item(ItemType.WATERBUCKET);
        }
    }
}