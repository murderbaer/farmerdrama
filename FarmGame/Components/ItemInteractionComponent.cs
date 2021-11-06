using OpenTK.Windowing.Common;

namespace FarmGame
{
    public class ItemInteractionComponent
    {
        public void OnKeyPress(KeyboardKeyEventArgs args, ref World world)
        {
            var playerX = (int)world.Player.Position.X;
            var playerY = (int)world.Player.Position.Y;
            IGridCell cell = world.Grid[playerX, playerY];
            world.Player.Interact(cell);
            System.Console.WriteLine("Space pressed");
        }
    }
}