using FarmGame.Core;

namespace FarmGame.Model.Grid
{
    public class GridCellWater : GridCell
    {
        public GridCellWater(float hiddenFactor)
        : base(hiddenFactor)
        {
        }

        public float PoisenCounter { get; set; }

        public override ItemType TakeItem()
        {
            if (PoisenCounter > 0)
            {
                return ItemType.EMPTY;
            }
            else
            {
                return ItemType.WATERBUCKET;
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