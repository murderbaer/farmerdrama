using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame
{
    public class QuestionComponent : IUpdatable, IKeyDownListener
    {
        private IPosition _positionPolice;

        private IPosition _positionPlayer;

        private Suspicion _suspicion;

        private XmlNodeList _questions;

        private XmlNode _currentQuestion;

        private int _selectedAnswer;

        private float _timeBetweenQuestions = 30;

        private float _timeSinceLastQuestion;

        public QuestionComponent(GameObject goPolice, GameObject goPlayer, GameObject goSuspicion)
        {
            _positionPolice = goPolice.GetComponent<IPosition>();
            _positionPlayer = goPlayer.GetComponent<IPosition>();
            _suspicion = goSuspicion.GetComponent<Suspicion>();
            _currentQuestion = null;
            _selectedAnswer = -1;
            _timeSinceLastQuestion = _timeBetweenQuestions;
            LoadQuestions();
        }

        public bool IsQuestioning { get; set; }

        public void ResetQuestion()
        {
            IsQuestioning = false;
            _currentQuestion = null;
            _selectedAnswer = -1;
            _timeSinceLastQuestion = 0;
        }

        public string GetCurrentQuestion()
        {
            return IsQuestioning ? _currentQuestion.Attributes["text"].Value : null;
        }

        public string[] GetAnswerOptions()
        {
            if (IsQuestioning)
            {
                return new string[]
                {
                    _currentQuestion.ChildNodes[0].Attributes["text"].Value,
                    _currentQuestion.ChildNodes[1].Attributes["text"].Value,
                };
            }

            return null;
        }

        public int GetCurrentAnswer()
        {
            return _selectedAnswer;
        }

        public string GetCurrentAnswerResponse()
        {
            if (IsQuestioning && _selectedAnswer != -1)
            {
                return _currentQuestion.ChildNodes[_selectedAnswer].ChildNodes[0].Attributes["text"].Value;
            }

            return null;
        }

        public void Update(float elapsedTime)
        {
            if (!IsQuestioning)
            {
                _timeSinceLastQuestion += elapsedTime;
                if (_timeSinceLastQuestion > _timeBetweenQuestions && (_positionPolice.Position - _positionPlayer.Position).LengthFast < 3)
                {
                    IsQuestioning = true;
                    GetRandomQuestion();
                }
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            if (IsQuestioning && _selectedAnswer == -1)
            {
                // Keyboard Layout issues: Z and Y are flipped on german keyboard
                if (args.Key == Keys.Z)
                {
                    _selectedAnswer = 0;
                    ApplySuspicion();
                }
                else if (args.Key == Keys.X)
                {
                    _selectedAnswer = 1;
                    ApplySuspicion();
                }
            }
            else if (IsQuestioning)
            {
                ResetQuestion();
            }
        }

        private void ApplySuspicion()
        {
            var amount = float.Parse(_currentQuestion.ChildNodes[_selectedAnswer].ChildNodes[0].Attributes["changeSuspicion"].Value);
            _suspicion.Value += amount;
        }

        private void LoadQuestions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("FarmGame.Resources.Questions.xml"))
            {
                var xml = new XmlDocument();
                xml.Load(stream);
                _questions = xml.GetElementsByTagName("question");
            }
        }

        private void GetRandomQuestion()
        {
            var sus = _suspicion.Value;
            var validQuestions = new List<XmlNode>();
            foreach (XmlNode question in _questions)
            {
                var minSuspicion = float.Parse(question.Attributes["minSuspicion"].Value);
                var maxSuspicion = float.Parse(question.Attributes["maxSuspicion"].Value);
                if (sus >= minSuspicion && sus <= maxSuspicion)
                {
                    validQuestions.Add(question);
                }
            }

            var random = new Random();
            _currentQuestion = validQuestions[random.Next(0, validQuestions.Count)];
        }
    }
}