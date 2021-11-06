using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public interface IUpdatable
    {
        void Update(float elapsedTime, IWorld world);
    }
}