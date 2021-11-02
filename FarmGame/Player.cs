using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class Player : IPlayer
    {
        public Player()
        {
            // Set starting position
            Position = new Vector2(12, 12);
            ItemInHand = new Item();
        }

        public Vector2 Position { get; set; }

        public Item ItemInHand { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        public void Draw()
        {
            GL.Color4(Color4.Orange);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(Position.X - 0.4, Position.Y);
            GL.Vertex2(Position.X + 0.4, Position.Y);
            GL.Vertex2(Position.X, Position.Y - .8);
            GL.End();
        }

        public void Update(float elapsedTime, ref KeyboardState keyboard)
        {
            Vector2 moveDirection = new ();
            moveDirection.X = (keyboard.IsKeyDown(Keys.Right) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Left) ? 1 : 0);
            moveDirection.Y = (keyboard.IsKeyDown(Keys.Down) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Up) ? 1 : 0);
            if (moveDirection.X == 0 && moveDirection.Y == 0)
            {
                return;
            }

            moveDirection.Normalize();
            Position += moveDirection * elapsedTime * MovementSpeed;
        }

        public void Interact(IGridCell cell)
        {
            if (ItemInHand.Type != ItemType.EMPTY)
            {
                var success = cell.InteractWithItem(ItemInHand);
                if (success)
                {
                    return;
                }
            }

            var newItem = cell.TakeItem();
            if (newItem.Type != ItemType.EMPTY)
            {
                ItemInHand = newItem;
                return;
            }
            ItemInHand.Place(Position);
        }

        public Item GiveItem()
        {
            if (ItemInHand.Type != ItemType.EMPTY)
            {
                Item temp = ItemInHand;
                ItemInHand = new Item();
                return temp;
            }

            return ItemInHand;
        }

        public void TakeItem(Item newItem)
        {
            if (ItemInHand.Type == ItemType.EMPTY)
            {
                ItemInHand = newItem;
            }
        }
    }
}