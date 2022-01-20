using FarmGame;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public class Program
    {
        private static Core.Scene _scene;

        public static void Main(string[] args)
        {
            var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled

            _scene = Game.LoadScene(window);
            AddListeners(window, _scene);

            window.Run();
        }

        private static void AddListeners(GameWindow window, Core.Scene scene)
        {
            window.UpdateFrame += Update;
            window.Resize += args => scene.Resize(args.Width, args.Height);
            window.RenderFrame += _ => GL.Clear(ClearBufferMask.ColorBufferBit); // Clear frame
            window.RenderFrame += _ => scene.Draw(); // called once each frame; callback should contain drawing code
            window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering

            window.KeyDown += scene.KeyDown;
            window.KeyUp += scene.KeyUp;
        }

        private static void Update(FrameEventArgs args)
        {
            var elapsedTime = (float)args.Time;
            _scene.Update(elapsedTime);
        }
    }
}
