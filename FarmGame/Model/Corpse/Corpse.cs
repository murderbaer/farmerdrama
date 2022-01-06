using System;
using System.Linq;

using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model.Grid;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class Corpse : IPosition, IUpdatable, ICollidable
    {
        private DataGrid _grid;

        private TiledHandler _tiledHandler = TiledHandler.Instance;

        public Corpse(GameObject goCorpse)
        {
            Position = _tiledHandler.TiledCorpsePos;

            _grid = goCorpse.GetComponent<DataGrid>();
        }

        public Vector2 Position { get; set; }

        public Vector2 MovementVector { get; set; } = Vector2.Zero;

        public float MovementSpeed { get; set; } = 0f;

        public float CollisionRadius { get; } = 0.6f;

        public void Update(float elapsedTime)
        {
            foreach (GridCellFarmLand farmland in _grid.GetCellsNearby(Position, 3).OfType<GridCellFarmLand>())
            {
                farmland.FarmLandGrowthRate = 1.3f;
            }
        }
    }
}