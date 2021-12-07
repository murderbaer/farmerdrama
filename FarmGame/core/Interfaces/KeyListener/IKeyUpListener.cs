using OpenTK.Windowing.Common;

namespace FarmGame.Core
{
    public interface IKeyUpListener : IComponent
    {
        void KeyUp(KeyboardKeyEventArgs args);
    }
}