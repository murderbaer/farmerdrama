using System;

namespace FarmGame
{
    public class OnStateChangeArgs : EventArgs 
    {
        public OnStateChangeArgs(bool watered, FarmLandState state, int pos)
        {
            IsWatered = watered;
            CurrentState = state;
            Position = pos;
        }

        public bool IsWatered { get; private set; }
        public FarmLandState CurrentState { get; private set; }
        public int Position { get; private set; }
    }
}