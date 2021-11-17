using OpenTK.Mathematics;

namespace FarmGame
{
    public class CorpseSearchComponent : IUpdatable
    {
        public bool IsFound { get; set; }

        public float CalculateSearchDistance(IWorld world)
        {
            var suspicion = world.Suspicion.Value;
            var corpsePosition = world.Corpse.Position;
            var policePosition = world.Movable.Position;
            var hiddenFactor = world.Grid[(int)corpsePosition.X, (int)corpsePosition.Y].HiddenFactor;
            var holdFactor = world.Corpse.IsPlaced ? 1f : 1.3f;

            return 4 * (1 + (suspicion / 100)) * hiddenFactor * holdFactor;
        }

        public bool Search(IWorld world)
        {
            if (IsFound)
            {
                return true;
            }

            var distance = CalculateSearchDistance(world);
            var corpsePosition = world.Corpse.Position;
            var policePosition = world.Movable.Position;

            IsFound = Vector2.Distance(corpsePosition, policePosition) < distance;
            if (IsFound)
            {
                FinishSearch(world);
            }

            return IsFound;
        }

        public void FinishSearch(IWorld world)
        {
            world.Movable.EndPath();
            System.Console.WriteLine("Corpse found!");
        }

        public void Update(float elapsedTime, IWorld world)
        {
            Search(world);
        }
    }
}