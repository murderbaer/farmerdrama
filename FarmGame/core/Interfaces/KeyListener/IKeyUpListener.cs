using OpenTK.Windowing.Common;

namespace FarmGame
{
    public interface IKeyUpListener : IComponent
    {
        void KeyUp(KeyboardKeyEventArgs args);
    }
}