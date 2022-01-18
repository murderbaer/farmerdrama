using FarmGame.Core;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class Particle : IPosition
    {
        private Vector2 _speedVector = Vector2.Zero;

        private float _lifeTime;

        public Particle(Vector2 position, Vector2 initialSpeed, Color4 color, float lifeTime = 1f)
        {
            Position = position;
            _lifeTime = lifeTime;
            _speedVector = initialSpeed;
            Color = color;
        }

        public Vector2 Position { get; set; }

        public Color4 Color { get; set; }

        public void Update(float elapsedTime)
        {
            _speedVector.Y += 2 * elapsedTime;
            Position += _speedVector * elapsedTime;
            _lifeTime -= elapsedTime;
        }

        public bool IsAlive()
        {
            return _lifeTime > 0;
        }
    }
}