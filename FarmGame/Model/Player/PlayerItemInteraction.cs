using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class PlayerItemInteraction : IKeyDownListener
    {
        private IPosition _position;

        public PlayerItemInteraction(GameObject goPlayer)
        {
            _position = goPlayer.GetComponent<IPosition>();
            ItemInHand = new Item();
        }

        public Item ItemInHand { get; set; }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (args.Key == Keys.Space)
            {
                var playerX = (int)_position.Position.X;
                var playerY = (int)_position.Position.Y;

                // Interact();
            }
        }

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