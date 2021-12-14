using System.Linq;

using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model;
using FarmGame.Model.Grid;
using FarmGame.Services;
using FarmGame.Visuals;

using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public static class Game
    {
        private static CollisionGrid _colGrid;

        private static DataGrid _dataGrid;

        public static Scene LoadScene(GameWindow window)
        {
            var scene = new Scene();

            var updateService = new UpdateService();
            scene.AddService(updateService);

            var collisionService = new NudgingService();
            scene.AddService(collisionService);

            var movementService = new MovementService();
            scene.AddService(movementService);

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
            LoadCorpse(goCorpse, scene);

            var goPolice = scene.CreateGameObject("Police");
            LoadPolice(goPolice, scene);

            var goPause = scene.CreateGameObject("Pause");
            LoadPause(goPause);

            SearchCorpse seachCorpse = new SearchCorpse(goPolice, goSuspicion, goCorpse, goGrid);
            goPolice.Components.Add(seachCorpse);

            var cameraController = goCamera.GetComponent<CameraController>();
            cameraController.FollowGameObject(goPlayer, setPosition: true);

            GameObject goPig;
            for (int n = 0; n < 4; n++)
            {
                goPig = scene.CreateGameObject("Pig");
                LoadPig(goPig, goGrid);
            }

            foreach (var go in scene.GameObjects)
            {
                foreach (var component in go.Components.OfType<IUpdatable>())
                {
                    updateService.AddUpdatable(component);
                }

                foreach (var component in go.Components.OfType<ICollidable>())
                {
                    collisionService.AddCollidable(component);
                }

                foreach (var component in go.Components.OfType<IMoving>())
                {
                    movementService.AddMovable(component);
                }
            }

            movementService.SetCollisionGrid(_colGrid);
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
            var playerAnimation = new PlayerAnimation(goPlayer);
            goPlayer.Components.Add(playerAnimation);

            // add listener
            player.OnChangeDirection += playerAnimation.Animate;

            var playerItemInteraction = new PlayerItemInteraction(goPlayer, _dataGrid);
            goPlayer.Components.Add(playerItemInteraction);

            var palyerItemVisual = new CarryItemVisual(playerItemInteraction, goPlayer);
            goPlayer.Components.Add(palyerItemVisual);
        }

        private static void LoadGrid(GameObject goGrid)
        {
            var gridVisuals = new LayeredSpriteGrid();
            goGrid.Components.Add(gridVisuals);

            _dataGrid = new DataGrid(gridVisuals.ReactOnStateChange);
            goGrid.Components.Add(_dataGrid);

            _colGrid = new CollisionGrid(TiledHandler.Instance.LayerThree);
        }

        private static void LoadCorpse(GameObject goCorpse, Scene scene)
        {
            var player = scene.GetGameObjects("Player").First();
            var grid = scene.GetGameObjects("Grid").First();
            goCorpse.Components.Add(_dataGrid);
            var corpse = new Corpse(player, goCorpse);
            goCorpse.Components.Add(corpse);
            var corpseWaterPoisening = new CorpseWaterPoisening(goCorpse, grid);
            goCorpse.Components.Add(corpseWaterPoisening);
            var corpseVisual = new CorpseVisual(goCorpse);
            goCorpse.Components.Add(corpseVisual);
        }

        private static void LoadPolice(GameObject goPolice, Scene scene)
        {
            var police = new Police();
            goPolice.Components.Add(police);
            var policeVisual = new PoliceVisual(goPolice);
            goPolice.Components.Add(policeVisual);

            var policeAnimation = new PoliceAnimation(goPolice);
            goPolice.Components.Add(policeAnimation);

            var goPlayer = scene.GetGameObjects("Player").First();
            var goSuspicion = scene.GetGameObjects("Suspicion").First();
            var policeQuestion = new QuestionComponent(goPolice, goPlayer, goSuspicion);
            goPolice.Components.Add(policeQuestion);

            var questionVisual = new QuestionVisual(goPolice);
            goPolice.Components.Add(questionVisual);

            // add listener
            goPolice.GetComponent<IChangeDirection>().OnChangeDirection +=
                goPolice.GetComponent<IAnimate>().Animate;
        }

        private static void LoadPig(GameObject goPig, GameObject goGrid)
        {
            var pigPen = TiledHandler.Instance.PigPen;
            var moveRandom = new MoveRandomComponent(pigPen);
            goPig.Components.Add(moveRandom);
            var pigVisual = new PigVisual(goPig);
            goPig.Components.Add(pigVisual);
            var hunger = new Hunger(goPig);
            goPig.Components.Add(hunger);
            var eating = new Eating(goPig, goGrid);
            goPig.Components.Add(eating);
            var pigAnimation = new PigAnimation(goPig);
            goPig.Components.Add(pigAnimation);

            moveRandom.OnChangeDirection += pigAnimation.Animate;
        }

        private static void LoadPause(GameObject goPause)
        {
            var pause = new Pause(goPause);
            goPause.Components.Add(pause);
            var pauseVisual = new PauseVisual(goPause);
            goPause.Components.Add(pauseVisual);
        }
    }
}