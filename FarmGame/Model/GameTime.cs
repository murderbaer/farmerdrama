using FarmGame.Core;

namespace FarmGame.Model
{
    public class GameTime : IUpdatable
    {
        public float ElapsedTime { get; private set; }

        public void Update(float elapsedTime)
        {
            ElapsedTime += elapsedTime;
        }
    }
}