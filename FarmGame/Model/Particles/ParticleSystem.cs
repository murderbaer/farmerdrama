using System.Collections.Generic;
using FarmGame.Core;
using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class ParticleSystem : IUpdatable
    {
        public HashSet<ParticleManager> ParticleManagers { get; } = new HashSet<ParticleManager>();

        public void SpawnParticles(Vector2 position, Color4 color)
        {
            ParticleManager p = new ParticleManager(position, color);
            ParticleManagers.Add(p);
        }

        public void Update(float elapsedTime)
        {
            foreach (var particleManager in ParticleManagers)
            {
                particleManager.Update(elapsedTime);
                if (particleManager.IsDone())
                {
                    ParticleManagers.Remove(particleManager);
                }
            }
        }
    }
}
