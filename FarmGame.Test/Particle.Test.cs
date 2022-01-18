using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Model;

using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class ParticleTest
    {
        Particle particle;

        public ParticleTest()
        {
            particle = new Particle(new Vector2(10, 15), new Vector2(5, 5), new Color4(1, 1, 1, 1));
        }

        [TestMethod]
        public void TestParticlePosition()
        {
            Assert.AreEqual(particle.Position.X, 10);
            Assert.AreEqual(particle.Position.Y, 15);
        }

        [TestMethod]
        public void TestParticleUpdate()
        {
            particle.Update(1f);
            Assert.AreEqual(particle.Position.X, 15);
            Assert.IsTrue(particle.Position.Y > 20);
        }

        [TestMethod]
        public void TestParticleIsAlive()
        {
            Assert.IsTrue(particle.IsAlive());
            particle.Update(2f);
            Assert.IsFalse(particle.IsAlive());
        }
    }
}