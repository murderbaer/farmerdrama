using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCell : IGridCell
    {
        public GridCell()
        {
        }

        public virtual bool HasCollision { get; set; }

        public virtual SpriteObject Sprite { get; private set; }

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
        public void DrawGridCellTextured(int positionX, int positionY, Box2 sprite)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(sprite.Min);
            GL.Vertex2(positionX, positionY);
            GL.TexCoord2(sprite.Max.X, sprite.Min.Y);
            GL.Vertex2(positionX + 1, positionY);
            GL.TexCoord2(sprite.Max);
            GL.Vertex2(positionX + 1, positionY + 1);
            GL.TexCoord2(sprite.Min.X, sprite.Max.Y);
            GL.Vertex2(positionX, positionY + 1);
            GL.End();
        }
    }
}