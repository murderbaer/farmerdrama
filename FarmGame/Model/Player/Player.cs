using System;
using System.Collections.Generic;

using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model.Input;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model
{
    public class Player : IUpdatable, IPosition, IMoving, IChangeDirection, ICollidable
    {
        private TiledHandler _tiledHandler = TiledHandler.Instance;

        private IInput input;

        public Player(GameObject goPlayer)
        {
            // Set starting position
            Position = _tiledHandler.TiledPlayerPos;
            input = goPlayer.GetComponent<IInput>();
        }

        public event EventHandler<OnChangeDirectionArgs> OnChangeDirection;

        public event EventHandler<OnPlaySoundArgs> OnPlaySound;

        public Vector2 Position { get; set; }

        public Vector2 MovementVector { get; set; }

        // Movement speed in Tiles per second
        public float MovementSpeed { get; set; } = 3f;

        public float CollisionRadius { get; set; } = 0.4f;

        public void Update(float elapsedTime)
        {
            Move(elapsedTime);
        }

        private void Move(float elapsedTime)
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            Vector2 moveDirection = input.PlayerDirection;

            OnChangeDirection?.Invoke(null, new OnChangeDirectionArgs(moveDirection));

            if (moveDirection.X == 0 && moveDirection.Y == 0)
            {
                return;
            }

            OnPlaySound?.Invoke(null, new OnPlaySoundArgs(Position, moveDirection));
            MovementVector = moveDirection.Normalized() * elapsedTime * MovementSpeed;
        }
    }
}