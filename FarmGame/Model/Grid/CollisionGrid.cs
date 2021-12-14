using FarmGame.Core;
using FarmGame.Helpers;

namespace FarmGame.Model.Grid
{
    public class CollisionGrid : IComponent, IReadOnlyGrid
    {
        public CollisionGrid(int[] collisionGrid)
        {
            int gridSize = TiledHandler.Instance.BoardX * TiledHandler.Instance.BoardY;
            Column = TiledHandler.Instance.BoardX;
            Row = TiledHandler.Instance.BoardY;
            CollidableGrid = new IGridCell[gridSize];

            float[] hiddenFactorHeatmap = TiledHandler.Instance.HiddenFactorGrid;
            for (int i = 0; i < gridSize; i++)
            {
                switch ((SpriteType)collisionGrid[i])
                {
                    case SpriteType.AIR:
                        CollidableGrid[i] = new GridCell(hiddenFactorHeatmap[i]);
                        break;
                    default:
                        CollidableGrid[i] = new GridCellCollision(hiddenFactorHeatmap[i]);
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

        public void Update(float elapsedTime)
        {
        }
    }
}