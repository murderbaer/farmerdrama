using FarmGame.Core;

namespace FarmGame.Items
{
    public class Item : IComponent
    {
        public Item(ItemType type)
        {
            Type = type;
        }

        public Item()
        {
            Type = ItemType.EMPTY;
        }

        public ItemType Type { get; }
    }
}