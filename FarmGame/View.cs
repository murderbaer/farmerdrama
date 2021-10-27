using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


namespace FarmGame
{
    internal class View
    {
        public View()
        {
            GL.ClearColor(Color4.Black);
        }

        internal Camera Camera { get; } = new Camera();

        internal void Draw(Model model)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Camera.SetCameraFocus(model.Player.Position.X, model.Player.Position.Y);
            DrawDemo();
            model.Player.Draw();
        }

        internal void Resize(ResizeEventArgs args)
        {
            Camera.Resize(args.Width, args.Height);
        }

        private void DrawDemo()
        {
            Camera.SetOverlayMatrix();
            GL.Color4(Color4.LightGray);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 9);
            GL.Vertex2(16, 9);
            GL.Vertex2(16, 0);
            GL.End();

            Camera.SetCameraMatrix();

            GL.Color4(Color4.IndianRed);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-2, -2);
            GL.Vertex2(2, -2);
            GL.Vertex2(2, 2);
            GL.Vertex2(-2, 2);
            GL.End();
        }
    }
}