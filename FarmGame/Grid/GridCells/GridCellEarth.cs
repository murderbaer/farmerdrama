using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCellEarth : GridCell
    {
        public GridCellEarth(int spriteId)
        : base(spriteId)
        {
            CellColor = Color4.Green;
        }

        public override void Update(float elapsedTime, IWorld world)
        {
        }
    }
}