using System.Collections.Generic;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Grid : IReadOnlyGrid
    {

        public int Column {get;}
        public int Row {get;}
        private readonly GridCell[] _grid;

        public Grid(int col, int row)
        {
            Column = col;
            Row = row;
            _grid = new GridCell[col * row];
            for (int i = 0; i < col * row; i++)
            {
                _grid[i] = new GridCell(false, GridType.EARTH);
            }
            _grid[(Column * Row) / 2 + row / 2] = new GridCell(false, GridType.SAND);
            _grid[(Column * Row) / 2 + row / 2 + 1] = new GridCell(false, GridType.WATER);
        }

        public GridCell this[int col, int row]
        {
            get { return _grid[col + Column * row];}
            set { _grid[col + Row * row] = value; }
        }

        // public List<GridCell> GetNeighbours(int posX, int posY)
        // {
        //     if ()
        // }
    }
}