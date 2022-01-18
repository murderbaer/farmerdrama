using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Model;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class ParticleManagerTest
    {
        ParticleManager manager;

        public ParticleManagerTest()
        {
            manager = new ParticleManager(Vector2.Zero, Color4.White);
        }

        [TestMethod]
        public void TestSpawnParticle()
        {
            manager.SpawnParticle();
            Assert.AreEqual(manager.Particles.Count, 1);
        }

        [TestMethod]
        public void TestIsDone()
        {
            Assert.IsTrue(manager.IsDone());
            manager.SpawnParticle();
            Assert.IsFalse(manager.IsDone());
        }

        [TestMethod]
        public void TestUpdate()
        {
            Assert.AreEqual(manager.Particles.Count, 0);
            manager.Update(0.2f);
            Assert.AreEqual(manager.Particles.Count, 1);
        }
    }
}