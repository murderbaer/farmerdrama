using OpenTK.Windowing.Common;

namespace FarmGame
{
    public interface IKeyDownListener : IComponent
    {
        void KeyDown(KeyboardKeyEventArgs args);
    }

    public interface IKeyUpListener : IComponent
    {
        void KeyUp(KeyboardKeyEventArgs args);
    }
}