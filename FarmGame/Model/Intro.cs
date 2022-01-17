using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model.Input;
using FarmGame.Services;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame.Model
{
    public class Intro : IKeyDownListener
    {
        private static int _introLength = 3;

        private UpdateService _updateService;

        public Intro(GameObject goPause)
        {
            _updateService = goPause.Scene.GetService<UpdateService>();
            _updateService.IsIntro = true;
        }

        public int IntroCounter { get; private set; } = 0;

        public bool IsIntro
        {
            get
            {
                return IntroCounter < _introLength;
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (IntroCounter >= _introLength)
            {
                return;
            }

            if (args.Key == Keys.Space)
            {
                IntroCounter++;
                if (IntroCounter == _introLength)
                {
                    _updateService.IsIntro = false;
                }
            }
        }
    }
}