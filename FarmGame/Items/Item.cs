using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Item
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