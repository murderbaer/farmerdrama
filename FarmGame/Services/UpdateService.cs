using System.Collections.Generic;
using System.Linq;
using FarmGame.Core;

namespace FarmGame.Services
{
    public class UpdateService : IService, IUpdatable
    {
        private HashSet<IUpdatable> _updatables = new HashSet<IUpdatable>();

        public bool IsPaused { get; set; }

        public bool IsIntro { get; set; }

        public bool IsFinished { get; set; }

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Update(float elapsedTime)
        {
            if (IsPaused || IsIntro || IsFinished)
            {
                _updatables.OfType<IInput>().FirstOrDefault().Update(elapsedTime);
                return;
            }

            foreach (var updatable in _updatables)
            {
                updatable.Update(elapsedTime);
            }
        }
    }
}
