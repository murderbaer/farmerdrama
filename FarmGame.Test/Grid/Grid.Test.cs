using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [TestClass]
    public class GridTest
    {
        Grid grid = new Grid();

        [TestMethod]
        public void TestSize()
        {
            Assert.AreEqual(grid.Column, TiledHandler.Instance.BoardX);
            Assert.AreEqual(grid.Row, TiledHandler.Instance.BoardY);
        }


    }
}
