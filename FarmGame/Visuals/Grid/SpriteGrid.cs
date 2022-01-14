using FarmGame.Core;

namespace FarmGame.Visuals
{
    public class SpriteGrid
    {
        private ISpriteObject[] _spriteGrid;

        public SpriteGrid(int size, int col, int row)
        {
            _spriteGrid = new SpriteObject[size];
            Column = col;
            Row = row;
        }

        public int Column { get; private set; }

        public int Row { get; private set; }

        public ISpriteObject this[int col, int row]
        {
            get { return _spriteGrid[col + (Column * row)]; }
            set { _spriteGrid[col + (Column * row)] = value; }
        }

        public ISpriteObject this[int id]
        {
            get { return _spriteGrid[id]; }
            set { _spriteGrid[id] = value; }
        }
    }
}