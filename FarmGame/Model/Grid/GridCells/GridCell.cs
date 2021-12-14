using FarmGame.Core;
using FarmGame.Items;

namespace FarmGame.Model.GridCells
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

        public virtual Item TakeItem()
        {
            return new Item(ItemType.EMPTY);
        }

        public virtual bool InteractWithItem(Item item)
        {
            return false;
        }
    }
}