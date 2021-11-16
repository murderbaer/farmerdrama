using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [TestClass]
    public class GridCellEarthTest
    {
        GridCellEarth cell = new GridCellEarth(90);

        [TestMethod]
        public void TestCollision()
        {
            Assert.IsFalse(cell.HasCollision);
        }

        [TestMethod]
        public void TestColor()
        {
            Assert.AreEqual(cell.CellColor, Color4.Green);
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
