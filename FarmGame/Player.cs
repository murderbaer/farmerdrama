using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;

namespace FarmGame
{
    public class Player
    {
        private Item _itemInHand;
        public Vector2 Position { get; set; }

        public Item ItemInHand {get {return _itemInHand;} }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        public Player() {
            // Set starting position
            Position = new(12, 12);
            _itemInHand = new();
        }

        public Item GiveItem()
        {
            if (_itemInHand.Type != ItemType.EMPTY)
            {
                Item temp = _itemInHand;
                _itemInHand = new();
                return temp;
            }
            return _itemInHand;
        }

        public void TakeItem(Item newItem)
        {
            if (_itemInHand.Type == ItemType.EMPTY)
            {
                _itemInHand = newItem;
            }
        }
    }
}