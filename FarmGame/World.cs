using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public class World : IWorld
    {
        public World(GameWindow window)
        {
            GlobalSprite = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.global.png");
            Window = window;
            Camera = new Camera();
            Grid = new Grid();
            Player = new Player();
            Movable = new Movable();
            Camera.CameraPosition = Player.Position;
        }

        public static int GlobalSprite { get; private set; }

        public GameWindow Window { get; set; }

        public IReadOnlyGrid Grid { get; set; }

        public ICamera Camera { get; }

        public IPlayer Player { get; }

        public IMovable Movable { get; }

        public ItemInteractionComponent ItemInteractionComponent { get; } = new ItemInteractionComponent();

        public void Draw()
        {
            DrawBackground();
            Camera.SetCameraMatrix();
            GL.BindTexture(TextureTarget.Texture2D, GlobalSprite);
            Grid.Draw();
            Player.Draw();
            Movable.Draw();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Update(float elapsedTime)
        {
            Grid.Update(elapsedTime, this);
            Player.Update(elapsedTime, this);
            Camera.CameraFocus = Player.Position;
            Camera.Update(elapsedTime, this);
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