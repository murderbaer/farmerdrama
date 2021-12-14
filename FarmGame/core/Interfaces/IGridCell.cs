namespace FarmGame.Core
{
    public interface IGridCell : IUpdatable
    {
        public bool HasCollision { get; }

        public float HiddenFactor { get; }

        public bool InteractWithItem(ItemType item);

        public ItemType TakeItem();
    }
}