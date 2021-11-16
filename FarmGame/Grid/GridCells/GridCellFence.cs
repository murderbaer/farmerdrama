using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellFence : GridCell
    {
        public GridCellFence(int spriteId)
        : base(spriteId)
        {
            CellColor = Color4.SlateGray;
            HasCollision = true;
        }
    }
}