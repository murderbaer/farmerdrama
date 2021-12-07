using System;

using FarmGame.Core;

namespace FarmGame.Model
{
    public class Hunger : IUpdatable
    {
        private float _hungerCounter = 0;

        private IMoving _moving;

        private float _initialMovementSpeed;

        public Hunger(GameObject goMoving)
        {
            _moving = goMoving.GetComponent<IMoving>();
            _initialMovementSpeed = _moving.MovementSpeed;
        }

        public float HungerCounter
        {
            get
            {
                return _hungerCounter;
            }

            set
            {
                _hungerCounter = Math.Max(0, value);
            }
        }

        public void Feed(float amount = 100)
        {
            HungerCounter -= amount;
        }

        public float GetMovementSpeed()
        {
            return _initialMovementSpeed * (1 + (_hungerCounter / 60));
        }

        public void Update(float elapsedTime)
        {
            _hungerCounter += elapsedTime;
            _moving.MovementSpeed = GetMovementSpeed();
        }
    }
}