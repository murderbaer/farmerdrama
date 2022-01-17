using FarmGame.Core;
using FarmGame.Model;
using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class IntroVisual : IDrawOverlay
    {
        private Intro _intro;

        private string[] _introSprites;

        public IntroVisual(GameObject goIntro)
        {
            _intro = goIntro.GetComponent<Intro>();
            _introSprites = new string[]
            {
                "FarmGame.Resources.Intro.Intro1.png",
                "FarmGame.Resources.Intro.Intro2.png",
                "FarmGame.Resources.Intro.Intro3.png",
            };
        }

        public void DrawOverlay()
        {
            if (!_intro.IsIntro)
            {
                return;
            }

            Box2 spritePos = new Box2(0, 0, 1, 1);
            SpriteRenderer.DrawSprite(_introSprites[_intro.IntroCounter], new Box2(0, 0, 16, 9), spritePos);
        }
    }
}