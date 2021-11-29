using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class Corpse : IPosition, IUpdatable, IKeyDownListener
    {
        private Player _player;

        private TiledHandler _tiledHandler = TiledHandler.Instance;

        public Corpse(GameObject goPlayer)
        {
            var corpsePos = _tiledHandler.TiledCorpsePos.SelectNodes("object");
            float posX = float.Parse(corpsePos[0].Attributes["x"].Value);
            float posY = float.Parse(corpsePos[0].Attributes["y"].Value);
            int pixelSize = _tiledHandler.TilePixelSize;
            Position = new Vector2(posX / pixelSize, posY / pixelSize);

            _player = goPlayer.GetComponent<Player>();
        }

        #if DEBUG
        public Corpse(Vector2 pos)
        {
            Position = pos;
        }
        #endif

        public bool IsPlaced { get; set; } = true;

        public Vector2 Position { get; set; }

        public void Update(float elapsedTime)
        {
            if (!IsPlaced)
            {
                Position = _player.Position;
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (args.Key == Keys.Q)
            {
                if (!IsPlaced)
                {
                    IsPlaced = true;
                    return;
                }

                var playerPos = _player.Position;
                var distance = Vector2.Distance(playerPos, Position);
                if (distance < 1)
                {
                    IsPlaced = false;
                }
            }
        }
    }
}