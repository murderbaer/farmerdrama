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

            var goGrid = scene.CreateGameObject("Grid");
            LoadGrid(goGrid);

            var goSuspicion = scene.CreateGameObject("Suspicion");
            LoadSuspicion(goSuspicion);

            var goPlayer = scene.CreateGameObject("Player");
            LoadPlayer(goPlayer);

            var goCorpse = scene.CreateGameObject("Corpse");
            LoadCorpse(goCorpse, window, scene);

            var goPolice = scene.CreateGameObject("Police");
            LoadPolice(goPolice);
            SearchCorpse seachCorpse = new SearchCorpse(goPolice, goSuspicion, goCorpse, goGrid);
            goPolice.Components.Add(seachCorpse);

            var cameraController = goCamera.GetComponent<CameraController>();
            cameraController.FollowGameObject(goPlayer, setPosition: true);

            return scene;
        }

        private static void LoadCamera(GameObject goCamera)
        {
            var camera = new Camera();
            goCamera.Components.Add(camera);
            var smoothCamera = new SmoothCamera(goCamera);
            goCamera.Components.Add(smoothCamera);
            var freeCamComponent = new CameraController(goCamera);
            goCamera.Components.Add(freeCamComponent);
        }

        private static void LoadSuspicion(GameObject goSuspicion)
        {
            var suspicion = new Suspicion(30);
            goSuspicion.Components.Add(suspicion);
            var suspicionBar = new SuspicionBar(goSuspicion);
            goSuspicion.Components.Add(suspicionBar);
        }

        private static void LoadPlayer(GameObject goPlayer)
        {
            var player = new Player();
            goPlayer.Components.Add(player);
            var playerVisual = new PlayerVisual(goPlayer);
            goPlayer.Components.Add(playerVisual);
        }

        private static void LoadGrid(GameObject goGrid)
        {
            var gridVisuals = new LayeredSpriteGrid();
            goGrid.Components.Add(gridVisuals);

            SpriteGrid l1 = gridVisuals.GetWholeLayer(0);
            SpriteGrid l2 = gridVisuals.GetWholeLayer(1);
            SpriteGrid l3 = gridVisuals.GetWholeLayer(2);
            SpriteGrid temp = SpriteGrid.SquashGrids(l1, l2);
            temp = SpriteGrid.SquashGrids(temp, l3);

            var grid = new GridData(temp);
            goGrid.Components.Add(grid);
        }

        private static void LoadCorpse(GameObject goCorpse, GameWindow window, Scene scene)
        {
            var player = scene.GetGameObjects("Player").First();
            var corpse = new Corpse(player);
            goCorpse.Components.Add(corpse);
            var corpseVisual = new CorpseVisual(goCorpse);
            goCorpse.Components.Add(corpseVisual);
        }

        private static void LoadPolice(GameObject goPolice)
        {
            var police = new Police();
            goPolice.Components.Add(police);
            var policeVisual = new PoliceVisual(goPolice);
            goPolice.Components.Add(policeVisual);
        }
    }
}