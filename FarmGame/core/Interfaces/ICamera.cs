namespace FarmGame.Core
{
    public interface ICamera : IComponent
    {
        void SetCameraMatrix();

        void SetOverlayMatrix();
    }
}