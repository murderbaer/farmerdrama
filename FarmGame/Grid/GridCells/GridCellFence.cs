using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellFence : GridCell
    {
        public GridCellFence()
        {
            CellColor = Color4.SlateGray;
            HasCollision = true;
        }
    }
}