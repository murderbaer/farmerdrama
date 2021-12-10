using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Model.GridCells;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class GridCellTest
    {
        GridCell cell = new GridCell(1f);

        [TestMethod]
        public void TestCollision()
        {
            Assert.IsFalse(cell.HasCollision);
            cell.HasCollision = true;
            Assert.IsTrue(cell.HasCollision);
        }

        [TestMethod]
        public void TestTakeItem()
        {
            Assert.AreEqual(cell.TakeItem().Type, ItemType.EMPTY);
        }

        [TestMethod]
        public void TestInteractWithItem()
        {
            Assert.IsFalse(cell.InteractWithItem(new Item(ItemType.EMPTY)));
        }
    }
}
