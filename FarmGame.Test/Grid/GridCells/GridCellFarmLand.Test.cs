using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Core;
using FarmGame.Model.Grid;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class GridCellFarmLandTest
    {
        GridCellFarmLand cell = new GridCellFarmLand(FarmLandState.EMPTY, 0, 1f);

        [TestMethod]
        public void TestCollision()
        {
            Assert.IsFalse(cell.HasCollision);
        }

        // [TestMethod] // test for sprite
        // public void TestColor()
        // {
        //     cell.State = FarmLandState.EMPTY;
        //     Assert.AreEqual(cell.CellColor, Color4.Brown);
        //     cell.State = FarmLandState.SEED;
        //     Assert.AreEqual(cell.CellColor, Color4.GreenYellow);
        //     cell.State = FarmLandState.HALFGROWN;
        //     Assert.AreEqual(cell.CellColor, Color4.LimeGreen);
        //     cell.State = FarmLandState.FULLGROWN;
        //     Assert.AreEqual(cell.CellColor, Color4.SeaGreen);
        //     cell.State = FarmLandState.OVERGROWN;
        //     Assert.AreEqual(cell.CellColor, Color4.DarkGreen);
        // }

        [TestMethod]
        public void TestTakeItem()
        {
            cell.State = FarmLandState.FULLGROWN;
            Assert.AreEqual(cell.TakeItem(), ItemType.WHEET);
            Assert.AreEqual(cell.State, FarmLandState.EMPTY);

            cell.State = FarmLandState.OVERGROWN;
            Assert.AreEqual(cell.TakeItem(), ItemType.EMPTY);
            Assert.AreEqual(cell.State, FarmLandState.EMPTY);

            cell.State = FarmLandState.SEED;
            Assert.AreEqual(cell.TakeItem(), ItemType.EMPTY);
            Assert.AreEqual(cell.State, FarmLandState.SEED);
        }

        [TestMethod]
        public void TestInteractWithItem()
        {
            cell.State = FarmLandState.EMPTY;
            Assert.IsFalse(cell.IsWatered);
            Assert.IsTrue(cell.InteractWithItem(ItemType.WATERBUCKET));
            Assert.IsTrue(cell.IsWatered);

            Assert.IsTrue(cell.InteractWithItem(ItemType.SEED));
            Assert.AreEqual(cell.State, FarmLandState.SEED);
        }

        [TestMethod]
        public void TestProgressState()
        {
            cell.State = FarmLandState.EMPTY;
            cell.ProgressState();
            Assert.AreEqual(cell.State, FarmLandState.EMPTY);

            cell.State = FarmLandState.SEED;
            cell.ProgressState();
            Assert.AreEqual(cell.State, FarmLandState.HALFGROWN);

            cell.State = FarmLandState.HALFGROWN;
            cell.ProgressState();
            Assert.AreEqual(cell.State, FarmLandState.FULLGROWN);

            cell.State = FarmLandState.FULLGROWN;
            cell.ProgressState();
            Assert.AreEqual(cell.State, FarmLandState.OVERGROWN);

            cell.State = FarmLandState.OVERGROWN;
            cell.ProgressState();
            Assert.AreEqual(cell.State, FarmLandState.OVERGROWN);
        }
    }
}
