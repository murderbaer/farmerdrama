using System.Linq;

using FarmGame.Core;
using FarmGame.Model.Grid;

namespace FarmGame.Model
{
    public class CorpseWaterPoisening : IUpdatable
    {
        private IPosition _position;

        private DataGrid _grid;

        public CorpseWaterPoisening(GameObject goCorpse, GameObject goGrid)
        {
            _position = goCorpse.GetComponent<IPosition>();
            _grid = goGrid.GetComponent<DataGrid>();
        }

        public void Update(float elapsedTime)
        {
            foreach (GridCellWater water in _grid.GetCellsNearby(_position.Position, 3).OfType<GridCellWater>())
            {
                water.PoisenCounter = 3;
            }
        }
    }
}