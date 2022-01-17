using System.Linq;

using FarmGame.Core;
using FarmGame.Model.Grid;
using FarmGame.Services;

namespace FarmGame
{
    public static class ServiceLoader
    {
        private static UpdateService _updateService = new UpdateService();

        private static NudgingService _nudgingService = new NudgingService();

        private static MovementService _movementService = new MovementService();

        public static void LoadServices(Scene scene)
        {
            scene.AddService(_updateService);
            scene.AddService(_nudgingService);
            scene.AddService(_movementService);
        }

        public static void InitializeServices(Scene scene, CollisionGrid colGrid)
        {
            foreach (var go in scene.GameObjects)
            {
                foreach (var component in go.Components.OfType<IUpdatable>())
                {
                    _updateService.AddUpdatable(component);
                }

                foreach (var component in go.Components.OfType<ICollidable>())
                {
                    _nudgingService.AddCollidable(component);
                }

                foreach (var component in go.Components.OfType<IMoving>())
                {
                    _movementService.AddMovable(component);
                }
            }

            _movementService.SetCollisionGrid(colGrid);
        }
    }
}