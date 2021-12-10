using System.Collections.Generic;
using System.Linq;
using FarmGame.Visuals;
using OpenTK.Windowing.Common;

namespace FarmGame.Core
{
    public class Scene : IScene
    {
        private readonly HashSet<GameObject> _gameObjects = new HashSet<GameObject>();

        private readonly HashSet<IService> _services = new HashSet<IService>();

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                foreach (var gameObject in _gameObjects)
                {
                    yield return gameObject;
                }
            }
        }

        public IEnumerable<IService> Services
        {
            get
            {
                foreach (var service in _services)
                {
                    yield return service;
                }
            }
        }

        public void AddService(IService service)
        {
            _services.Add(service);
        }

        public T GetService<T>()
        {
            return Services.OfType<T>().First();
        }

        public GameObject CreateGameObject(string name)
        {
            GameObject gameObject = new GameObject(this, name);
            _gameObjects.Add(gameObject);

            return gameObject;
        }

        public void DestroyGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
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
            foreach (var service in Services.OfType<IUpdatable>())
            {
                service.Update(elapsedTime);
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

            foreach (var gameObject in GetAllComponents<IDraw>())
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