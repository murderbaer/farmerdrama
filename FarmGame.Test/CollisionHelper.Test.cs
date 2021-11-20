using System.Collections.Generic;
using OpenTK.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class CollisionHelperTest
    {
        Box2 boxA = new Box2(0, 0, 1, 1);
        Box2 boxB = new Box2(1.5f, 1.5f, 3, 3);
        Box2 boxC = new Box2(0.5f, 0.5f, 2, 2);
        List<Box2> boxList = new List<Box2> { new Box2(2, 2, 3, 3), new Box2(2, 3, 3, 4) };
        [TestMethod]
        public void TestBoxCollide1()
        {
            Assert.IsFalse(CollisionHelper.BoxCollide(boxA, boxB));
            Assert.IsTrue(CollisionHelper.BoxCollide(boxA, boxC));
        }

        [TestMethod]
        public void TestBoxCollide2()
        {
            Assert.IsFalse(CollisionHelper.BoxCollide(boxA, boxList));
            Assert.IsTrue(CollisionHelper.BoxCollide(boxB, boxList));
        }

        [TestMethod]
        public void TestGetCollisionBox()
        {
            Box2 box = CollisionHelper.GetCollisionBox(0, 0, size: 2, centered: true);
            Assert.AreEqual(box.Min.X, -1);
            Assert.AreEqual(box.Min.Y, -1);
            Assert.AreEqual(box.Max.X, 1);
            Assert.AreEqual(box.Max.Y, 1);
        }
    }
}