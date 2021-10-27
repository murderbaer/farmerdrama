using OpenTK.Windowing.GraphicsLibraryFramework;

namespace FarmGame {
    internal class Control {
        private readonly Model _model;
        private readonly View _view;
        public Control(Model model, View view) {
            _model = model;
            _view = view;
        }

        public void Update(float elapsedTime, KeyboardState keyboard) {

        }
    }
}