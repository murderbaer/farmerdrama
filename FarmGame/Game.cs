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
            var colGrid = LoadGrid(goGrid);

            var goSuspicion = scene.CreateGameObject("Suspicion");
            LoadSuspicion(goSuspicion);

            var goPlayer = scene.CreateGameObject("Player");
            LoadPlayer(goPlayer, colGrid);

            var goCorpse = scene.CreateGameObject("Corpse");
            LoadCorpse(goCorpse, window, scene);

            var goPolice = scene.CreateGameObject("Police");
            LoadPolice(goPolice);

            SearchCorpse seachCorpse = new SearchCorpse(goPolice, goSuspicion, goCorpse, goGrid);
            goPolice.Components.Add(seachCorpse);

            var cameraController = goCamera.GetComponent<CameraController>();
            cameraController.FollowGameObject(goPlayer, setPosition: true);

            GameObject goPig;
            for (int n = 0; n < 1; n++)
            {
                goPig = scene.CreateGameObject("Pig");
                LoadPig(goPig);
            }

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

        private static void LoadPlayer(GameObject goPlayer, IReadOnlyGrid colGrid)
        {
            var player = new Player(colGrid);
            goPlayer.Components.Add(player);
            var playerVisual = new PlayerVisual(goPlayer);
            goPlayer.Components.Add(playerVisual);
        }

        private static IReadOnlyGrid LoadGrid(GameObject goGrid)
        {
            var gridVisuals = new LayeredSpriteGrid();
            goGrid.Components.Add(gridVisuals);

            SpriteGrid l1 = gridVisuals.GetWholeLayer(0);
            SpriteGrid l2 = gridVisuals.GetWholeLayer(1);
            SpriteGrid l3 = gridVisuals.GetWholeLayer(2);
            SpriteGrid temp = SpriteGrid.SquashGrids(l1, l2);
            temp = SpriteGrid.SquashGrids(temp, l3);

            var grid = new DataGrid(temp);
            goGrid.Components.Add(grid);

            return new CollisionGrid(l3);
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

        private static void LoadPig(GameObject goPig)
        {
            var pigPen = TiledHandler.Instance.PigPen;
            var moveRandom = new MoveRandomComponent(pigPen);
            goPig.Components.Add(moveRandom);
            var pigVisual = new PigVisual(goPig);
            goPig.Components.Add(pigVisual);
            var hunger = new Hunger(goPig);
            goPig.Components.Add(hunger);
        }
    }
}