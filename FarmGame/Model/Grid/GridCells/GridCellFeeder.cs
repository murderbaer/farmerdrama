using System;

using FarmGame.Core;

namespace FarmGame.Model.GridCells
{
    public class GridCellFeeder : GridCell
    {
        private static float _fillPerItem = 300;

        private static float _maxCapacity = 1000;

        private float _fillState = 1000;

        public GridCellFeeder(float hiddenFactor)
        : base(hiddenFactor)
        {
        }

        public float FillState
        {
            get
            {
                return _fillState;
            }

            set
            {
                _fillState = Math.Max(0, Math.Min(_maxCapacity, value));
            }
        }

        public override bool InteractWithItem(ItemType item)
        {
            if (item == ItemType.WHEET)
            {
                _fillState = Math.Min(_maxCapacity, _fillState + _fillPerItem);
                return true;
            }

            return false;
        }
    }
}