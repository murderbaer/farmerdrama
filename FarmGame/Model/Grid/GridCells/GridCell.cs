using FarmGame.Core;

namespace FarmGame.Model.Grid
{
    public class GridCell : IGridCell
    {
        public GridCell(float hiddenFactor)
        {
            HiddenFactor = hiddenFactor;
        }

        public virtual bool HasCollision { get; set; }

        public virtual float HiddenFactor { get; } = 1f;

        public virtual void Update(float elapsedTime)
        {
        }

        public virtual ItemType TakeItem()
        {
            return ItemType.EMPTY;
        }

        public virtual bool InteractWithItem(ItemType item)
        {
            return false;
        }
    }
}