using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Core;
using FarmGame.Model;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class GameTimeTest
    {
        GameTime gameTime;

        public GameTimeTest()
        {
            gameTime = new GameTime();
        }

        [TestMethod]
        public void TestElapsedTime()
        {
            gameTime.Update(10f);
            Assert.AreEqual(gameTime.ElapsedTime, 10);
        }

    }
}