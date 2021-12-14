using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Model;
using FarmGame.Helpers;
using FarmGame.Core;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class GridTest
    {
        DataGrid grid = new DataGrid(TestEventMethod);

        [TestMethod]
        public void TestSize()
        {
            Assert.AreEqual(grid.Column, TiledHandler.Instance.BoardX);
            Assert.AreEqual(grid.Row, TiledHandler.Instance.BoardY);
        }

        [TestMethod]
        public void TestSuspicionFactor()
        {
            int gridSize = grid.Column * grid.Row;
            for (int i = 0; i < gridSize; i += 1) // somehow an overlow exception is thrown when les
            {
                int x = i % grid.Column;
                int y = i / grid.Row;
                if (grid[i].HiddenFactor != TiledHandler.Instance.HiddenFactorGrid[i])
                {
                    Assert.Fail();
                }
            }
            Assert.IsTrue(true);
        }

        private static void TestEventMethod(object sender, OnStateChangeArgs e)
        {

        }
    }
}
