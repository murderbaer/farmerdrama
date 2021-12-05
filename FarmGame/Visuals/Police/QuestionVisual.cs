using ImageMagick;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace FarmGame
{
    public class QuestionVisual : IDrawOverlay
    {
        private MagickImage _spriteSheet;

        private QuestionComponent _question;

        private TextHelper _textHelper = TextHelper.Instance;

        private int _spriteHandle;

        public QuestionVisual(GameObject goPolice)
        {
            _question = goPolice.GetComponent<QuestionComponent>();
            _spriteSheet = SpriteHelper.LoadTexture("FarmGame.Resources.Graphics.SpriteSheets.QuestionBackground.png");
            _spriteHandle = SpriteHelper.GenerateHandle(_spriteSheet);
            Sprite = new SpriteObject(_spriteSheet, 14);
        }

        public SpriteObject Sprite { get; }

        public void DrawOverlay()
        {
            if (_question.IsQuestioning)
            {
                DrawQuestionBackground();
                DrawQuestion();
                DrawAnswerOptions();
                DrawQuestionResponse();
            }
        }

        private void DrawQuestionBackground()
        {
            Box2 spritePos = new Box2(0, 0, 1, 1);
            GL.Color4(Color4.White);

            GL.BindTexture(TextureTarget.Texture2D, _spriteHandle);
            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(spritePos.Min);
            GL.Vertex2(0, 0);

            GL.TexCoord2(spritePos.Max.X, spritePos.Min.Y);
            GL.Vertex2(16, 0);

            GL.TexCoord2(spritePos.Max);
            GL.Vertex2(16, 9);

            GL.TexCoord2(spritePos.Min.X, spritePos.Max.Y);
            GL.Vertex2(0, 9);

            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void DrawQuestion()
        {
            _textHelper.DrawText(_question.GetCurrentQuestion(), 2.4f, 1.3f, Color4.LightBlue);
        }

        private void DrawAnswerOptions()
        {
            var answers = _question.GetAnswerOptions();
            if (answers == null)
            {
                return;
            }

            var currentAnswer = _question.GetCurrentAnswer();
            var color = Color4.White;
            string options = "yx";
            for (int i = 0; i < answers.Length; i++)
            {
                if (i == currentAnswer)
                {
                    color = Color4.Green;
                }
                else
                {
                    color = Color4.White;
                }

                _textHelper.DrawChar(options[i], 1.8f, 3f + (1.7f * i), color);
                _textHelper.DrawText(answers[i], 2.3f, 3f + (1.7f * i), color);
            }
        }

        private void DrawQuestionResponse()
        {
            var response = _question.GetCurrentAnswerResponse();
            if (response == null)
            {
                return;
            }

            _textHelper.DrawText(response, 2f, 6f, Color4.LightBlue);
        }
    }
}