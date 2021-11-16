using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [TestClass]
    public class GridCellTest
    {
        GridCell cell = new GridCell(1);

        [TestMethod]
        public void TestCollision()
        {
            Assert.IsFalse(cell.HasCollision);
            cell.HasCollision = true;
        	Assert.IsTrue(cell.HasCollision);
        }

        [TestMethod]
        public void TestColor()
        {
            cell.CellColor = Color4.White;
            Assert.AreEqual(cell.CellColor, Color4.White);
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
