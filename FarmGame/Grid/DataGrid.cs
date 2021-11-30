using System;

namespace FarmGame
{
    public class DataGrid : IComponent, IReadOnlyGrid
    {
        private IGridCell[] _grid;

        public DataGrid(SpriteGrid functionalSprites)
        {
            int gridSize = functionalSprites.Column * functionalSprites.Row;
            _grid = new IGridCell[gridSize];
            Column = functionalSprites.Column;
            Row = functionalSprites.Row;

            for (int i = 0; i < gridSize; i++)
            {
                switch ((SpriteType)functionalSprites[i].Gid)
                {
                    case SpriteType.FARM_LAND:
                    _grid[i] = new GridCellFarmLand(FarmLandState.EMPTY);
                    GridCellFarmLand temp = (GridCellFarmLand)_grid[i];
                    temp.OnStateChange += test;
                    break;
                    case SpriteType.WATER_LD:
                    case SpriteType.WATER_LU:
                    case SpriteType.WATER_RD:
                    case SpriteType.WATER_RU:
                    _grid[i] = new GridCellWater();
                    break;
                    case SpriteType.COLLISION:
                    _grid[i] = new GridCellCollision();
                    break;
                    case SpriteType.SEEDS:
                    _grid[i] = new GridCellSeedStorage();
                    break;
                    default:
                    _grid[i] = new GridCell();
                    break;
                }
            }
        }

        private void test(object sender, EventArgs e)
        {
            Console.WriteLine("Testing");
        }

        #if DEBUG
        public DataGrid()
        {
            int gridSize = 100;
            _grid = new IGridCell[gridSize];

            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new GridCell();
            }
        }
        #endif

        public int Column { get; }

        public int Row { get; }

        public IGridCell this[int col, int row]
        {
            get { return _grid[col + (Column * row)]; }
            set { _grid[col + (Column * row)] = value; }
        }

        public void Update(float elapsedTime)
        {
            foreach (IGridCell cell in _grid)
            {
                cell.Update(elapsedTime);
            }
        }
    }
}