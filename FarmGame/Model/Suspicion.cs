namespace FarmGame
{
    public class Suspicion : IComponent
    {
        private float _value;

        public Suspicion(float value = 0)
        {
            Value = value;
        }

        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value > 100)
                {
                    _value = 100;
                }
                else if (value < 0)
                {
                    _value = 0;
                }
                else
                {
                    _value = value;
                }
            }
        }
    }
}