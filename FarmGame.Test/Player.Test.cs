using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class PlayerTest
    {
        GameObject goPlayer = new GameObject();
        Player player = new Player();
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

        [TestMethod]
        public void TestPickUpCorpse()
        {
            Corpse testCorpse = new Corpse(goPlayer);
            player.Position = testCorpse.Position;
            playerItemInteraction.ItemInHand = new Item();
            testCorpse.IsPlaced = false;

            Assert.AreEqual(testCorpse.IsPlaced, false);

            player.Position = new Vector2(testCorpse.Position.X + 10,testCorpse.Position.Y + 10);
            testCorpse.Update(1f);

            Assert.AreEqual(player.Position.X, testCorpse.Position.X);
            Assert.AreEqual(player.Position.Y, testCorpse.Position.Y);
            
            testCorpse.IsPlaced = true;
            Assert.AreEqual(testCorpse.IsPlaced, true);
            
            player.Position = new Vector2(testCorpse.Position.X + 10,testCorpse.Position.Y + 10);
            testCorpse.Update(1f);
            
            Assert.AreNotEqual(player.Position.X, testCorpse.Position.X);
            Assert.AreNotEqual(player.Position.Y, testCorpse.Position.Y);
        }
    }
}
