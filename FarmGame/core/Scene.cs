using System.Collections.Generic;
using System.Linq;
using OpenTK.Windowing.Common;

namespace FarmGame
{
    public class Scene
    {
        public GameObject CreateGameObject(string name)
        {
            GameObject gameObject = new GameObject(name);
            if (isIterating)
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
            if (isIterating)
            {
                _toRemove.Add(gameObject);
            }
            else
            {
                _gameObjects.Remove(gameObject);
            }
        }

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
            foreach (var gameObject in GetAllComponents<IDrawGround>())
            {
                gameObject.DrawGround();
            }
            foreach (var gameObject in GetAllComponents<IDrawable>())
            {
                gameObject.Draw();
            }
            foreach (var gameObject in GetAllComponents<IDrawAbove>())
            {
                gameObject.DrawAbove();
            }
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

        private bool isIterating => _iterationCount > 0;

        private readonly HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

        private readonly HashSet<GameObject> _toAdd = new HashSet<GameObject>();

        private readonly HashSet<GameObject> _toRemove = new HashSet<GameObject>();

        private int _iterationCount = 0;
    }
}