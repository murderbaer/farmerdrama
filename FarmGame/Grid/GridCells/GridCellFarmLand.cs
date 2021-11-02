using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class GridCellFarmLand : IGridCell
    {
        public GridCellFarmLand(FarmLandState state)
        {
            State = state;
        }

        public GridCellFarmLand()
        {
        }

        public bool HasCollision { get; } = false;

        public Color4 CellColor
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

        public bool IsWatered { get; set; }

        public FarmLandState State { get; set; }

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
            // TODO: Grow
        }

        public Item TakeItem()
        {
            switch (State)
            {
                case FarmLandState.FULLGROWN:
                    State = FarmLandState.EMPTY;
                    return new Item(ItemType.WHEET);
                case FarmLandState.OVERGROWN:
                    State = FarmLandState.EMPTY;
                    return new Item(ItemType.EMPTY);
                default:
                    return new Item(ItemType.EMPTY);
            }
        }

        public bool InteractWithItem(Item item)
        {
            if (item.Type == ItemType.WATERBUCKET & !IsWatered)
            {
                IsWatered = true;
            }
            else if (item.Type == ItemType.SEED & State == FarmLandState.EMPTY)
            {
                State = FarmLandState.SEED;
            }

            return false;
        }
    }
}