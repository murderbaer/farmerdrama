using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using System.Diagnostics.CodeAnalysis;

using FarmGame.Core;
using FarmGame.Model;
using FarmGame.Model.Grid;

namespace FarmGame.Test
{
    [ExcludeFromCodeCoverageAttribute]
    [TestClass]
    public class PlayerTest
    {
        Scene scene = null;
        GameObject goPlayer;
        GameObject goCorpse;
        Player player = new Player();

        DataGrid grid = new DataGrid();
        PlayerItemInteraction playerItemInteraction;

        public PlayerTest()
        {
            goPlayer = new GameObject(scene);
            goCorpse = new GameObject(scene);
            goPlayer.Components.Add(player);
            goPlayer.Components.Add(playerItemInteraction);
            playerItemInteraction = new PlayerItemInteraction(goPlayer, grid);
            goCorpse.Components.Add(grid);
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
            Assert.AreEqual(playerItemInteraction.ItemInHand, ItemType.EMPTY);
            playerItemInteraction.ItemInHand = ItemType.WATERBUCKET;
            Assert.AreEqual(playerItemInteraction.ItemInHand, ItemType.WATERBUCKET);
        }

        [TestMethod]
        public void TestInteract()
        {
            var farmland = new GridCellFarmLand(FarmLandState.EMPTY, 0, 1f);
            playerItemInteraction.ItemInHand = ItemType.WATERBUCKET;
            playerItemInteraction.Interact(farmland);
            Assert.IsTrue(farmland.IsWatered);
            Assert.AreEqual(playerItemInteraction.ItemInHand, ItemType.EMPTY);

            var water = new GridCellWater(1f);
            playerItemInteraction.Interact(water);
            Assert.AreEqual(playerItemInteraction.ItemInHand, ItemType.WATERBUCKET);

            farmland.State = FarmLandState.FULLGROWN;
            playerItemInteraction.ItemInHand = ItemType.EMPTY;
            playerItemInteraction.Interact(farmland);

            Assert.AreEqual(playerItemInteraction.ItemInHand, ItemType.WHEET);
            Assert.AreEqual(farmland.State, FarmLandState.EMPTY);
        }
    }
}
