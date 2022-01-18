using System;
using System.Collections.Generic;
using FarmGame.Core;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class ParticleManager : IUpdatable
    {
        private Vector2 _position;

        private float _lifeTime;

        private Color4 _color;

        private Random _random = new Random();

        public ParticleManager(Vector2 position, Color4 color, float lifeTime = 1f)
        {
            _position = position;
            _lifeTime = lifeTime;
            _color = color;
        }

        public HashSet<Particle> Particles { get; } = new HashSet<Particle>();

        public void Update(float elapsedTime)
        {
            _lifeTime -= elapsedTime;
            if (_lifeTime > 0)
            {
                SpawnParticle();
            }

            foreach (var particle in Particles)
            {
                particle.Update(elapsedTime);
                if (!particle.IsAlive())
                {
                    Particles.Remove(particle);
                }
            }
        }

        public void SpawnParticle()
        {
            Vector2 speed = new Vector2((float)(_random.NextDouble() * 2) - 1, (float)(_random.NextDouble() * 2) - 1);
            Particle p = new Particle(_position, speed, _color);
            Particles.Add(p);
        }

        public bool IsDone()
        {
            return Particles.Count == 0;
        }
    }
}