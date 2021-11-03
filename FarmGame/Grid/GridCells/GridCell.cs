using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCell : IGridCell
    {
        public virtual bool HasCollision { get; set; }

        public virtual Color4 CellColor { get; set; }

        public virtual void Update(float elapsedTime, IWorld world)
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

        public virtual void DrawGridCell(int positionX, int positionY)
        {
            GL.Color4(CellColor);
            GL.Vertex2(positionX, positionY);
            GL.Vertex2(positionX, positionY + 1);
            GL.Vertex2(positionX + 1, positionY + 1);
            GL.Vertex2(positionX + 1, positionY);
        }
    }
}