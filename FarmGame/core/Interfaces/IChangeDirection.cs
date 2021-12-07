using System;

namespace FarmGame.Core
{
    public interface IChangeDirection : IComponent
    {
        event EventHandler<OnChangeDirectionArgs> OnChangeDirection;
    }
}