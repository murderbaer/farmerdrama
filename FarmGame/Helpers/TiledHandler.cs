using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

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

                LevelOneTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='groundLayer']");
                LevelTwoTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='smallObjectLayer']");
                LevelThreeTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='collisionLayer']");
                LevelFourTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='aboveLayer']");
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

        public XmlNode LevelOneTiles { get; private set; }

        public XmlNode LevelTwoTiles { get; private set; }

        public XmlNode LevelThreeTiles { get; private set; }

        public XmlNode LevelFourTiles { get; private set; }

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
    }
}