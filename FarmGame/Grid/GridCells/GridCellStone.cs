namespace FarmGame
{
    // TODO: remove after sprites are used, temporary to test tile types
    public class GridCellStone : GridCell
    {
        public GridCellStone(int spriteId)
        : base(spriteId)
        {
            CellColor = OpenTK.Mathematics.Color4.DarkGray;
        }
    }
}