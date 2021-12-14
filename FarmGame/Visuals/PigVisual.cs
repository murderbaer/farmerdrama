using FarmGame.Core;
using FarmGame.Helpers;

using OpenTK.Mathematics;

namespace FarmGame.Visuals
{
    public class PigVisual : IDraw
    {
        private IPosition _position;

        private string _spriteName;

        private Box2 _texCoords;

        public PigVisual(GameObject goPig)
        {
            _position = goPig.GetComponent<IPosition>();
            _spriteName = "FarmGame.Resources.Graphics.SpriteSheets.FarmPerson.png";
            _texCoords = SpriteRenderer.GetTexCoord(_spriteName, 16, 45);
        }

        public SpriteObject Sprite { get; private set; }

        public void Draw()
        {
            SpriteRenderer.DrawSprite(_spriteName, _position.Position, 1, _texCoords, centered: true);
        }
    }
}