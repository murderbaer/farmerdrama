using System;

using FarmGame.Core;
using FarmGame.Model.Input;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model
{
    public class PlayerItemInteraction : IUpdatable
    {
        private IPosition _position;

        private IReadOnlyGrid _grid;

        private InputHandler _input = InputHandler.Instance;

        public PlayerItemInteraction(GameObject goPlayer, IReadOnlyGrid grid)
        {
            _position = goPlayer.GetComponent<IPosition>();
            _grid = grid;
            ItemInHand = ItemType.EMPTY;
        }

        public ItemType ItemInHand { get; set; }

        public void Update(float elapsedTime)
        {
            if (_input.Interact)
            {
                Interact();
            }
        }

        public void Interact()
        {
            var playerX = (int)Math.Floor(_position.Position.X);
            var playerY = (int)Math.Floor(_position.Position.Y);
            IGridCell cell = _grid[playerX, playerY];

            if (ItemInHand != ItemType.EMPTY)
            {
                var success = cell.InteractWithItem(ItemInHand);
                if (success)
                {
                    ItemInHand = ItemType.EMPTY;
                    return;
                }
            }

            var newItem = cell.TakeItem();
            if (newItem != ItemType.EMPTY)
            {
                ItemInHand = newItem;
                return;
            }
        }

        // for testing
        public void Interact(IGridCell cell)
        {
            if (ItemInHand != ItemType.EMPTY)
            {
                var success = cell.InteractWithItem(ItemInHand);
                if (success)
                {
                    ItemInHand = ItemType.EMPTY;
                    return;
                }
            }

            var newItem = cell.TakeItem();
            if (newItem != ItemType.EMPTY)
            {
                ItemInHand = newItem;
                return;
            }
        }
    }
}