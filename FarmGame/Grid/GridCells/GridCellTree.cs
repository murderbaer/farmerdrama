using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCellTree : GridCell
    {
        public GridCellTree(int spriteId)
        : base(spriteId)
        {
            CellColor = Color4.Cyan;
            HasCollision = true;
        }


    }
}