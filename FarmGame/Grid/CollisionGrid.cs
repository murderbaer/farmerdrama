namespace FarmGame
{
    public class CollisionGrid : IComponent
    {
        public CollisionGrid(SpriteGrid collisionGrid)
        {
            if (collisionGrid == null)
            {
                return;
            }
            int gridSize = collisionGrid.Column * collisionGrid.Row;
            Column = collisionGrid.Column;
            Row = collisionGrid.Row;
            CollidableGrid = new IGridCell[gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                switch ((SpriteType)collisionGrid[i].Gid)
                {
                    case SpriteType.AIR:
                        CollidableGrid[i] = new GridCell();
                        break;
                    default:
                        CollidableGrid[i] = new GridCellCollision();
                        break;
                }
            }
        }

        public IGridCell[] CollidableGrid { get; private set; }

        public int Column { get; private set; }

        public int Row { get; private set; }

        public IGridCell this[int col, int row]
        {
            get { return CollidableGrid[col + (Column * row)]; }
            set { CollidableGrid[col + (Column * row)] = value; }
        }
    }
}