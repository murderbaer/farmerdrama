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
    }
}