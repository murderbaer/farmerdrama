using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellFence : GridCell
    {
        public GridCellFence()
        {
            HasCollision = true;
        }
    }
}