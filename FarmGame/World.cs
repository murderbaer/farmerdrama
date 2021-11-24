#if false // to be deleted
using ImageMagick;
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
            Grid = new Grid();
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

        public ICorpse Corpse { get; }

        public IMovable Movable { get; }

        public Suspicion Suspicion { get; }

        public ItemInteractionComponent ItemInteractionComponent { get; } = new ItemInteractionComponent();

        public CorpseInteractionComponent CorpseInteractionComponent { get; } = new CorpseInteractionComponent();

        public CorpseSearchComponent CorpseSearchComponent { get; } = new CorpseSearchComponent();

        #if DEBUG
        public FreeCamComponent FreeCamComponent { get; } = new FreeCamComponent();
        #endif

        public void Draw()
        {
            DrawBackground();
            Camera.SetCameraMatrix();
            Grid.DrawLayer(0);
            Grid.DrawLayer(1);
            Grid.DrawLayer(2);
            Player.Draw();
            Corpse.Draw();
            Movable.Draw();
            Grid.DrawLayer(3);
            Camera.SetOverlayMatrix();
            Suspicion.Draw();
        }

        public void Update(float elapsedTime)
        {
            Grid.Update(elapsedTime, this);
            Player.Update(elapsedTime, this);
            Camera.CameraFocus = Player.Position;
            #if DEBUG
            if (FreeCamComponent.IsActive)
            {
                FreeCamComponent.Update(elapsedTime, this);
            }
            #endif

            Camera.Update(elapsedTime, this);
            Corpse.Update(elapsedTime, this);
            Movable.Update(elapsedTime, this);
            CorpseSearchComponent.Update(elapsedTime, this);

            if (!Movable.IsMoving() && !CorpseSearchComponent.IsFound)
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
#endif