using System;

namespace FarmGame.Model.GridCells
{
    public class GridCellFarmLand : GridCell
    {
        private readonly int _postition;

        private float _farmStateProgress = 0f;
        private float _farmStateNextPhase = 20f;

        private float _secondsIntervallProgress = 0f;

        public GridCellFarmLand(FarmLandState state, int position)
        {
            State = state;
            _postition = position;
        }

        public event EventHandler<OnStateChangeArgs> OnStateChange;

        public float FarmLandGrowthRate { get; set; } = 1f;

        public override float HiddenFactor
        {
            get
            {
                switch (State)
                {
                    case FarmLandState.SEED:
                        return 1f;
                    case FarmLandState.HALFGROWN:
                        return 0.7f;
                    case FarmLandState.FULLGROWN:
                        return 0.5f;
                    case FarmLandState.OVERGROWN:
                        return 0.8f;
                    default:
                        return 1f;
                }
            }
        }

        public bool IsWatered { get; set; }

        public FarmLandState State { get; set; } = FarmLandState.EMPTY;

        public override Item TakeItem()
        {
            Item returnItem;
            switch (State)
            {
                case FarmLandState.FULLGROWN:
                    State = FarmLandState.EMPTY;
                    returnItem = new Item(ItemType.WHEET); break;
                case FarmLandState.OVERGROWN:
                    State = FarmLandState.EMPTY;
                    returnItem = new Item(ItemType.EMPTY); break;
                default:
                    returnItem = new Item(ItemType.EMPTY); break;
            }

            OnStateChange?.Invoke(null, CreateStateEventArgs());
            return returnItem;
        }

        public override bool InteractWithItem(Item item)
        {
            bool success = false;
            if (item.Type == ItemType.WATERBUCKET & !IsWatered)
            {
                IsWatered = true;
                FarmLandGrowthRate += 0.2f;
                success = true;
            }
            else if (item.Type == ItemType.SEED & State == FarmLandState.EMPTY)
            {
                State = FarmLandState.SEED;
                success = true;
            }

            OnStateChange?.Invoke(null, CreateStateEventArgs());
            return success;
        }

        public void ProgressState()
        {
            switch (State)
            {
                case FarmLandState.SEED:
                    State = FarmLandState.HALFGROWN;
                    break;
                case FarmLandState.HALFGROWN:
                    State = FarmLandState.FULLGROWN;
                    IsWatered = false;
                    FarmLandGrowthRate -= 0.2f;
                    break;
                case FarmLandState.FULLGROWN:
                    State = FarmLandState.OVERGROWN;
                    break;
            }

            OnStateChange?.Invoke(null, CreateStateEventArgs());
        }

        public override void Update(float elapsedTime)
        {
            if (State == FarmLandState.EMPTY || State == FarmLandState.OVERGROWN)
            {
                return;
            }

            _secondsIntervallProgress += elapsedTime;
            if (_secondsIntervallProgress > 1f)
            {
                _secondsIntervallProgress = 0f;
                _farmStateProgress += FarmLandGrowthRate;
            }

            if (_farmStateProgress > _farmStateNextPhase)
            {
                ProgressState();
                _farmStateProgress = 0;
            }
        }

        private OnStateChangeArgs CreateStateEventArgs()
        {
            return new OnStateChangeArgs(IsWatered, State, _postition);
        }
    }
}