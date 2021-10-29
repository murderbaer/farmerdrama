using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;

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
            DrawBoard(model.Grid);
            DrawPlayer(model.Player);
        }

        private void DrawPlayer(Player player)
        {
            GL.Color4(Color4.Orange);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(player.Position.X - 0.5, player.Position.Y);
            GL.Vertex2(player.Position.X + 0.5, player.Position.Y);
            GL.Vertex2(player.Position.X, player.Position.Y - 1);
            GL.End();
            Console.WriteLine(player.Position.X + " " + player.Position.Y);
        }

        internal void Resize(ResizeEventArgs args)
        {
            Camera.Resize(args.Width, args.Height);
        }

        private void DrawGrid(IReadOnlyGrid grid)
        {
            for (int column = 0; column < grid.Column; ++column)
            {
                for (int row = 0; row < grid.Row; ++row)
                {
                    GridCell cell = grid[column, row];
                    switch (cell.GridType)
                    {
                        case GridType.EARTH: 
                            GL.Color4(Color4.Green);
                            break;
                        case GridType.WATER: 
                            GL.Color4(Color4.Blue);
                            break;
                        case GridType.SAND: 
                            GL.Color4(Color4.Yellow);
                            break;
                    }
                    Quad(grid, column, row);
                }
            }

        }
        private void DrawBoard(Grid g)
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
            DrawGrid(g);
        }

        private void Quad(Vector2 min, Vector2 size)
        {
            var max = min + size;
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(min);
            GL.Vertex2(min.X, max.Y);
            GL.Vertex2(max);
            GL.Vertex2(max.X, min.Y);
            GL.End();
        }

        private void Quad(IReadOnlyGrid grid, int column, int row)
        {
            var size = new Vector2(1, 1);
            var min = new Vector2(-16, -9) + new Vector2(column, row) * size;
            Quad(min, size);
        }
    }
}