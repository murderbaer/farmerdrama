using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

using FarmGame.Model;
using OpenTK.Mathematics;

namespace FarmGame.Helpers
{
    public class TiledHandler
    {
        private static TiledHandler _instance = null;

        private XmlDocument _doc;

        // TODO: Refactor this in more methods/classes
        private TiledHandler()
        {
            if (_instance != null)
            {
                throw new UnauthorizedAccessException("No second object is allowed to be created!");
            }

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FarmGame.Resources.Graphics.Map.tmx"))
            {
                _doc = new XmlDocument();
                _doc.Load(stream);

                InitLayers();
                SquashedLayers = SquashGrids(LayerOne, LayerTwo);
                GenerateHiddenFactorGrid();
            }
        }

        public static TiledHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TiledHandler();
                }

                return _instance;
            }
        }

        public int TilePixelSize
        {
            get
            {
                return int.Parse(_doc.DocumentElement.GetAttribute("tilewidth"));
            }
        }

        public Vector2 TiledPlayerPos
        {
            get
            {
                var playerPos = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='playerPos']").SelectNodes("object");
                float posX = float.Parse(playerPos[0].Attributes["x"].Value);
                float posY = float.Parse(playerPos[0].Attributes["y"].Value);
                return new Vector2(posX / TilePixelSize, posY / TilePixelSize);
            }
        }

        public Vector2 TiledCorpsePos
        {
            get
            {
                var corpsePos = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='corpsePos']").SelectNodes("object");
                float posX = float.Parse(corpsePos[0].Attributes["x"].Value);
                float posY = float.Parse(corpsePos[0].Attributes["y"].Value);
                return new Vector2(posX / TilePixelSize, posY / TilePixelSize);
            }
        }

        public Vector2 TiledPolicePos
        {
            get
            {
                var corpsePos = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='policePos']").SelectNodes("object");
                float posX = float.Parse(corpsePos[0].Attributes["x"].Value);
                float posY = float.Parse(corpsePos[0].Attributes["y"].Value);
                return new Vector2(posX / TilePixelSize, posY / TilePixelSize);
            }
        }

        public List<List<Vector2>> TiledPolicePaths
        {
            get
            {
                var vectorPath = new List<List<Vector2>>();
                var policePos = TiledPolicePos;
                var paths = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='policePaths']").SelectNodes("object");

                float offset_x = 0f;
                float offset_y = 0f;
                for (int i = 0; i < paths.Count; i++)
                {
                    offset_x = float.Parse(paths[i].Attributes["x"].Value);
                    offset_y = float.Parse(paths[i].Attributes["y"].Value);
                    string polPath = paths[i].SelectNodes("polygon")[0].Attributes["points"].Value;
                    List<Vector2> singelPath = new List<Vector2>();
                    string[] cords = polPath.Split(' ');

                    for (int j = 0; j < cords.Length; j++)
                    {
                        string[] singleCoord = cords[j].Split(',');
                        float x = (float.Parse(singleCoord[0]) + offset_x) / 16f;
                        float y = (float.Parse(singleCoord[1]) + offset_y) / 16f;
                        singelPath.Add(new Vector2(x, y));
                    }

                    singelPath.Add(policePos);
                    vectorPath.Add(singelPath);
                }

                return vectorPath;
            }
        }

        public int[] LayerOne { get; private set; }

        public int[] LayerTwo { get; private set; }

        public int[] LayerThree { get; private set; }

        public int[] LayerFour { get; private set; }

        public int[] SquashedLayers { get; private set; }

        public float[] HiddenFactorGrid { get; private set; }

        public int BoardX
        {
            get
            {
                return int.Parse(_doc.DocumentElement.GetAttribute("width"));
            }
        }

        public int BoardY
        {
            get
            {
                return int.Parse(_doc.DocumentElement.GetAttribute("height"));
            }
        }

        public Box2 PigPen
        {
            get
            {
                var pigPen = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='PigPen']").SelectNodes("object");
                float posX = float.Parse(pigPen[0].Attributes["x"].Value) / 16;
                float posY = float.Parse(pigPen[0].Attributes["y"].Value) / 16;
                float width = float.Parse(pigPen[0].Attributes["width"].Value) / 16;
                float height = float.Parse(pigPen[0].Attributes["height"].Value) / 16;
                return new Box2(posX, posY, posX + width, posY + height);
            }
        }

        private int[] SquashGrids(int[] board1, int[] board2)
        {
            int gridSize = board1.Length;
            int[] ret = new int[gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                if (board2[i] == (int)SpriteType.AIR)
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

        private void GenerateHiddenFactorGrid()
        {
            var tiledSuspicionHeatMap = _doc.DocumentElement.SelectSingleNode("layer[@name='suspicionHeatmap']");
            XmlNode data = tiledSuspicionHeatMap.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");
            int gridSize = tiles.Count;

            HiddenFactorGrid = new float[gridSize];
            for (int i = 0; i < tiles.Count; i++)
            {
                HiddenFactorGrid[i] = 1f / int.Parse(tiles[i].Attributes["gid"].Value);
            }
        }

        private void InitLayers()
        {
            var tiledLevelOneTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='groundLayer']");
            XmlNode data = tiledLevelOneTiles.SelectSingleNode("data");
            XmlNodeList tiles = data.SelectNodes("tile");
            int gridSize = tiles.Count;

            LayerOne = new int[gridSize];
            for (int i = 0; i < tiles.Count; i++)
            {
                LayerOne[i] = int.Parse(tiles[i].Attributes["gid"].Value);
            }

            var tiledLevelTwoTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='smallObjectLayer']");
            LayerTwo = new int[gridSize];
            data = tiledLevelTwoTiles.SelectSingleNode("data");
            tiles = data.SelectNodes("tile");
            for (int i = 0; i < tiles.Count; i++)
            {
                LayerTwo[i] = int.Parse(tiles[i].Attributes["gid"].Value);
            }

            var tiledLevelThreeTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='collisionLayer']");
            LayerThree = new int[gridSize];
            data = tiledLevelThreeTiles.SelectSingleNode("data");
            tiles = data.SelectNodes("tile");
            for (int i = 0; i < tiles.Count; i++)
            {
                LayerThree[i] = int.Parse(tiles[i].Attributes["gid"].Value);
            }

            var tiledLevelFourTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='aboveLayer']");
            LayerFour = new int[gridSize];
            data = tiledLevelFourTiles.SelectSingleNode("data");
            tiles = data.SelectNodes("tile");
            for (int i = 0; i < tiles.Count; i++)
            {
                LayerFour[i] = int.Parse(tiles[i].Attributes["gid"].Value);
            }
        }
    }
}