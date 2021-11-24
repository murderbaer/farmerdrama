using System.Linq;
using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public static class Game
    {
        public static Scene LoadScene(GameWindow window)
        {
            var scene = new Scene();

            var goCamera = scene.CreateGameObject("Camera");
            LoadCamera(goCamera);

            var goBackground = scene.CreateGameObject("Background");
            goBackground.Components.Add(new Background());

            var goSuspicion = scene.CreateGameObject("Suspicion");
            LoadSuspicion(goSuspicion);

            var goPlayer = scene.CreateGameObject("Player");
            LoadPlayer(goPlayer);

            var goCorpse = scene.CreateGameObject("Corpse");
            LoadCorpse(goCorpse, window, scene);

            var goPolice = scene.CreateGameObject("Police");
            LoadPolice(goPolice);

            var cameraController = goCamera.GetComponent<CameraController>();
            cameraController.FollowGameObject(goPlayer);

            return scene;
        }

        public static void LoadCamera(GameObject goCamera)
        {
            var camera = new Camera();
            goCamera.Components.Add(camera);
            var smoothCamera = new SmoothCamera(goCamera);
            goCamera.Components.Add(smoothCamera);
            var freeCamComponent = new CameraController(goCamera);
            goCamera.Components.Add(freeCamComponent);
        }

        public static void LoadSuspicion(GameObject goSuspicion)
        {
            var suspicion = new Suspicion(30);
            goSuspicion.Components.Add(suspicion);
            var suspicionBar = new SuspicionBar(goSuspicion);
            goSuspicion.Components.Add(suspicionBar);
        }

        public static void LoadPlayer(GameObject goPlayer)
        {
            var player = new Player();
            goPlayer.Components.Add(player);
            var playerVisual = new PlayerVisual(goPlayer);
            goPlayer.Components.Add(playerVisual);
        }

        public static void LoadCorpse(GameObject goCorpse, GameWindow window, Scene scene)
        {
            var player = scene.GetGameObjects("Player").First();
            var corpse = new Corpse(player);
            goCorpse.Components.Add(corpse);
            var corpseVisual = new CorpseVisual(goCorpse);
            goCorpse.Components.Add(corpseVisual);
        }

        public static void LoadPolice(GameObject goPolice)
        {
            var police = new Police();
            goPolice.Components.Add(police);
            var policeVisual = new PoliceVisual(goPolice);
            goPolice.Components.Add(policeVisual);
        }
    }
}