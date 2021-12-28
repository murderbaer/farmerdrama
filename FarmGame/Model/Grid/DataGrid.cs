using System;
using System.Collections.Generic;
using System.Linq;

using FarmGame.Core;
using FarmGame.Helpers;

using OpenTK.Mathematics;

namespace FarmGame.Model.Grid
{
    public class DataGrid : IComponent, IReadOnlyGrid
    {
        private IGridCell[] _grid;

        public DataGrid(EventHandler<OnStateChangeArgs> stateChange)
        {
            Column = TiledHandler.Instance.BoardX;
            Row = TiledHandler.Instance.BoardY;
            int gridSize = Column * Row;

            _grid = new IGridCell[gridSize];
            float[] hiddenFactorHeatmap = TiledHandler.Instance.HiddenFactorGrid;
            var layers = TiledHandler.Instance.SquashedLayers;
            for (int i = 0; i < gridSize; i++)
            {
                switch (layers[i])
                {
                    case SpriteType.FARMLAND:
                        _grid[i] = new GridCellFarmLand(FarmLandState.EMPTY, i, hiddenFactorHeatmap[i]);
                        GridCellFarmLand temp = (GridCellFarmLand)_grid[i];
                        temp.OnStateChange += stateChange;
                        break;
                    case SpriteType.COLLISION:
                        _grid[i] = new GridCellCollision(hiddenFactorHeatmap[i]);
                        break;
                    case SpriteType.SEEDS:
                        _grid[i] = new GridCellSeedStorage(hiddenFactorHeatmap[i]);
                        break;
                    case SpriteType.FEEDER:
                        _grid[i] = new GridCellFeeder(hiddenFactorHeatmap[i]);
                        break;
                    default:
                        if (SpriteType.WATER.Contains(layers[i]))
                        {
                            _grid[i] = new GridCellWater(hiddenFactorHeatmap[i]);
                        }
                        else
                        {
                            _grid[i] = new GridCell(hiddenFactorHeatmap[i]);
                        }
                        break;
                }
            }
        }

        // for testing
        public DataGrid()
        {
            int gridSize = 100;
            _grid = new IGridCell[gridSize];

            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new GridCell(1f);
            }
        }

        public int Column { get; }

        public int Row { get; }

        public IGridCell this[int col, int row]
        {
            get { return _grid[col + (Column * row)]; }
            set { _grid[col + (Column * row)] = value; }
        }

        public IGridCell this[int id]
        {
            get { return _grid[id]; }
            set { _grid[id] = value; }
        }

        public void Update(float elapsedTime)
        {
            foreach (IGridCell cell in _grid)
            {
                cell.Update(elapsedTime);
            }
        }

        public IEnumerable<IGridCell> GetCellsNearby(Vector2 position, float radius)
        {
            var invRadius = 1 / radius;
            var limX = MathHelper.Ceiling(position.X + radius);
            var limY = MathHelper.Ceiling(position.Y + radius);
            for (int x = (int)MathHelper.Floor(position.X - radius); x < limX; x++)
            {
                for (int y = (int)MathHelper.Floor(position.Y - radius); y < limY; y++)
                {
                    var dist = MathHelper.InverseSqrtFast(MathHelper.Pow(x - position.X, 2) + MathHelper.Pow(y - position.Y, 2));
                    if (x >= 0 && x < Column && y >= 0 && y < Row && dist > invRadius)
                    {
                        yield return this[x, y];
                    }
                }
            }
        }
    }
}