using OpenTK.Mathematics;

namespace FarmGame.Core
{
    public interface IInput
    {
        Vector2 PlayerDirection { get; }

        bool Close { get; }
    }
}