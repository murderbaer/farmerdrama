using System;

using FarmGame.Core;
using FarmGame.Model.Input;

using OpenTK.Mathematics;

namespace FarmGame.Model
{
    public class PlayerItemInteraction : IUpdatable
    {
        private IPosition _position;

        private IReadOnlyGrid _grid;

        private InputHandler _input = InputHandler.Instance;

        private ParticleSystem _particleSystem;

        public PlayerItemInteraction(GameObject goPlayer, IReadOnlyGrid grid, GameObject particle)
        {
            _position = goPlayer.GetComponent<IPosition>();
            _grid = grid;
            _particleSystem = particle.GetComponent<ParticleSystem>();
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
                    if (ItemInHand == ItemType.WATERBUCKET)
                    {
                        _particleSystem.SpawnParticles(_position.Position, Color4.Aqua);
                    }

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