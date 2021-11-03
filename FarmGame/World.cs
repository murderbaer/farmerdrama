using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public class World : IWorld
    {
        public World(GameWindow window)
        {
            Window = window;
            Camera = new Camera();
            Grid = new Grid(32, 18);
            Player = new Player();
            Camera.CameraPosition = Player.Position;
        }

        public GameWindow Window { get; set; }

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

        public void Update(float elapsedTime)
        {
            Grid.Update(elapsedTime, this);
            Player.Update(elapsedTime, this);
            Camera.CameraFocus = Player.Position;
            Camera.Update(elapsedTime, this);
        }
    }
}