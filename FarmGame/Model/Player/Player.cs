using System;
using System.Collections.Generic;

using FarmGame.Core;
using FarmGame.Helpers;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model
{
    public class Player : IUpdatable, IPosition, IKeyDownListener, IKeyUpListener, IMoving, IChangeDirection, ICollidable
    {
        private TiledHandler _tiledHandler = TiledHandler.Instance;

        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        public Player()
        {
            // Set starting position
            Position = _tiledHandler.TiledPlayerPos;
        }

        public event EventHandler<OnChangeDirectionArgs> OnChangeDirection;

        public Vector2 Position { get; set; }

        public Vector2 MovementVector { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        public float CollisionRadius { get; set; } = 0.4f;

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

            OnChangeDirection?.Invoke(null, new OnChangeDirectionArgs(moveDirection));

            if (moveDirection.X == 0 && moveDirection.Y == 0)
            {
                return;
            }

            MovementVector = moveDirection.Normalized() * elapsedTime * MovementSpeed;
        }
    }
}