using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Model;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class ParticleSystemTest
    {
        ParticleSystem system;

        public ParticleSystemTest()
        {
            system = new ParticleSystem();
        }

        [TestMethod]
        public void TestSpawnParticles()
        {
            system.SpawnParticles(Vector2.Zero, Color4.White);
            Assert.AreEqual(system.ParticleManagers.Count, 1);
        }

        [TestMethod]
        public void TestUpdate()
        {
            Assert.AreEqual(system.ParticleManagers.Count, 0);
            system.SpawnParticles(Vector2.Zero, Color4.White);
            system.Update(0.2f);
            Assert.AreEqual(system.ParticleManagers.Count, 1);
            system.Update(3f);
            Assert.AreEqual(system.ParticleManagers.Count, 0);
        }
    }
}