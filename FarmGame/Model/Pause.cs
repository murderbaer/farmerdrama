using FarmGame.Core;
using FarmGame.Services;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model
{
    public class Pause : IKeyDownListener
    {
        private UpdateService _updateService;

        public Pause(GameObject goPause)
        {
            _updateService = goPause.Scene.GetService<UpdateService>();
            IsPaused = false;
        }

        public bool IsPaused { get; private set; }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (args.Key == Keys.O)
            {
                _updateService.IsPaused = !_updateService.IsPaused;
                IsPaused = _updateService.IsPaused;
            }
        }
    }
}