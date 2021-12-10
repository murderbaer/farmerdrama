using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model;

using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class PauseVisual : IDrawOverlay
    {
        private Pause _pause;

        private string _pauseBackground;

        public PauseVisual(GameObject goPause)
        {
            _pause = goPause.GetComponent<Pause>();
            _pauseBackground = "FarmGame.Resources.Graphics.SpriteSheets.QuestionBackground.png";
        }

        public void DrawOverlay()
        {
            if (!_pause.IsPaused)
            {
                return;
            }

            Box2 spritePos = new Box2(0, 0, 1, 1);
            SpriteRenderer.DrawSprite(_pauseBackground, new Box2(0, 0, 16, 9), spritePos);

            TextHelper.Instance.DrawText("Paused", 6, 3, Color4.BlueViolet, size: 1);
            TextHelper.Instance.DrawText("Press O to resume", 5, 5, Color4.LimeGreen, size: 0.5f);
        }
    }
}