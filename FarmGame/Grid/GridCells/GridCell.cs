namespace FarmGame
{
    public class GridCell : IGridCell
    {
        public virtual bool HasCollision { get; set; }

        public virtual SpriteObject Sprite { get; private set; }

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