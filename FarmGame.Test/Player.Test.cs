using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace FarmGame.Test
{
    [TestClass]
    public class PlayerTest
    {
        Player player = new Player();

        [TestMethod]
        public void TestPosition()
        {
            player.Position = new Vector2(10, 15);
            Assert.AreEqual(player.Position.X, 10);
            Assert.AreEqual(player.Position.Y, 15);
        }

        [TestMethod]
        public void TestMovementSpeed()
        {
            player.MovementSpeed = 5;
            Assert.AreEqual(player.MovementSpeed, 5);
        }

        [TestMethod]
        public void TestItemInHand()
        {
            Assert.AreEqual(player.ItemInHand.Type, ItemType.EMPTY);
            player.ItemInHand = new Item(ItemType.WATERBUCKET);
            Assert.AreEqual(player.ItemInHand.Type, ItemType.WATERBUCKET);
        }

        [TestMethod]
        public void TestInteract()
        {
            var farmland = new GridCellFarmLand(534);
            player.ItemInHand = new Item(ItemType.WATERBUCKET);
            player.Interact(farmland);
            Assert.IsTrue(farmland.IsWatered);
            Assert.AreEqual(player.ItemInHand.Type, ItemType.EMPTY);

            var water = new GridCellWater(1);
            player.Interact(water);
            Assert.AreEqual(player.ItemInHand.Type, ItemType.WATERBUCKET);

            farmland.State = FarmLandState.FULLGROWN;
            player.ItemInHand = new Item();
            player.Interact(farmland);

            Assert.AreEqual(player.ItemInHand.Type, ItemType.WHEET);
            Assert.AreEqual(farmland.State, FarmLandState.EMPTY);
            
        }
    }
}
