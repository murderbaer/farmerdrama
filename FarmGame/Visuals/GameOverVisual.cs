using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model;

using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class GameOverVisual : IDrawOverlay
    {
        private GameTime _gameTime;

        private SearchCorpse _searchCorpse;

        private string _gameOverBackground = "FarmGame.Resources.Graphics.SpriteSheets.GameOver.png";

        public GameOverVisual(GameObject goGameOver, GameObject goSearchCorpse)
        {
            _gameTime = goGameOver.GetComponent<GameTime>();
            _searchCorpse = goSearchCorpse.GetComponent<SearchCorpse>();
        }

        public void DrawOverlay()
        {
            if (!_searchCorpse.IsFound)
            {
                return;
            }

            Box2 spritePos = new Box2(0, 0, 1, 1);
            SpriteRenderer.DrawSprite(_gameOverBackground, new Box2(0, 0, 16, 9), spritePos);
            TextHelper.Instance.DrawText(string.Format("{0:0.00}", _gameTime.ElapsedTime), 7.5f, 5.4f, Color4.White, size: 1);
        }
    }
}