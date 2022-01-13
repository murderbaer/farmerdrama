using OpenTK.Mathematics;

namespace FarmGame.Core
{
    public interface IInputDevice
    {
        Vector2 PlayerDirection { get; }
        
        Vector2 CameraDirection { get; }

        bool Close { get; }

        bool DetachCamera { get; }

        bool Pause { get; }
        
        bool Interact { get; }

        bool XAnswer { get; }

        bool YAnswer { get; }

        bool Fullscreen { get; }
    }
}