using OpenTK.Windowing.Common;

namespace FarmGame.Core
{
    public interface IKeyDownListener : IComponent
    {
        void KeyDown(KeyboardKeyEventArgs args);
    }
}