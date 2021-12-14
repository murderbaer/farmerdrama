using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Model.GridCells;
using FarmGame.Items;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class GridCellSeedStorageTest
    {
        GridCellSeedStorage cell = new GridCellSeedStorage(1f);

        [TestMethod]
        public void TestCollision()
        {
            Assert.IsFalse(cell.HasCollision);
        }

        // [TestMethod]
        // public void TestColor()
        // {
        //     Assert.AreEqual(cell.CellColor, Color4.BlueViolet);
        // }

        [TestMethod]
        public void TestTakeItem()
        {
            Assert.AreEqual(cell.TakeItem().Type, ItemType.SEED);
        }

        [TestMethod]
        public void TestInteractWithItem()
        {
            Assert.IsFalse(cell.InteractWithItem(new Item(ItemType.EMPTY)));
        }
    }
}
