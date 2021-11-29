using System.Collections.Generic;
using System.Linq;
using OpenTK.Windowing.Common;

namespace FarmGame
{
    public class Scene
    {
        private readonly HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

        private readonly HashSet<GameObject> _toAdd = new HashSet<GameObject>();

        private readonly HashSet<GameObject> _toRemove = new HashSet<GameObject>();

        private int _iterationCount = 0;

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                ++_iterationCount;
                foreach (var gameObject in _gameObjects)
                {
                    yield return gameObject;
                }

                --_iterationCount;
            }
        }

        private bool IsIterating => _iterationCount > 0;

        public GameObject CreateGameObject(string name)
        {
            GameObject gameObject = new GameObject(name);
            if (IsIterating)
            {
                _toAdd.Add(gameObject);
            }
            else
            {
                _gameObjects.Add(gameObject);
            }

            return gameObject;
        }

        public void DestroyGameObject(GameObject gameObject)
        {
            if (IsIterating)
            {
                _toRemove.Add(gameObject);
            }
            else
            {
                _gameObjects.Remove(gameObject);
            }
        }

        public IEnumerable<GameObject> GetGameObjects(string name)
        {
            return GameObjects.Where(gameObject => gameObject.Name == name);
        }

        public IEnumerable<TYPE> GetAllComponents<TYPE>()
        {
            return GameObjects.SelectMany(gameObject => gameObject.Components.OfType<TYPE>());
        }

        public void Update(float elapsedTime)
        {
            foreach (var gameObject in _toAdd)
            {
                _gameObjects.Add(gameObject);
            }

            _toAdd.Clear();
            foreach (var gameObject in _toRemove)
            {
                _gameObjects.Remove(gameObject);
            }

            _toRemove.Clear();
            foreach (var gameObject in GetAllComponents<IUpdatable>())
            {
                gameObject.Update(elapsedTime);
            }
        }

        public void Draw()
        {
            var camera = GetAllComponents<Camera>().FirstOrDefault();
            camera.SetOverlayMatrix();
            foreach (var gameObject in GetAllComponents<IDrawBackground>())
            {
                gameObject.DrawBackground();
            }

            camera.SetCameraMatrix();

            IDrawGridLayer gridDraw = GetAllComponents<IDrawGridLayer>().First<IDrawGridLayer>();
            gridDraw.DrawLayer(0);
            gridDraw.DrawLayer(1);
            gridDraw.DrawLayer(2);

            foreach (var gameObject in GetAllComponents<IDrawable>())
            {
                gameObject.Draw();
            }

            gridDraw.DrawLayer(3);

            camera.SetOverlayMatrix();
            foreach (var gameObject in GetAllComponents<IDrawOverlay>())
            {
                gameObject.DrawOverlay();
            }
        }

        public void Resize(int width, int height)
        {
            foreach (var gameObject in GetAllComponents<IResizable>())
            {
                gameObject.Resize(width, height);
            }
        }

        public void KeyDown(KeyboardKeyEventArgs args)
        {
            foreach (var gameObject in GetAllComponents<IKeyDownListener>())
            {
                gameObject.KeyDown(args);
            }
        }

        public void KeyUp(KeyboardKeyEventArgs args)
        {
            foreach (var gameObject in GetAllComponents<IKeyUpListener>())
            {
                gameObject.KeyUp(args);
            }
        }
    }
}