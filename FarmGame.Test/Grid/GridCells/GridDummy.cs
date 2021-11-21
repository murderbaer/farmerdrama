using System.Xml;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using ImageMagick;
using FarmGame;

namespace FarmGame.Test
{
    public class GridDummy : IReadOnlyGrid
    {
        private static TiledHandler _tileHandler = TiledHandler.Instance;

        private readonly IGridCell[] _grid;

        private SpriteGrid[] _spriteGrid;

        private MagickImage _spriteSheet;


        public GridDummy()
        {
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.global.png");
            // _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
            XmlNode data = _tileHandler.LevelOneTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            _grid = new IGridCell[tiles.Count];
            Column = _tileHandler.BoardX;
            Row = _tileHandler.BoardY;

            _spriteGrid = new SpriteGrid[4];

            _spriteGrid[0] = new SpriteGrid(tiles.Count, Column, Row);
            _spriteGrid[1] = new SpriteGrid(tiles.Count, Column, Row);
            _spriteGrid[2] = new SpriteGrid(tiles.Count, Column, Row);
            _spriteGrid[3] = new SpriteGrid(tiles.Count, Column, Row);

            intializeLayerOne();
            intializeLayerTwo();
            intializeLayerThree();
            intializeLayerFour();
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

                _spriteGrid[0][i] = new SpriteObject(_spriteSheet, gid);
            }
        }

        private void intializeLayerTwo()
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

                _spriteGrid[1][i] = new SpriteObject(_spriteSheet, gid);
            }
        }

        private void intializeLayerThree()
        {
            XmlNode data = _tileHandler.LevelThreeTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                if (gid != (int)SpriteType.AIR)
                {
                    _grid[i] = new GridCellCollision();
                }
                _spriteGrid[2][i] = new SpriteObject(_spriteSheet, gid);
            }
        }

        private void intializeLayerFour()
        {
            XmlNode data = _tileHandler.LevelFourTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                _spriteGrid[3][i] = new SpriteObject(_spriteSheet, gid);
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
        }

        public void DrawLayer(int layer)
        {

        }
    }
}