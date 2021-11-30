using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class PigTest
    {
        GameObject goPig = new GameObject();
        MoveRandomComponent moveRandom;

        Hunger hunger;

        public PigTest()
        {
            moveRandom = new MoveRandomComponent(new Box2(3, 3, 10, 10));
            goPig.Components.Add(moveRandom);
            hunger = new Hunger(goPig);
            goPig.Components.Add(hunger);
        }

        [TestMethod]
        public void TestPosition()
        {
            moveRandom.Position = new Vector2(7, 8);
            Assert.AreEqual(moveRandom.Position.X, 7);
            Assert.AreEqual(moveRandom.Position.Y, 8);
        }

        [TestMethod]
        public void TestMovementSpeed()
        {
            moveRandom.MovementSpeed = 5;
            Assert.AreEqual(moveRandom.MovementSpeed, 5);
        }

        [TestMethod]
        public void TestGetRandomPosition()
        {
            for (int n = 0; n < 10; n++)
            {
                var position = moveRandom.GetRandomPosition();
                Assert.IsTrue(position.X <= 10);
                Assert.IsTrue(position.Y <= 10);
                Assert.IsTrue(position.X >= 3);
                Assert.IsTrue(position.Y >= 3);
            }
        }

        [TestMethod]
        public void TestSetHunger()
        {
            hunger.HungerCounter = 10;
            Assert.AreEqual(hunger.HungerCounter, 10);
            hunger.HungerCounter = -10;
            Assert.AreEqual(hunger.HungerCounter, 0);
        }

        [TestMethod]
        public void TestMovementSpeedIncrease()
        {
            hunger.HungerCounter = 0;
            var speed1 = hunger.GetMovementSpeed();
            hunger.HungerCounter = 100;
            var speed2 = hunger.GetMovementSpeed();
            Assert.IsTrue(speed2 > speed1);
        }

        public void TestHungerUpdate()
        {
            hunger.HungerCounter = 0;
            var speed1 = hunger.GetMovementSpeed();
            hunger.Update(100);
            var speed2 = hunger.GetMovementSpeed();
            Assert.IsTrue(speed2 > speed1);
            Assert.AreEqual(hunger.HungerCounter, 100);
        }

        public void Feed()
        {
            hunger.HungerCounter = 200;
            hunger.Feed(100);
            Assert.AreEqual(hunger.HungerCounter, 100);
        }
    }
}