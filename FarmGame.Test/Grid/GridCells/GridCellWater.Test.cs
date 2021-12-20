using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Core;
using FarmGame.Model.Grid;


namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class GridCellWaterTest
    {
        GridCellWater cell = new GridCellWater(1f);

        [TestMethod]
        public void TestCollision()
        {
            Assert.IsFalse(cell.HasCollision);
        }

        [TestMethod]
        public void TestTakeItem()
        {
            Assert.AreEqual(cell.TakeItem(), ItemType.WATERBUCKET);
        }

        [TestMethod]
        public void TestInteractWithItem()
        {
            Assert.IsFalse(cell.InteractWithItem(ItemType.EMPTY));
        }


        [TestMethod]
        public void TestPoision()
        {
            var waterCell = new GridCellWater(1);

            waterCell.PoisenCounter = 0;
            Assert.AreEqual(ItemType.WATERBUCKET, waterCell.TakeItem());

            waterCell.PoisenCounter = 1;
            Assert.AreEqual(ItemType.EMPTY, waterCell.TakeItem());
        }
    }
}
