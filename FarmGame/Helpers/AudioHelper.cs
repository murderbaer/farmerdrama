using ManagedBass;

namespace FarmGame.Helpers
{
    public class AudioHelper
    {

        public static int GetStepsHanlde()
        {
            return Bass.CreateStream("FarmGame/Resources/Sounds/step.wav");
        }

        public static void InitAudio()
        {
            Bass.Init(Bass.CurrentDevice);
        }
    }
}