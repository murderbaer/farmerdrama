using System;

using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model.Grid;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model
{
    public class Corpse : IPosition, IUpdatable, IKeyDownListener, ICollidable
    {
        private Player _player;

        private DataGrid _grid;

        private TiledHandler _tiledHandler = TiledHandler.Instance;

        public Corpse(GameObject goPlayer, GameObject goCorpse)
        {
            Position = _tiledHandler.TiledCorpsePos;

            _player = goPlayer.GetComponent<Player>();
            _grid = goCorpse.GetComponent<DataGrid>();
        }

        public bool IsPlaced { get; set; } = true;

        public Vector2 Position { get; set; }

        public Vector2 MovementVector { get; set; } = Vector2.Zero;

        public float MovementSpeed { get; set; } = 0f;

        public float CollisionRadius { get; } = 0.6f;

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