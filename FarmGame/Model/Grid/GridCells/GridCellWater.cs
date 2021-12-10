using FarmGame.Core;

namespace FarmGame.Model.GridCells
{
    public class GridCellWater : GridCell
    {
        public float PoisenCounter { get; set; }

        public override Item TakeItem()
        {
            if (PoisenCounter > 0)
            {
                return new Item(ItemType.EMPTY);
            }
            else
            {
                return new Item(ItemType.WATERBUCKET);
            }
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
            PoisenCounter -= elapsedTime;
            if (PoisenCounter < 0)
            {
                PoisenCounter = 0;
            }
        }
    }
}