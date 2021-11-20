using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImageMagick;

namespace FarmGame
{
    public class Player : IPlayer
    {
        private int _spriteHandle;
        private MagickImage _spriteSheet;
        private SpriteObject _playerSprite;
        public Player()
        {
            // Set starting position
            Position = TiledHandler.Instance.TiledPlayerPos;
            ItemInHand = new Item();
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.FarmPerson.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
            _playerSprite = new SpriteObject(_spriteSheet, 1);
        }

        public Vector2 Position { get; set; }

        public Item ItemInHand { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        public void Draw()
        {
            Box2 spritePos = SpriteHelper.GetTexCoordFromSprite(_playerSprite);

            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(Position.X, Position.Y);
            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(Position.X + 1, Position.Y);
            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(Position.X + 1, Position.Y + 1);
            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(Position.X, Position.Y + 1);
            GL.End();
        }

        public void Update(float elapsedTime, IWorld world)
        {
            Move(elapsedTime, world);
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

        private void Move(float elapsedTime, IWorld world)
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            var keyboard = world.Window.KeyboardState;
            Vector2 moveDirection = new();
            moveDirection.X = (keyboard.IsKeyDown(Keys.Right) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Left) ? 1 : 0);
            moveDirection.Y = (keyboard.IsKeyDown(Keys.Down) ? 1 : 0) - (keyboard.IsKeyDown(Keys.Up) ? 1 : 0);
            if (moveDirection.X == 0 && moveDirection.Y == 0)
            {
                return;
            }

            moveDirection.Normalize();
            var movementVector = moveDirection * elapsedTime * MovementSpeed;

            // check x direction first, then y direction
            for (int direction = 0; direction < 2; direction++)
            {
                newPosition[direction] = Position[direction] + movementVector[direction];
                var playerBox = CollisionHelper.GetCollisionBox(newPosition, size: 0.8f, centered: true);
                var gridCollision = CollisionHelper.GetTileCollisionBoxesAround(newPosition, world.Grid);
                if (CollisionHelper.BoxCollide(playerBox, gridCollision))
                {
                    newPosition[direction] = Position[direction];
                }
            }

            Position = newPosition;
        }
    }
}