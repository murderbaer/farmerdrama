using System.Collections.Generic;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class CollisionService : IService, IUpdatable
    {
        private readonly List<ICollidable> _collidables = new List<ICollidable>();

        private readonly Dictionary<ICollidable, Vector2> _collisionCorrections = new Dictionary<ICollidable, Vector2>();

        private IReadOnlyGrid _collisionGrid;

        public void SetCollisionGrid(IReadOnlyGrid collisionGrid)
        {
            _collisionGrid = collisionGrid;
        }

        public void AddCollidable(ICollidable collidable)
        {
            _collidables.Add(collidable);
        }

        public void Update(float elapsedTime)
        {
            _collisionCorrections.Clear();
            foreach (var pair in GetCollisionPairs())
            {
                var collisionResponse = CollisionHelper.GetCollisionResponse(pair[0], pair[1]);
                _collisionCorrections[pair[0]] = _collisionCorrections.GetValueOrDefault(pair[0], Vector2.Zero) - collisionResponse;
                _collisionCorrections[pair[1]] = _collisionCorrections.GetValueOrDefault(pair[1], Vector2.Zero) + collisionResponse;
            }

            foreach (var collidable in _collidables)
            {
                ApplyCollisions(collidable);
            }
        }

        private void ApplyCollisions(ICollidable collidable)
        {
            Vector2 newPosition = collidable.Position;
            for (int direction = 0; direction < 2; direction++)
            {
                newPosition[direction] = collidable.Position[direction] + _collisionCorrections[collidable][direction];
                var componentBox = CollisionHelper.GetCollisionBox(newPosition, size: collidable.CollisionRadius * 2, centered: true);

                // Collision - Disabled for now
                var gridCollision = CollisionHelper.GetTileCollisionBoxesAround(newPosition, _collisionGrid);
                if (CollisionHelper.BoxCollide(componentBox, gridCollision))
                {
                    newPosition[direction] = collidable.Position[direction];
                }
            }

            collidable.Position = newPosition;
        }

        private IEnumerable<ICollidable[]> GetCollisionPairs()
        {
            var pair = new ICollidable[2];
            for (var i = 0; i < _collidables.Count; i++)
            {
                for (var j = i + 1; j < _collidables.Count; j++)
                {
                    pair[0] = _collidables[i];
                    pair[1] = _collidables[j];
                    yield return pair;
                }
            }
        }
    }
}