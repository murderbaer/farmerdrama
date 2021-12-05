using System.Collections.Generic;

namespace FarmGame
{
    public class NudgingService : IService, IUpdatable
    {
        private const float NUDGESTIFFNESS = 5f;
        private readonly List<ICollidable> _collidables = new List<ICollidable>();

        public void AddCollidable(ICollidable collidable)
        {
            _collidables.Add(collidable);
        }

        public void Update(float elapsedTime)
        {
            foreach (var pair in GetCollisionPairs())
            {
                var collisionResponse = CollisionHelper.GetCollisionResponse(pair[0], pair[1]);
                pair[0].MovementVector -= collisionResponse * elapsedTime * NUDGESTIFFNESS;
                pair[1].MovementVector += collisionResponse * elapsedTime * NUDGESTIFFNESS;
            }
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