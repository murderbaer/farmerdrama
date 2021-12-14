using FarmGame.Helpers;
using FarmGame.Model;

namespace FarmGame.Visuals
{
    public class SpriteGrid
    {
        private SpriteObject[] _spriteGrid;

        public SpriteGrid(int size, int col, int row)
        {
            _spriteGrid = new SpriteObject[size];
            Column = col;
            Row = row;
        }

        public SpriteGrid()
        {
            var tiledHandler = TiledHandler.Instance;
            _spriteGrid = new SpriteObject[tiledHandler.BoardX * tiledHandler.BoardY];
            Column = tiledHandler.BoardX;
            Row = tiledHandler.BoardY;
        }

        public int Column { get; private set; }

        public int Row { get; private set; }

        public SpriteObject this[int col, int row]
        {
            get { return _spriteGrid[col + (Column * row)]; }
            set { _spriteGrid[col + (Column * row)] = value; }
        }

        public SpriteObject this[int id]
        {
            get { return _spriteGrid[id]; }
            set { _spriteGrid[id] = value; }
        }

        public static SpriteGrid SquashGrids(SpriteGrid board1, SpriteGrid board2)
        {
            int gridSize = board1.Column * board1.Row;
            SpriteGrid ret = new SpriteGrid(gridSize, board1.Column, board1.Row);

            for (int i = 0; i < gridSize; i++)
            {
                if (board2[i].Gid == (int)SpriteType.AIR)
                {
                    ret[i] = board1[i];
                }
                else
                {
                    ret[i] = board2[i];
                }
            }

            return ret;
        }
    }
}