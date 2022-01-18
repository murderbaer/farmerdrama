using FarmGame.Core;
using FarmGame.Model;

using OpenTK.Graphics.OpenGL;

namespace FarmGame.Visuals
{
    public class ParticleVisual : IDraw
    {
        private ParticleSystem _particleSystem;

        public ParticleVisual(GameObject goParticle)
        {
            _particleSystem = goParticle.GetComponent<ParticleSystem>();
        }

        public ISpriteObject Sprite { get; private set; }

        public void Draw()
        {
            foreach (var particleManager in _particleSystem.ParticleManagers)
            {
                foreach (var particle in particleManager.Particles)
                {
                    GL.Begin(PrimitiveType.Quads);
                    GL.Color4(particle.Color);
                    GL.Vertex2(particle.Position.X, particle.Position.Y);
                    GL.Vertex2(particle.Position.X + 0.1f, particle.Position.Y);
                    GL.Vertex2(particle.Position.X + 0.1f, particle.Position.Y + 0.1f);
                    GL.Vertex2(particle.Position.X, particle.Position.Y + 0.1f);
                    GL.End();
                }
            }
        }
    }
}