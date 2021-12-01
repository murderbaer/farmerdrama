using System;

namespace FarmGame
{
    public interface IChangeDirection : IComponent
    {
        event EventHandler<OnChangeDirectionArgs> OnChangeDirection;
    }
}