namespace FarmGame.Model.Grid
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