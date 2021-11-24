using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
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