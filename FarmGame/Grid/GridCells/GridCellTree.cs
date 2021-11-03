using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCellTree : GridCell
    {
        public GridCellTree()
        {
            CellColor = Color4.Cyan;
            HasCollision = true;
        }

        public override void DrawGridCell(int positionX, int positionY)
        {
            GL.Color4(CellColor);
            GL.Vertex2(positionX, positionY - 1);
            GL.Vertex2(positionX + 1, positionY - 1);
            GL.Vertex2(positionX + 1, positionY + 1.5);
            GL.Vertex2(positionX, positionY + 1.5);
        }
    }
}