using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class World : IWorld
    {
        public World()
        {
            Camera = new Camera();
            Grid = new Grid(32, 18);
            Player = new Player();
            Camera.CameraPosition = Player.Position;
        }

        public IReadOnlyGrid Grid { get; set; }

        public ICamera Camera { get; }

        public IPlayer Player { get; }

        public ItemInteractionComponent ItemInteractionComponent = new ItemInteractionComponent();

        public void Draw()
        {
            Camera.SetCameraMatrix();
            Grid.Draw();
            Player.Draw();
        }

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
            Grid.Update(elapsedTime, ref keyboard);
            Player.Update(elapsedTime, ref keyboard);
            Camera.CameraFocus = Player.Position;
            Camera.Update(elapsedTime, ref keyboard);
        }
    }
}