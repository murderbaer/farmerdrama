using System.IO;

using ManagedBass;

namespace FarmGame.Helpers
{
    public class AudioHelper
    {
        public static int GetStepsHanlde()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "FarmGame/Resources/Sounds/step.wav");
            return Bass.CreateStream(path);
        }
    }
}