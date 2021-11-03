using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCellEarth : GridCell
    {
        public GridCellEarth()
        {
            CellColor = Color4.Green;
        }

        public override void Update(float elapsedTime, IWorld world)
        {
        }
    }
}