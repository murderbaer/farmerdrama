using System;
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

                BoardX = int.Parse(_doc.DocumentElement.GetAttribute("width"));
                BoardY = int.Parse(_doc.DocumentElement.GetAttribute("height"));
                TilePixelSize = int.Parse(_doc.DocumentElement.GetAttribute("tilewidth"));
                LevelOneTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='groundLayer']");
                LevelTwoTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='smallObjectLayer']");
                LevelThreeTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='collisionLayer']");
                LevelFourTiles = _doc.DocumentElement.SelectSingleNode("layer[@name='aboveLayer']");

                TiledPlayerPos = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='playerPos']");
                TiledCorpsePos = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='corpsePos']");
                TiledPolicePaths = _doc.DocumentElement.SelectSingleNode("objectgroup[@name='policePaths']");
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

        public int TilePixelSize { get; private set; }

        public XmlNode TiledPlayerPos { get; private set; }

        public XmlNode TiledCorpsePos { get; private set; }

        public XmlNode TiledPolicePaths { get; private set; }

        public XmlNode LevelOneTiles { get; private set; }

        public XmlNode LevelTwoTiles { get; private set; }

        public XmlNode LevelThreeTiles { get; private set; }

        public XmlNode LevelFourTiles { get; private set; }

        public int BoardX { get; private set; }

        public int BoardY { get; private set; }

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