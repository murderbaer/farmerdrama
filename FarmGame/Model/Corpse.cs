using OpenTK.Mathematics;

namespace FarmGame
{
    public class Corpse : IPosition
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
    }
}