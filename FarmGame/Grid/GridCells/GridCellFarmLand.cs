using System;

namespace FarmGame
{
    public class GridCellFarmLand : GridCell
    {
        private readonly int _postition;

        public GridCellFarmLand(FarmLandState state, int position)
        {
            State = state;
            _postition = position;
        }

        public event EventHandler<OnStateChangeArgs> OnStateChange;
        /*  public override Color4 CellColor
            {
                get
                {
                    switch (State)
                    {
                        case FarmLandState.SEED:
                            return Color4.GreenYellow;
                        case FarmLandState.HALFGROWN:
                            return Color4.LimeGreen;
                        case FarmLandState.FULLGROWN:
                            return Color4.SeaGreen;
                        case FarmLandState.OVERGROWN:
                            return Color4.DarkGreen;
                        default:
                            return Color4.Brown;
                    }
                }
            }
        */

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

        public double BaseGrowthRate { get; set; } = 30f;

        public double GrowthRate { get => BaseGrowthRate / (IsWatered ? 1.2 : 1); }

        private double GrowthTimer { get; set; } = 0;

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
            bool sucess = false;
            if (item.Type == ItemType.WATERBUCKET & !IsWatered)
            {
                IsWatered = true;
                sucess = true;
            }
            else if (item.Type == ItemType.SEED & State == FarmLandState.EMPTY)
            {
                State = FarmLandState.SEED;
                sucess = true;
            }

            OnStateChange?.Invoke(null, CreateStateEventArgs());
            return sucess;
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

            GrowthTimer += elapsedTime;
            if (GrowthTimer >= GrowthRate)
            {
                GrowthTimer = 0;
                this.ProgressState();
            }
        }

        private OnStateChangeArgs CreateStateEventArgs()
        {
            return new OnStateChangeArgs(IsWatered, State, _postition);
        }
    }
}