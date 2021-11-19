using System;
using System.IO;
using System.Reflection;
using System.Xml;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class TiledHandler
    {
        private static TiledHandler _instance = null;

        private TiledHandler()
        {
            if (_instance != null)
            {
                throw new UnauthorizedAccessException("No second object is allowed to be created!");
            }

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FarmGame.Resources.Graphics.Map.tmx"))
            using (StreamReader reader = new StreamReader(stream))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);

                BoardX = int.Parse(doc.DocumentElement.GetAttribute("width"));
                BoardY = int.Parse(doc.DocumentElement.GetAttribute("height"));
                TilePixelSize = int.Parse(doc.DocumentElement.GetAttribute("tilewidth"));
                LevelOneTiles = doc.DocumentElement.SelectSingleNode("layer[@name='groundLayer']");
                LevelTwoTiles = doc.DocumentElement.SelectSingleNode("layer[@name='decorationLayer']");
                LevelThreeTiles = doc.DocumentElement.SelectSingleNode("layer[@name='walkLayer']");
                LevelFourTiles = doc.DocumentElement.SelectSingleNode("layer[@name='aboveLayer']");

                XmlNode objectsLayer = doc.DocumentElement.SelectSingleNode("objectgroup[@name='movement']");
                XmlNodeList objects = objectsLayer.SelectNodes("object");

                float posX = float.Parse(objects[0].Attributes["x"].Value);
                float posY = float.Parse(objects[0].Attributes["y"].Value);

                TiledPlayerPos = new Vector2(posX / TilePixelSize, posY / TilePixelSize);
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

        public Vector2 TiledPlayerPos { get; private set; }

        public XmlNode LevelOneTiles { get; private set; }

        public XmlNode LevelTwoTiles { get; private set; }

        public XmlNode LevelThreeTiles { get; private set; }

        public XmlNode LevelFourTiles { get; private set; }

        public int BoardX { get; private set; }

        public int BoardY { get; private set; }
    }
}