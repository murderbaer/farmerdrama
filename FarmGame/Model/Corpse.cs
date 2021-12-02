using System;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class Corpse : IPosition, IUpdatable, IKeyDownListener, ICollidable
    {
        private Player _player;

        private DataGrid _grid;

        private TiledHandler _tiledHandler = TiledHandler.Instance;

        public Corpse(GameObject goPlayer, GameObject goCorpse)
        {
            var corpsePos = _tiledHandler.TiledCorpsePos.SelectNodes("object");
            float posX = float.Parse(corpsePos[0].Attributes["x"].Value);
            float posY = float.Parse(corpsePos[0].Attributes["y"].Value);
            int pixelSize = _tiledHandler.TilePixelSize;
            Position = new Vector2(posX / pixelSize, posY / pixelSize);

            _player = goPlayer.GetComponent<Player>();
            _grid = goCorpse.GetComponent<DataGrid>();
        }

        public bool IsPlaced { get; set; } = true;

        public Vector2 Position { get; set; }

        public float CollisionRadius { get; } = 0.4f;

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
                    PlacedOnFarmLand();
                    return;
                }

                var playerPos = _player.Position;
                var distance = Vector2.Distance(playerPos, Position);
                if (distance < 1)
                {
                    RemovedFromFarmLand();
                    IsPlaced = false;
                }
            }
        }

        private void PlacedOnFarmLand()
        {
            int x = (int)Math.Floor(_player.Position.X);
            int y = (int)Math.Floor(_player.Position.Y);

            if (_grid[x, y].GetType() == typeof(GridCellFarmLand))
            {
                var temp = (GridCellFarmLand)_grid[x, y];
                temp.FarmLandGrowthRate += 0.3f;
            }
        }

        private void RemovedFromFarmLand()
        {
            int x = (int)Math.Floor(_player.Position.X);
            int y = (int)Math.Floor(_player.Position.Y);

            if (_grid[x, y].GetType() == typeof(GridCellFarmLand))
            {
                var temp = (GridCellFarmLand)_grid[x, y];
                temp.FarmLandGrowthRate -= 0.3f;
            }
        }
    }
}