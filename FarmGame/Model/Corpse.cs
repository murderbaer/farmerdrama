using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class Corpse : IPosition, IUpdatable, IKeyDownListener
    {
        private Player _player;

        public Corpse(GameObject goPlayer)
        {
            Position = new Vector2(0, 0);
            _player = goPlayer.GetComponent<Player>();
        }

        public bool IsPlaced { get; set; } = true;

        public Vector2 Position { get; set; }

        public void Update(float elapsedTime)
        {
            if (!IsPlaced)
            {
                Position = _player.Position;
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            System.Console.WriteLine("KeyDown");
            if (args.Key == Keys.Q)
            {
                if (!IsPlaced)
                {
                    IsPlaced = true;
                    return;
                }

                var playerPos = _player.Position;
                var distance = Vector2.Distance(playerPos, Position);
                if (distance < 1)
                {
                    IsPlaced = false;
                }
            }
        }
    }
}