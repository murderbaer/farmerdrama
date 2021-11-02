using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Item : IDrawable
    {
        public Item(ItemType type)
        {
            Type = type;
        }

        public Item()
        {
            Type = ItemType.EMPTY;
        }
        
        public bool IsPlaced { get; set; }

        public Vector2 PlacedOnPosition { get; set; }

        public ItemType Type { get; }

        public void Draw()
        {
            if (IsPlaced)
            {
                GL.Vertex2(PlacedOnPosition.X - 0.2, PlacedOnPosition.Y - 0.2);
                GL.Vertex2(PlacedOnPosition.X - 0.2, PlacedOnPosition.Y + 0.2);
                GL.Vertex2(PlacedOnPosition.X + 0.2, PlacedOnPosition.Y + 0.2);
                GL.Vertex2(PlacedOnPosition.X + 0.2, PlacedOnPosition.Y - 0.2);
            }
        }

        public void Place(Vector2 position)
        {
            PlacedOnPosition = position;
            IsPlaced = true;
        }

        public void PickUp()
        {
            IsPlaced = false;
        }
    }
}