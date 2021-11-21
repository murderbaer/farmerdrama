using FarmGame;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled

World world = new World(window);
window.UpdateFrame += Update;
window.Resize += args => world.Camera.Resize(args.Width, args.Height);
window.KeyDown += args =>
{
    switch (args.Key)
    {
        case Keys.Escape:
            window.Close();
            break;
        case Keys.F:
            window.WindowState = window.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
            break;
        case Keys.Space:
            world.ItemInteractionComponent.OnKeyPress(args, ref world);
            break;
        case Keys.Q:
            world.CorpseInteractionComponent.OnKeyPress(args, ref world);
            break;
    }
};

window.RenderFrame += _ => GL.Clear(ClearBufferMask.ColorBufferBit); // Clear frame
window.RenderFrame += _ => world.Draw(); // called once each frame; callback should contain drawing code
window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering

window.Run();

void Update(FrameEventArgs args)
{
    var elapsedTime = (float)args.Time;
    world.Update(elapsedTime);
}
