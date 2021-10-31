namespace FarmGame
{
    public class Item
    {
        private ItemType _type;
        public ItemType Type{get {return _type;} }

        public Item(ItemType type)
        {
            _type = type;
        }
        public Item()
        {
            _type = ItemType.EMPTY;
        }
    }
}