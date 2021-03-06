using System.Linq;

using FarmGame.Audio;
using FarmGame.Core;
using FarmGame.Helpers;
using FarmGame.Model;
using FarmGame.Model.Grid;
using FarmGame.Model.Input;
using FarmGame.Visuals;

using OpenTK.Windowing.Desktop;

namespace FarmGame
{
    public static class Game
    {
        private static CollisionGrid _colGrid;

        private static DataGrid _dataGrid;

        public static Scene LoadScene()
        {
            var scene = new Scene();

            ServiceLoader.LoadServices(scene);

            var goCamera = scene.CreateGameObject("Camera");
            LoadCamera(goCamera);

            var goBackground = scene.CreateGameObject("Background");
            goBackground.Components.Add(new Background());

            var goParticle = scene.CreateGameObject("Particle");
            LoadParticles(goParticle);

            var goGrid = scene.CreateGameObject("Grid");
            LoadGrid(goGrid);

            var goSuspicion = scene.CreateGameObject("Suspicion");
            LoadSuspicion(goSuspicion);

            var goPlayer = scene.CreateGameObject("Player");
            LoadPlayer(goPlayer, scene);

            var goCorpse = scene.CreateGameObject("Corpse");
            LoadCorpse(goCorpse, scene);

            var goPolice = scene.CreateGameObject("Police");
            LoadPolice(goPolice, scene);

            var goPause = scene.CreateGameObject("Pause");
            LoadPause(goPause);

            SearchCorpse seachCorpse = new SearchCorpse(goPolice, goSuspicion, goCorpse, goGrid);
            goPolice.Components.Add(seachCorpse);

            var goIntro = scene.CreateGameObject("Intro");
            LoadIntro(goIntro);

            var goGameOver = scene.CreateGameObject("GameOver");
            LoadGameOver(goGameOver, scene);

            var cameraController = goCamera.GetComponent<CameraController>();
            cameraController.FollowGameObject(goPlayer, setPosition: true);

            GameObject goPig;
            for (int n = 0; n < 4; n++)
            {
                goPig = scene.CreateGameObject("Pig");
                LoadPig(goPig, goGrid);
            }

            ServiceLoader.InitializeServices(scene, _colGrid);
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

            AudioMaster.Init(goCamera);
            goCamera.Components.Add(AudioMaster.Instance);
        }

        private static void LoadParticles(GameObject goParticle)
        {
            var particleSystem = new ParticleSystem();
            goParticle.Components.Add(particleSystem);
            var particleVisual = new ParticleVisual(goParticle);
            goParticle.Components.Add(particleVisual);
        }

        private static void LoadSuspicion(GameObject goSuspicion)
        {
            var suspicion = new Suspicion(30);
            goSuspicion.Components.Add(suspicion);
            var suspicionBar = new SuspicionBar(goSuspicion);
            goSuspicion.Components.Add(suspicionBar);
        }

        private static void LoadPlayer(GameObject goPlayer, Scene scene)
        {
            var player = new Player();
            goPlayer.Components.Add(player);

            var playerVisual = new PlayerVisual(goPlayer);
            goPlayer.Components.Add(playerVisual);
            var playerAnimation = new PlayerAnimation(goPlayer);
            goPlayer.Components.Add(playerAnimation);

            var playerSound = AudioMaster.Instance.GetStepsHandle(goPlayer.GetComponent<IMoving>());
            goPlayer.Components.Add(playerSound);

            GameObject particleSystem = scene.GetGameObjects("Particle").First();

            var playerItemInteraction = new PlayerItemInteraction(goPlayer, _dataGrid, particleSystem);
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
            var grid = scene.GetGameObjects("Grid").First();
            goCorpse.Components.Add(_dataGrid);
            var corpse = new Corpse(goCorpse);
            goCorpse.Components.Add(corpse);
            var corpseWaterPoisening = new CorpseWaterPoisening(goCorpse, grid);
            goCorpse.Components.Add(corpseWaterPoisening);
            var corpseVisual = new CorpseVisual(goCorpse);
            goCorpse.Components.Add(corpseVisual);
        }

        private static void LoadPolice(GameObject goPolice, Scene scene)
        {
            var police = new Police(scene.GetGameObjects("Suspicion").First());
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

            var playerSound = AudioMaster.Instance.GetStepsHandle(goPolice.GetComponent<IMoving>());
            goPlayer.Components.Add(playerSound);
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
            var pigSound = AudioMaster.Instance.GetPigSnortHandle(goPig.GetComponent<IMoving>());
            goPig.Components.Add(pigSound);
        }

        private static void LoadPause(GameObject goPause)
        {
            var pause = new Pause(goPause);
            goPause.Components.Add(pause);
            var pauseVisual = new PauseVisual(goPause);
            goPause.Components.Add(pauseVisual);
        }

        private static void LoadIntro(GameObject goIntro)
        {
            var intro = new Intro(goIntro);
            goIntro.Components.Add(intro);
            var introVisual = new IntroVisual(goIntro);
            goIntro.Components.Add(introVisual);
        }

        private static void LoadGameOver(GameObject goGameOver, Scene scene)
        {
            var playTime = new GameTime();
            goGameOver.Components.Add(playTime);
            var goSearchCorpse = scene.GetGameObjects("Police").First();
            var gameOver = new GameOverVisual(goGameOver, goSearchCorpse);
            goGameOver.Components.Add(gameOver);
        }
    }
}