using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCell : IGridCell
    {
        public GridCell(int spriteId)
        {
            SpriteId = spriteId;
        }

        public virtual bool HasCollision { get; set; }

        public virtual Color4 CellColor { get; set; }

        public virtual int SpriteId { get; private set; }

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
        public void DrawGridCellTextured(int positionX, int positionY, Vector2 texCord)
        {
            float xSize = 16f / 1456f;
            float ySize = 16f / 1344f;

            GL.TexCoord2(texCord);
            GL.Vertex2(positionX, positionY);

            GL.TexCoord2(texCord.X + xSize, texCord.Y);
            GL.Vertex2(positionX + 1, positionY);

            GL.TexCoord2(texCord.X + xSize, texCord.Y + ySize);
            GL.Vertex2(positionX + 1, positionY + 1);

            GL.TexCoord2(texCord.X, texCord.Y + ySize);
            GL.Vertex2(positionX, positionY + 1);
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