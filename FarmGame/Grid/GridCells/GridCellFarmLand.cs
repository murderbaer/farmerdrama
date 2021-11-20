using OpenTK.Mathematics;

namespace FarmGame
{
    public class GridCellFarmLand : GridCell
    {
        public GridCellFarmLand(FarmLandState state)
        {
            State = state;
        }



        // public override Color4 CellColor
        // {
        //     get
        //     {
        //         switch (State)
        //         {
        //             case FarmLandState.SEED:
        //                 return Color4.GreenYellow;
        //             case FarmLandState.HALFGROWN:
        //                 return Color4.LimeGreen;
        //             case FarmLandState.FULLGROWN:
        //                 return Color4.SeaGreen;
        //             case FarmLandState.OVERGROWN:
        //                 return Color4.DarkGreen;
        //             default:
        //                 return Color4.Brown;
        //         }
        //     }
        // }

        public bool IsWatered { get; set; }

        public FarmLandState State { get; set; }

        public override Item TakeItem()
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

        public override bool InteractWithItem(Item item)
        {
            if (item.Type == ItemType.WATERBUCKET & !IsWatered)
            {
                IsWatered = true;
                return true;
            }
            else if (item.Type == ItemType.SEED & State == FarmLandState.EMPTY)
            {
                State = FarmLandState.SEED;
                return true;
            }

            return false;
        }
    }
}