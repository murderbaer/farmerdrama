using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [TestClass]
    public class GridTest
    {
        Grid grid = new Grid(32, 18);

        [TestMethod]
        public void TestSize()
        {
            Assert.AreEqual(grid.Column, 32);
            Assert.AreEqual(grid.Row, 18);
        }

        [TestMethod]
        public void TestAccess()
        {
            grid[3, 2] = new GridCellSand();
            Assert.AreEqual(grid[3, 2].CellColor, Color4.Orange);
        }
    }
}
