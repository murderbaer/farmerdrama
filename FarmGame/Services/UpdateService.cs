using System.Collections.Generic;
using FarmGame.Core;

namespace FarmGame.Services
{
    public class UpdateService : IService, IUpdatable
    {
        private HashSet<IUpdatable> _updatables = new HashSet<IUpdatable>();

        public bool IsPaused { get; set; }

        public bool IsFinished { get; set; }

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Update(float elapsedTime)
        {
            if (IsPaused || IsFinished)
            {
                return;
            }

            foreach (var updatable in _updatables)
            {
                updatable.Update(elapsedTime);
            }
        }
    }
}
