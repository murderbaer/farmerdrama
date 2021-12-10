namespace FarmGame.Model.GridCells
{
    public class GridCellCollision : GridCell
    {
        public GridCellCollision(float hiddenFactor)
        : base(hiddenFactor)
        {
            HasCollision = true;
        }
    }
}