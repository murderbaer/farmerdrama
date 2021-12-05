using System;
using System.Collections.Generic;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class PlayerItemInteraction : IKeyDownListener
    {
        private IPosition _position;

        private IReadOnlyGrid _grid;

        public PlayerItemInteraction(GameObject goPlayer, IReadOnlyGrid grid)
        {
            _position = goPlayer.GetComponent<IPosition>();
            _grid = grid;
            ItemInHand = new Item();
        }

        public Item ItemInHand { get; set; }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (args.Key == Keys.Space)
            {
                Interact();
            }
        }

        public void Interact()
        {
            var playerX = (int)Math.Floor(_position.Position.X);
            var playerY = (int)Math.Floor(_position.Position.Y);
            IGridCell cell = _grid[playerX, playerY];

            if (ItemInHand.Type != ItemType.EMPTY)
            {
                var success = cell.InteractWithItem(ItemInHand);
                if (success)
                {
                    ItemInHand = new Item();
                    return;
                }
            }

            var newItem = cell.TakeItem();
            if (newItem.Type != ItemType.EMPTY)
            {
                ItemInHand = newItem;
                return;
            }
        }

        // for testing
        public void Interact(IGridCell cell)
        {
            if (ItemInHand.Type != ItemType.EMPTY)
            {
                var success = cell.InteractWithItem(ItemInHand);
                if (success)
                {
                    ItemInHand = new Item();
                    return;
                }
            }

            var newItem = cell.TakeItem();
            if (newItem.Type != ItemType.EMPTY)
            {
                ItemInHand = newItem;
                return;
            }
        }
    }
}