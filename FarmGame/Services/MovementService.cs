using System.Collections.Generic;

using FarmGame.Core;
using FarmGame.Helpers;

using OpenTK.Mathematics;

namespace FarmGame.Services
{
    public class MovementService : IService, IUpdatable
    {
        private readonly List<IMoving> _movables = new List<IMoving>();

        private IReadOnlyGrid _collisionGrid;

        public int FrozenCounter { get; set; } = 0;

        public void AddMovable(IMoving movable)
        {
            _movables.Add(movable);
        }

        public void SetCollisionGrid(IReadOnlyGrid collisionGrid)
        {
            _collisionGrid = collisionGrid;
        }

        public void Update(float elapsedTime)
        {
            if (FrozenCounter != 0)
            {
                foreach (var movable in _movables)
                {
                    movable.MovementVector = Vector2.Zero;
                }
            }

            foreach (var movable in _movables)
            {
                if (movable is ICollidable collidable)
                {
                    ApplyMovement(collidable);
                }
                else
                {
                    ApplyMovement(movable);
                }
            }
        }

        private void ApplyMovement(IMoving movable)
        {
            movable.Position += movable.MovementVector;
            movable.MovementVector = Vector2.Zero;
        }

        private void ApplyMovement(ICollidable collidable)
        {
            Vector2 newPosition = collidable.Position;
            for (int direction = 0; direction < 2; direction++)
            {
                newPosition[direction] = collidable.Position[direction] + collidable.MovementVector[direction];
                var componentBox = CollisionHelper.GetCollisionBox(newPosition, size: collidable.CollisionRadius * 2, centered: true);

                // Collision - Disabled for now
                var gridCollision = CollisionHelper.GetTileCollisionBoxesAround(newPosition, _collisionGrid);
                if (CollisionHelper.BoxCollide(componentBox, gridCollision))
                {
                    newPosition[direction] = collidable.Position[direction];
                }
            }

            collidable.Position = newPosition;
            collidable.MovementVector = Vector2.Zero;
        }
    }
}