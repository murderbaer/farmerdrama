// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability }); // window with immediate mode rendering enabled

float x = 0f;
window.UpdateFrame += Update;
window.Resize += Resize;
window.KeyDown += args => { if (args.Key == Keys.Escape)
{
    window.Close();
}
};

window.RenderFrame += Draw; // called once each frame; callback should contain drawing code
window.RenderFrame += _ => window.SwapBuffers(); // buffer swap needed for double buffering

// setup code executed once
GL.ClearColor(Color4.LightGray);

window.Run();

void Draw(FrameEventArgs args)
{
    // clear screen
    GL.Clear(ClearBufferMask.ColorBufferBit);
    GL.Color4(Color4.IndianRed);

    // draw a quad
    GL.Begin(PrimitiveType.Quads);
    GL.Vertex2(0.0f, 0.0f); // draw first quad corner
    GL.Vertex2(0.5f, 0.0f);
    GL.Vertex2(0.5f, 0.5f);
    GL.Vertex2(0.0f, 0.5f);
    GL.End();
}

void Resize(ResizeEventArgs args)
{
    GL.Viewport(0, 0, args.Width, args.Height);
}

void Update(FrameEventArgs args)
{
    var elapsedTime = (float)args.Time;
    if (window.IsKeyDown(Keys.Left))
    {
        x -= elapsedTime;
    }

    if (window.IsKeyDown(Keys.Right))
    {
        x += elapsedTime;
    }
}
