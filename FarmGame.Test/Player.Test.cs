using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class PlayerTest
    {
        GameObject goPlayer = new GameObject();
        Player player = new Player(new CollisionGrid(null));
        PlayerItemInteraction playerItemInteraction;

        public PlayerTest()
        {
            goPlayer.Components.Add(player);
            goPlayer.Components.Add(playerItemInteraction);
            playerItemInteraction = new PlayerItemInteraction(goPlayer);
        }

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
            Assert.AreEqual(playerItemInteraction.ItemInHand.Type, ItemType.EMPTY);
            playerItemInteraction.ItemInHand = new Item(ItemType.WATERBUCKET);
            Assert.AreEqual(playerItemInteraction.ItemInHand.Type, ItemType.WATERBUCKET);
        }

        [TestMethod]
        public void TestInteract()
        {
            var farmland = new GridCellFarmLand(FarmLandState.EMPTY);
            playerItemInteraction.ItemInHand = new Item(ItemType.WATERBUCKET);
            playerItemInteraction.Interact(farmland);
            Assert.IsTrue(farmland.IsWatered);
            Assert.AreEqual(playerItemInteraction.ItemInHand.Type, ItemType.EMPTY);

            var water = new GridCellWater();
            playerItemInteraction.Interact(water);
            Assert.AreEqual(playerItemInteraction.ItemInHand.Type, ItemType.WATERBUCKET);

            farmland.State = FarmLandState.FULLGROWN;
            playerItemInteraction.ItemInHand = new Item();
            playerItemInteraction.Interact(farmland);

            Assert.AreEqual(playerItemInteraction.ItemInHand.Type, ItemType.WHEET);
            Assert.AreEqual(farmland.State, FarmLandState.EMPTY);
        }
    }
}
