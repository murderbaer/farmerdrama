using System.Linq;

namespace FarmGame
{
    public class Eating : IUpdatable
    {
        private IPosition _position;

        private Hunger _hunger;

        private DataGrid _grid;

        public Eating(GameObject go, GameObject grid)
        {
            _position = go.GetComponent<IPosition>();
            _hunger = go.GetComponent<Hunger>();
            _grid = grid.GetComponent<DataGrid>();
        }

        public void Update(float elapsedTime)
        {
            foreach (GridCellFeeder feeder in _grid.GetCellsNearby(_position.Position, 3).OfType<GridCellFeeder>())
            {
                if (_hunger.HungerCounter > feeder.FillState)
                {
                    _hunger.Feed(feeder.FillState);
                    feeder.FillState = 0;
                }
                else
                {
                    feeder.FillState -= _hunger.HungerCounter;
                    _hunger.Feed(_hunger.HungerCounter);
                }

                return;
            }
        }
    }
}