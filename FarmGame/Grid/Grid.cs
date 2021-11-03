using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class Grid : IReadOnlyGrid
    {
        private readonly IGridCell[] _grid;

        public Grid(int col, int row)
        {
            Column = col;
            Row = row;
            _grid = new IGridCell[col * row];
            for (int i = 0; i < col * row; i++)
            {
                _grid[i] = new GridCellEarth();
            }

            _grid[(Column * Row / 2) + (row / 2)] = new GridCellSand();
            _grid[(Column * Row / 2) + (row / 2) + 1] = new GridCellWater();
            _grid[(Column * Row / 2) + (row / 2) + 2] = new GridCellFarmLand(FarmLandState.EMPTY);
            _grid[(Column * Row / 2) + (row / 2) + 3] = new GridCellFarmLand(FarmLandState.SEED);
            _grid[(Column * Row / 2) + (row / 2) + 4] = new GridCellSeedStorage();
        }

        public int Column { get; }

        public int Row { get; }

        public IGridCell this[int col, int row]
        {
            get { return _grid[col + (Column * row)]; }
            set { _grid[col + (Row * row)] = value; }
        }

        public void Update(float elapsedTime, IWorld world)
        {
            foreach (IGridCell cell in _grid)
            {
                cell.Update(elapsedTime, world);
            }
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            for (int row = 0; row < Row; ++row)
            {
                for (int column = 0; column < Column; ++column)
                {
                    IGridCell cell = this[column, row];
                    GL.Color4(cell.CellColor);
                    GL.Vertex2(column, row);
                    GL.Vertex2(column + 1, row);
                    GL.Vertex2(column + 1, row + 1);
                    GL.Vertex2(column, row + 1);
                }
            }

            GL.End();
        }
    }
}