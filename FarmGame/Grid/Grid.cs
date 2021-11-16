using System.Xml;

using OpenTK.Graphics.OpenGL;

namespace FarmGame
{
    public class Grid : IReadOnlyGrid
    {
        private static TiledHandler _tileHandler = TiledHandler.Instance;

        private readonly IGridCell[] _grid;

        public Grid()
        {
            XmlNode data = _tileHandler.LevelOneTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");
            _grid = new IGridCell[tiles.Count];
            Column = _tileHandler.BoardX;
            Row = _tileHandler.BoardY;
            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                switch ((SpriteType)gid)
                {
                    case SpriteType.FARM_LAND:
                    _grid[i] = new GridCellFarmLand(FarmLandState.EMPTY, gid); break;
                    case SpriteType.STONE_LEFT:
                    case SpriteType.STONE:
                    case SpriteType.STONE_RIGHT:
                    _grid[i] = new GridCellStone(gid); break;
                    case SpriteType.WATER_LU:
                    case SpriteType.WATER_RU:
                    case SpriteType.WATER_LD:
                    case SpriteType.WATER_RD:
                    _grid[i] = new GridCellWater(gid); break;
                    case SpriteType.SEEDS:
                    _grid[i] = new GridCellSeedStorage(gid); break;
                    default:
                    _grid[i] = new GridCellEarth(gid); break;
                }
            }

            data = _tileHandler.LevelTwoTiles.SelectSingleNode("data");
            tiles = data.SelectNodes("tile");
            for (int i = 0; i < tiles.Count; i++)
            {  
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                switch ((SpriteType)gid)
                {
                    case SpriteType.AIR:
                    break;
                    case SpriteType.STONE_LEFT:
                    case SpriteType.STONE:
                    case SpriteType.STONE_RIGHT:
                    _grid[i] = new GridCellStone(gid); break;
                    case SpriteType.WATER_LU:
                    case SpriteType.WATER_RU:
                    case SpriteType.WATER_LD:
                    case SpriteType.WATER_RD:
                    _grid[i] = new GridCellWater(gid); break;
                    case SpriteType.SEEDS:
                    _grid[i] = new GridCellSeedStorage(gid); break;
                    default:
                    _grid[i] = new GridCellEarth(gid); break;
                }
            }
        }

        public int Column { get; }

        public int Row { get; }

        public IGridCell this[int col, int row]
        {
            get { return _grid[col + (Column * row)]; }
            set { _grid[col + (Column * row)] = value; }
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
                    cell.DrawGridCell(column, row);
                }
            }

            GL.End();
        }
    }
}