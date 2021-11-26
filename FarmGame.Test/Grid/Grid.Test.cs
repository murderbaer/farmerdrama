using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
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
