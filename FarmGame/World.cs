using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
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
            Corpse = new Corpse();
            Movable = new Movable();
            Suspicion = new Suspicion(30);
            Camera.CameraPosition = Player.Position;
        }

        public GameWindow Window { get; set; }

        public IReadOnlyGrid Grid { get; set; }

        public ICamera Camera { get; }

        public IPlayer Player { get; }

        public Corpse Corpse { get; }

        public IMovable Movable { get; }

        public Suspicion Suspicion { get; }

        public ItemInteractionComponent ItemInteractionComponent { get; } = new ItemInteractionComponent();

        public CorpseInteractionComponent CorpseInteractionComponent { get; } = new CorpseInteractionComponent();

        public void Draw()
        {
            DrawBackground();
            Camera.SetCameraMatrix();
            Grid.Draw();
            Player.Draw();
            Corpse.Draw();
            Movable.Draw();
            Camera.SetOverlayMatrix();
            Suspicion.Draw();
        }

        public void Update(float elapsedTime)
        {
            Grid.Update(elapsedTime, this);
            Player.Update(elapsedTime, this);
            Camera.CameraFocus = Player.Position;
            Camera.Update(elapsedTime, this);
            Corpse.Update(elapsedTime, this);
            Movable.Update(elapsedTime, this);

            if (!Movable.IsMoving())
            {
                Movable.StartRandomPath();
            }
        }

        private void DrawBackground()
        {
            Camera.SetOverlayMatrix();
            GL.Color4(Color4.LightGray);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 9);
            GL.Vertex2(16, 9);
            GL.Vertex2(16, 0);
            GL.End();
        }
    }
}