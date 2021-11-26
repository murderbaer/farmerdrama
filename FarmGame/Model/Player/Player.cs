using System.Collections.Generic;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class Player : IUpdatable, IPosition, IKeyDownListener, IKeyUpListener
    {
        private TiledHandler _tiledHandler = TiledHandler.Instance;

        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        public Player()
        {
            // Set starting position
            var playerPos = _tiledHandler.TiledPlayerPos.SelectNodes("object");
            float posX = float.Parse(playerPos[0].Attributes["x"].Value);
            float posY = float.Parse(playerPos[0].Attributes["y"].Value);
            int pixelSize = _tiledHandler.TilePixelSize;
            Position = new Vector2(posX / pixelSize, posY / pixelSize);
            Position = new Vector2(0, 0);
        }

        public Vector2 Position { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        public void Update(float elapsedTime)
        {
            Move(elapsedTime);
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Add(args.Key);
        }

        public void KeyUp(KeyboardKeyEventArgs args)
        {
            _pressedKeys.Remove(args.Key);
        }

        private void Move(float elapsedTime)
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            Vector2 moveDirection = new ();
            moveDirection.X = (_pressedKeys.Contains(Keys.Right) ? 1 : 0) - (_pressedKeys.Contains(Keys.Left) ? 1 : 0);
            moveDirection.Y = (_pressedKeys.Contains(Keys.Down) ? 1 : 0) - (_pressedKeys.Contains(Keys.Up) ? 1 : 0);
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

                // Collision - Disabled for now
                // var gridCollision = CollisionHelper.GetTileCollisionBoxesAround(newPosition, world.Grid);
                // if (CollisionHelper.BoxCollide(playerBox, gridCollision))
                // {
                //     newPosition[direction] = Position[direction];
                // }
            }

            Position = newPosition;
        }
    }
}