// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using FarmGame;

var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled
View view = new ();
Model model = new ();
Control control = new (model, view);

window.UpdateFrame += Update;
window.Resize += view.Resize;
window.KeyDown += args => { if (args.Key == Keys.Escape)
{
    window.Close();
}
};

window.RenderFrame += _ => view.Draw(model); // called once each frame; callback should contain drawing code
window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering


window.Run();

void Update(FrameEventArgs args)
{
    var elapsedTime = (float)args.Time;
    control.Update(elapsedTime, window.KeyboardState);
    model.Update(elapsedTime);
}
