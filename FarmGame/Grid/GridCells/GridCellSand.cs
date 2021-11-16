using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellSand : GridCell
    {
        public GridCellSand(int spriteId)
        : base(spriteId)
        {
            CellColor = Color4.Orange;
        }
    }
}