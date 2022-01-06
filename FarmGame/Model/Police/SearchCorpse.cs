using FarmGame.Core;
using FarmGame.Model.Grid;
using FarmGame.Services;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class SearchCorpse : IUpdatable
    {
        private Police _police;

        private Suspicion _suspicion;

        private Corpse _corpse;

        private UpdateService _updateService;

        private DataGrid _grid;

        public SearchCorpse(GameObject goPolice, GameObject goSuspicion, GameObject goCorpse, GameObject goGrid)
        {
            _police = goPolice.GetComponent<Police>();
            _suspicion = goSuspicion.GetComponent<Suspicion>();
            _corpse = goCorpse.GetComponent<Corpse>();
            _grid = goGrid.GetComponent<DataGrid>();
            _updateService = goGrid.Scene.GetService<UpdateService>();
        }

        public bool IsFound { get; set; }

        public float CalculateSearchDistance()
        {
            var suspicion = _suspicion.Value;
            var corpsePosition = _corpse.Position;
            var hiddenFactor = _grid[(int)corpsePosition.X, (int)corpsePosition.Y].HiddenFactor;
            var holdFactor = _corpse.IsPlaced ? 1f : 1.3f;

            return 4 * (1 + (suspicion / 100)) * hiddenFactor * holdFactor;
        }

        public bool Search()
        {
            if (IsFound)
            {
                return true;
            }

            var distance = CalculateSearchDistance();
            var corpsePosition = _corpse.Position;
            var policePosition = _police.Position;

            IsFound = Vector2.Distance(corpsePosition, policePosition) < distance;
            if (IsFound)
            {
                FinishSearch();
            }

            return IsFound;
        }

        public void FinishSearch()
        {
            _updateService.IsFinished = true;
            _police.EndPath();
        }

        public void Update(float elapsedTime)
        {
            if (!_police.IsMoving() && !IsFound)
            {
                _police.StartRandomPath();
            }

            Search();
        }
    }
}