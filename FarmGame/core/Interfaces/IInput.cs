using OpenTK.Mathematics;

namespace FarmGame.Core
{
    public interface IInput
    {
        Vector2 PlayerDirection { get; }
        
        Vector2 CameraDirection { get; }

        bool Close { get; }

        bool DetachCamera { get; }

        bool Pause { get; }
    }
}