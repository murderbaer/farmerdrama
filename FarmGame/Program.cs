using FarmGame;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled

var scene = Game.LoadScene(window);
window.UpdateFrame += Update;
window.Resize += args => scene.Resize(args.Width, args.Height);
window.KeyDown += args =>
{
    switch (args.Key)
    {
        case Keys.F4:
            window.Close();
            break;
        case Keys.F11:
            window.WindowState = window.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
            break;
    }
};

window.RenderFrame += _ => GL.Clear(ClearBufferMask.ColorBufferBit); // Clear frame
window.RenderFrame += _ => scene.Draw(); // called once each frame; callback should contain drawing code
window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering

window.Run();

void Update(FrameEventArgs args)
{
    var elapsedTime = (float)args.Time;
    scene.Update(elapsedTime);
}
