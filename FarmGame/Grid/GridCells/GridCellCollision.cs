using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellCollision : GridCell
    {
        public GridCellCollision()
        {
            HasCollision = true;
        }
    }
}