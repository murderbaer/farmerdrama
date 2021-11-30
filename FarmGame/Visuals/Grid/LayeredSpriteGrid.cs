using System;
using System.Xml;

using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class LayeredSpriteGrid : IDrawGridLayer
    {
        private static TiledHandler _tileHandler = TiledHandler.Instance;

        private readonly IGridCell[] _grid;

        private SpriteGrid[] _spriteGrid;

        private MagickImage _spriteSheet;

        private int _spriteHandle;

        public LayeredSpriteGrid()
        {
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.global.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
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

            IntializeLayerOne();
            IntializeLayerTwo();
            IntializeLayerThree();
            IntializeLayerFour();
        }

        public int Column { get; }

        public int Row { get; }

        public SpriteGrid GetWholeLayer(int i)
        {
            return _spriteGrid[i];
        }

        public void DrawLayer(int layer)
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            for (int row = 0; row < Row; ++row)
            {
                for (int column = 0; column < Column; ++column)
                {
                    SpriteObject toDraw = _spriteGrid[layer][column, row];
                    if (toDraw.Gid != (int)SpriteType.AIR)
                    {
                        DrawSingleSprite(column, row, SpriteHelper.GetTexCoordFromSprite(toDraw));
                    }
                }
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void DrawSingleSprite(int positionX, int positionY, Box2 sprite)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(sprite.Min);
            GL.Vertex2(positionX, positionY);
            GL.TexCoord2(sprite.Max.X, sprite.Min.Y);
            GL.Vertex2(positionX + 1, positionY);
            GL.TexCoord2(sprite.Max);
            GL.Vertex2(positionX + 1, positionY + 1);
            GL.TexCoord2(sprite.Min.X, sprite.Max.Y);
            GL.Vertex2(positionX, positionY + 1);
            GL.End();
        }

        public void ReactOnStateChange(object source, OnStateChangeArgs args)
        {
            switch (args.CurrentState)
            {
                case FarmLandState.EMPTY:
                    _spriteGrid[1][args.Position].Gid = (int)SpriteType.AIR;
                    break;
                case FarmLandState.FULLGROWN:
                    _spriteGrid[1][args.Position].Gid = 5903;
                    break;
                case FarmLandState.HALFGROWN:
                    _spriteGrid[1][args.Position].Gid = 5902;
                    break;
                case FarmLandState.OVERGROWN:
                    _spriteGrid[1][args.Position].Gid = 5905;
                    break;
                case FarmLandState.SEED:
                    _spriteGrid[1][args.Position].Gid = 5900;
                    break;
            }

            if (args.IsWatered)
            {
                _spriteGrid[0][args.Position].Gid = 825;
            }
            else
            {
                _spriteGrid[0][args.Position].Gid = (int)SpriteType.FARM_LAND;
            }

            Console.WriteLine(args.CurrentState);
        }

        private void IntializeLayerOne()
        {
            XmlNode data = _tileHandler.LevelOneTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                _spriteGrid[0][i] = new SpriteObject(_spriteSheet, gid);
            }
        }

        private void IntializeLayerTwo()
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

        private void IntializeLayerThree()
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

        private void IntializeLayerFour()
        {
            XmlNode data = _tileHandler.LevelFourTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");

            for (int i = 0; i < tiles.Count; i++)
            {
                int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                _spriteGrid[3][i] = new SpriteObject(_spriteSheet, gid);
            }
        }
    }
}