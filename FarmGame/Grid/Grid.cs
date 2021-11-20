using System.Xml;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class Grid : IReadOnlyGrid
    {
        private static TiledHandler _tileHandler = TiledHandler.Instance;

        private readonly IGridCell[] _grid;
        private SpriteGrid[] _spriteGrid;

        public Grid()
        {
            XmlNode data = _tileHandler.LevelOneTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            _grid = new IGridCell[tiles.Count];
            Column = _tileHandler.BoardX;
            Row = _tileHandler.BoardY;

            _spriteGrid = new SpriteGrid[4];

            _spriteGrid[0] = new SpriteGrid(tiles.Count, Column, Row);
            _spriteGrid[1] = new SpriteGrid(tiles.Count, Column, Row);

            intializeLayerOne();
            intializeLayerTwo();
        }

        private void intializeLayerOne()
        {
            XmlNode data = _tileHandler.LevelOneTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                switch ((SpriteType)gid)
                {
                    case SpriteType.FARM_LAND:
                        _grid[i] = new GridCellFarmLand(FarmLandState.EMPTY); break;
                    case SpriteType.WATER_LU:
                    case SpriteType.WATER_RU:
                    case SpriteType.WATER_LD:
                    case SpriteType.WATER_RD:
                        _grid[i] = new GridCellWater(); break;
                    case SpriteType.AIR:
                        break;
                    case SpriteType.SEEDS:
                        _grid[i] = new GridCellSeedStorage(); break;
                    default:
                        _grid[i] = new GridCell(); break;
                }

                _spriteGrid[0][i] = new SpriteObject(World.GlobalSprite, gid);
            }
        }

        private void intializeLayerTwo()
        {
            XmlNode data = _tileHandler.LevelTwoTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");


            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                _spriteGrid[1][i] = new SpriteObject(World.GlobalSprite, gid);
            }
        }

        private void intializeLayerThree()
        {
            XmlNode data = _tileHandler.LevelTwoTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");


            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                if (gid == (int)SpriteType.SEEDS)
                {
                    _grid[i] = new GridCellSeedStorage();
                }
                _spriteGrid[1][i] = new SpriteObject(World.GlobalSprite, gid);
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
            DrawLayer(0);
            DrawLayer(1);
        }

        private void DrawLayer(int layer)
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            for (int row = 0; row < Row; ++row)
            {
                for (int column = 0; column < Column; ++column)
                {
                    IGridCell cell = this[column, row];
                    SpriteObject toDraw = _spriteGrid[layer][column, row];
                    if (toDraw.Gid != (int)SpriteType.AIR)
                    {
                        cell.DrawGridCellTextured(column, row,
                            SpriteHelper.GetTexCoordFromSprite(toDraw));
                    }
                }
            }
        }
    }
}