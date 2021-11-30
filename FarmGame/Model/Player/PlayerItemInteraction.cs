using System;
using System.Collections.Generic;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class PlayerItemInteraction : IKeyDownListener, IKeyUpListener, IUpdatable
    {
        private IPosition _position;
        private IReadOnlyGrid _grid;

        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        private bool _spacePressedLastFrame;

        public PlayerItemInteraction(GameObject goPlayer, IReadOnlyGrid grid)
        {
            _position = goPlayer.GetComponent<IPosition>();
            _grid = grid;
            _spacePressedLastFrame = false;
            ItemInHand = new Item();
        }

        public Item ItemInHand { get; set; }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Add(args.Key);
        }

        public void KeyUp(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Remove(args.Key);
        }

        public void Update(float elapsedTime)
        {
            if (_pressedKeys.Contains(Keys.Space) && !_spacePressedLastFrame)
            {
                Interact();
            }
            else if (!_pressedKeys.Contains(Keys.Space))
            {
                _spacePressedLastFrame = false;
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

        #if DEBUG
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
        #endif
    }
}